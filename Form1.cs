using AINN;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace UndoPixels
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Neural network to unpixellate.
        /// </summary>
        readonly NeuralNetwork networkQE2Unpixellator;

        int scale = 2;

        // increasingly less pixellated sample images
        readonly Bitmap HerMajestyQueenElizabethIIPixellated32 = new(@".\Assets\HerMajesty32.png");
        readonly Bitmap HerMajestyQueenElizabethIIPixellated16 = new(@".\Assets\HerMajesty16.png");
        readonly Bitmap HerMajestyQueenElizabethIIPixellated8 = new(@".\Assets\HerMajesty8.png");
        readonly Bitmap HerMajestyQueenElizabethIIPixellated4 = new(@".\Assets\HerMajesty4.png");
        readonly Bitmap HerMajestyQueenElizabethIIPixellated2 = new(@".\Assets\HerMajesty2.png");
        readonly Bitmap HerMajestyQueenElizabethIIPixellated0 = new(@".\Assets\HerMajesty0.png");

        /// <summary>
        /// 
        /// </summary>
        readonly Dictionary<string, Bitmap> mapToPixellatedImages = new();

        /// <summary>
        /// Set to true when closing the app (to break out of the loop).
        /// </summary>
        bool exitLoop = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            labelGeneration.Visible = false;

            int[] layers = new int[] { 21024, 288, 21024 }; // 21024 = 144x146

            ActivationFunctions[] activationFunctions = new ActivationFunctions[] { ActivationFunctions.TanH, ActivationFunctions.TanH, ActivationFunctions.TanH, ActivationFunctions.TanH };

            networkQE2Unpixellator = new(0, layers, activationFunctions, false);

            mapToPixellatedImages.Add("32", HerMajestyQueenElizabethIIPixellated32);
            mapToPixellatedImages.Add("16", HerMajestyQueenElizabethIIPixellated16);
            mapToPixellatedImages.Add("8", HerMajestyQueenElizabethIIPixellated8);
            mapToPixellatedImages.Add("4", HerMajestyQueenElizabethIIPixellated4);
            mapToPixellatedImages.Add("2", HerMajestyQueenElizabethIIPixellated2);
            mapToPixellatedImages.Add("1", HerMajestyQueenElizabethIIPixellated0);
        }

        /// <summary>
        /// Create pixellated images in c:\temp. If you have filesystem protection enabled (as you should) a dotnet app cannot
        /// write to many locations, such as /Assets. You'll need to manually copy them
        /// </summary>
        private void GeneratePixellatedImages()
        {
            Bitmap sourceImageToGeneratePixellatedImagesFrom;

            sourceImageToGeneratePixellatedImagesFrom = new Bitmap(HerMajestyQueenElizabethIIPixellated0);

            scale = 1;

            for (int i = 0; i < 6; i++)
            {
                if (pictureBoxNextFromAI.Image == null)
                {
                    pictureBoxNextFromAI.Image = new Bitmap(sourceImageToGeneratePixellatedImagesFrom);

                    Bitmap b0 = ResizeImage((Bitmap)sourceImageToGeneratePixellatedImagesFrom, sourceImageToGeneratePixellatedImagesFrom.Width, sourceImageToGeneratePixellatedImagesFrom.Height);
                    b0.Save($@"c:\temp\HerMajesty0.png");
                    continue;
                }

                // twice as pixellated
                scale *= 2;
                sourceImageToGeneratePixellatedImagesFrom = ResizeImage((Bitmap)sourceImageToGeneratePixellatedImagesFrom, sourceImageToGeneratePixellatedImagesFrom.Width / 2, sourceImageToGeneratePixellatedImagesFrom.Height / 2);

                // pixellate, and save
                Bitmap b = DrawPixelsEnlarged(sourceImageToGeneratePixellatedImagesFrom, scale);

                // you'll need to copy to assets folder
                b.Save($@"c:\temp\HerMajesty{scale}.png");
            }

            MessageBox.Show("Sample pixellated images created. Don't forget to copy them into the /Assets folder.");
        }

        /// <summary>
        /// We shrink the image, but if we unshrink (enlarge) it'll be antialiased with colours used.
        /// So it's safer to do it ourselves.
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private static Bitmap DrawPixelsEnlarged(Bitmap pixels, int scale)
        {
            // final image (consistent for all). We paint the enlarged image onto this.
            Bitmap bitmap = new(144, 146);

            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.None;

            for (int y = 0; y < pixels.Height; y++)
            {
                for (int x = 0; x < pixels.Width; x++)
                {
                    using SolidBrush brush = new(pixels.GetPixel(x, y));
                    graphics.FillRectangle(brush, new Rectangle(x * scale, y * scale, scale, scale));
                }
            }

            graphics.Flush();

            return bitmap;
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="canvasWidth"></param>
        /// <param name="canvasHeight"></param>
        /// <returns></returns>
        private static Bitmap ResizeImage(Bitmap image, int canvasWidth, int canvasHeight)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            Bitmap thumbnail = new(canvasWidth, canvasHeight); // changed parm names

            using Graphics graphic = Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.Low;
            graphic.SmoothingMode = SmoothingMode.HighSpeed;
            graphic.PixelOffsetMode = PixelOffsetMode.None;
            graphic.CompositingQuality = CompositingQuality.HighSpeed;

            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)originalWidth;
            double ratioY = (double)canvasHeight / (double)originalHeight;

            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (originalWidth * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (originalHeight * ratio)) / 2);

            graphic.Clear(Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            return thumbnail;
        }

        /// <summary>
        /// We need all 21024 pixels (144px * 146px) as an array to input into AI. This
        /// capture what is on screen, and turns it into a byte array.
        /// </summary>
        internal static double[] CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(Bitmap img)
        {
            if (img is null) throw new ArgumentNullException(nameof(img), "image should be populated before calling this."); // can't cache what has been drawn!

            // get "bytes" of data representing the 4 byte per pixel .png
            Bitmap? srcDisplayBitMap = img;

            BitmapData? srcDisplayMapData = srcDisplayBitMap.LockBits(new Rectangle(0, 0, srcDisplayBitMap.Width, srcDisplayBitMap.Height), ImageLockMode.ReadOnly, img.PixelFormat);

            IntPtr srcDisplayMapDataPtr = srcDisplayMapData.Scan0;

            int strideDisplay = srcDisplayMapData.Stride;

            int totalLengthDisplay = Math.Abs(strideDisplay) * srcDisplayBitMap.Height;

            byte[] rgbValuesDisplay = new byte[totalLengthDisplay];

            System.Runtime.InteropServices.Marshal.Copy(srcDisplayMapDataPtr, rgbValuesDisplay, 0, totalLengthDisplay);

            srcDisplayBitMap.UnlockBits(srcDisplayMapData);

            double[] result = new double[totalLengthDisplay / 4];

            int idx = 0;

            // grey-scale the image, using the standard formula, then store a SINGLE pixel value into our double[] array for the AI.
            for (int i = 0; i < totalLengthDisplay; i += 4)
            {
                float colour = (rgbValuesDisplay[i] + rgbValuesDisplay[i + 1] + rgbValuesDisplay[i + 2]) / 3; // average
                result[idx++] = colour / 255;
            }

            return result;
        }

        /// <summary>
        /// User clicked [Learn], so we do, in a loop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLearn_Click(object sender, EventArgs e)
        {
            comboBoxSourceImage.SelectedIndex = 0;
            buttonLearn.Enabled = false; // stop clicking again

            int generation = 0;

            labelGeneration.Visible = true;

            ShowImage();

            exitLoop = false; // clicking [x] sets this, and closes.

            // we don't want to be doing this in a loop when we can do it ONCE
            double[] image0 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated0);
            double[] image2 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated2);
            double[] image4 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated4);
            double[] image8 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated8);
            double[] image16 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated16);
            double[] image32 = CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(HerMajestyQueenElizabethIIPixellated32);

            while (!exitLoop) // until close.
            {
                // back-propagate to train pixellated image with less pixellated.
                networkQE2Unpixellator.BackPropagate(image2, image0); // 2->0
                networkQE2Unpixellator.BackPropagate(image4, image2); // 4->2

                Application.DoEvents(); 
                
                networkQE2Unpixellator.BackPropagate(image8, image4); // 8->4
                networkQE2Unpixellator.BackPropagate(image16, image8); // 16->8
                
                Application.DoEvents();

                networkQE2Unpixellator.BackPropagate(image32, image16); // 32->16

                // show the unpixellated image
                labelGeneration.Text = $"GENERATION: {generation++}";

                int betterImage = int.Parse(comboBoxSourceImage.Text);

                // ask for the current image based on what it learnt. 32->16
                pictureBoxNextFromAI.Image?.Dispose();
                pictureBoxNextFromAI.Image = BitmapFromAIOutput(
                                            networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(mapToPixellatedImages[betterImage.ToString()])));

                Application.DoEvents();
            }
        }

        /// <summary>
        /// Turns the greyscale single byte pixels as an image.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Bitmap BitmapFromAIOutput(double[] data)
        {
            Bitmap bitmap = new(144, 146);

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int height;
            int width;
            int stride;
            int bytesPerPixel;
            int offset;

            try
            {
                height = bitmap.Height;
                width = bitmap.Width;
                stride = bmData.Stride;
                bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                offset = stride;

                int totalLength = Math.Abs(stride) * bitmap.Height;

                IntPtr ptr = bmData.Scan0;
                IntPtr dstPtr = bmData.Scan0;

                byte[] rgbValues = new byte[totalLength];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalLength);

                int p = 0;

                for (int y = 0; y < 146; y++)
                {
                    for (int x = 0; x < 144; x++)
                    {
                        int index = x * bytesPerPixel + (y * offset);

                        byte b = (byte)Math.Abs(Math.Round((float)(data[p] * 255F)));

                        rgbValues[index] = b; // b
                        rgbValues[index + 1] = b; // g
                        rgbValues[index + 2] = b; // r
                        rgbValues[index + 3] = 255; // alpha
                        p++;
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, dstPtr, totalLength);
            }
            finally
            {
                bitmap.UnlockBits(bmData);
            }

            return bitmap;
        }

        /// <summary>
        /// User changed the contents of drop down. Show their chosen image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSourceImage_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowImage();
        }

        /// <summary>
        /// Update the source/target imageds.
        /// </summary>
        private void ShowImage()
        {
            int betterImage = int.Parse(comboBoxSourceImage.Text) / 2;

            if (betterImage < 1) betterImage = 1;

            pictureBoxSelectedImage.Image?.Dispose();
            pictureBoxNextImage.Image?.Dispose();

            pictureBoxSelectedImage.Image = new Bitmap(mapToPixellatedImages[comboBoxSourceImage.Text]);
            pictureBoxNextImage.Image = new Bitmap(mapToPixellatedImages[betterImage.ToString()]);
        }

        /// <summary>
        /// Learn runs in a while(true), so to exit we have to break the loop. This is done by moving to
        /// while (!exitLoop), and on close setting exitLoop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitLoop = true; // make the loop exit 
        }

        /// <summary>
        /// Takes image 32, converts into 8,4,2,0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTest_Click(object sender, EventArgs e)
        {
            pictureBoxUnpixellated.Image?.Dispose();

            if (1 == 0)
            {
                // experiment 2, gives it a blank white image to see what happens.
                using Bitmap hugelyPixellated = new(HerMajestyQueenElizabethIIPixellated32.Width, HerMajestyQueenElizabethIIPixellated32.Height);
                using Graphics graphics = Graphics.FromImage(hugelyPixellated);
                graphics.Clear(Color.White);
                graphics.Flush();
                pictureBoxUnpixellated.Image = BitmapFromAIOutput(
                            networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(hugelyPixellated)));
            }
            else
            {
                using Bitmap hugelyPixellated = new(HerMajestyQueenElizabethIIPixellated32);

                // ask for the current image based on what it learnt.

                // 32 in => 16
                Bitmap progress = BitmapFromAIOutput(
                         networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(hugelyPixellated)));

                // 16 in => 8
                progress = BitmapFromAIOutput(
                         networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(progress)));

                // 8 in => 4
                progress = BitmapFromAIOutput(
                         networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(progress)));

                // 4 => 2
                progress = BitmapFromAIOutput(
                         networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(progress)));

                // 2 => 0
                pictureBoxUnpixellated.Image = BitmapFromAIOutput(
                         networkQE2Unpixellator.FeedForward(CopyImageOfVideoDisplayToAnAccessibleInMemoryArray(progress)));

            }
        }
    }
}