namespace UndoPixels
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxNextFromAI = new System.Windows.Forms.PictureBox();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.pictureBoxSelectedImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxNextImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSourceImage = new System.Windows.Forms.ComboBox();
            this.labelGeneration = new System.Windows.Forms.Label();
            this.buttonRecovered = new System.Windows.Forms.Button();
            this.pictureBoxUnpixellated = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextFromAI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUnpixellated)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxNextFromAI
            // 
            this.pictureBoxNextFromAI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxNextFromAI.Location = new System.Drawing.Point(314, 61);
            this.pictureBoxNextFromAI.Name = "pictureBoxNextFromAI";
            this.pictureBoxNextFromAI.Size = new System.Drawing.Size(144, 146);
            this.pictureBoxNextFromAI.TabIndex = 0;
            this.pictureBoxNextFromAI.TabStop = false;
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(402, 13);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(56, 23);
            this.buttonLearn.TabIndex = 2;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.ButtonLearn_Click);
            // 
            // pictureBoxSelectedImage
            // 
            this.pictureBoxSelectedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSelectedImage.Location = new System.Drawing.Point(14, 61);
            this.pictureBoxSelectedImage.Name = "pictureBoxSelectedImage";
            this.pictureBoxSelectedImage.Size = new System.Drawing.Size(144, 146);
            this.pictureBoxSelectedImage.TabIndex = 3;
            this.pictureBoxSelectedImage.TabStop = false;
            // 
            // pictureBoxNextImage
            // 
            this.pictureBoxNextImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxNextImage.Location = new System.Drawing.Point(164, 61);
            this.pictureBoxNextImage.Name = "pictureBoxNextImage";
            this.pictureBoxNextImage.Size = new System.Drawing.Size(144, 146);
            this.pictureBoxNextImage.TabIndex = 4;
            this.pictureBoxNextImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Source Image to AI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image AI should recover";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(335, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Recovered Image";
            // 
            // comboBoxSourceImage
            // 
            this.comboBoxSourceImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceImage.FormattingEnabled = true;
            this.comboBoxSourceImage.Items.AddRange(new object[] {
            "32",
            "16",
            "8",
            "4",
            "2"});
            this.comboBoxSourceImage.Location = new System.Drawing.Point(14, 35);
            this.comboBoxSourceImage.Name = "comboBoxSourceImage";
            this.comboBoxSourceImage.Size = new System.Drawing.Size(144, 23);
            this.comboBoxSourceImage.TabIndex = 8;
            this.comboBoxSourceImage.SelectedValueChanged += new System.EventHandler(this.ComboBoxSourceImage_SelectedValueChanged);
            // 
            // labelGeneration
            // 
            this.labelGeneration.AutoSize = true;
            this.labelGeneration.Location = new System.Drawing.Point(167, 17);
            this.labelGeneration.Name = "labelGeneration";
            this.labelGeneration.Size = new System.Drawing.Size(89, 15);
            this.labelGeneration.TabIndex = 9;
            this.labelGeneration.Text = "Generation: 999";
            this.labelGeneration.Visible = false;
            // 
            // buttonRecovered
            // 
            this.buttonRecovered.Location = new System.Drawing.Point(552, 13);
            this.buttonRecovered.Name = "buttonRecovered";
            this.buttonRecovered.Size = new System.Drawing.Size(56, 23);
            this.buttonRecovered.TabIndex = 10;
            this.buttonRecovered.Text = "Test";
            this.buttonRecovered.UseVisualStyleBackColor = true;
            this.buttonRecovered.Click += new System.EventHandler(this.ButtonTest_Click);
            // 
            // pictureBoxUnpixellated
            // 
            this.pictureBoxUnpixellated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxUnpixellated.Location = new System.Drawing.Point(464, 61);
            this.pictureBoxUnpixellated.Name = "pictureBoxUnpixellated";
            this.pictureBoxUnpixellated.Size = new System.Drawing.Size(144, 146);
            this.pictureBoxUnpixellated.TabIndex = 11;
            this.pictureBoxUnpixellated.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(482, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Unpixellated32->0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 225);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxUnpixellated);
            this.Controls.Add(this.buttonRecovered);
            this.Controls.Add(this.labelGeneration);
            this.Controls.Add(this.comboBoxSourceImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxNextImage);
            this.Controls.Add(this.pictureBoxSelectedImage);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.pictureBoxNextFromAI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Her Majesty Queen Elizabeth II Unpixellated";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextFromAI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUnpixellated)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBoxNextFromAI;
        private Button buttonLearn;
        private PictureBox pictureBoxSelectedImage;
        private PictureBox pictureBoxNextImage;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox comboBoxSourceImage;
        private Label labelGeneration;
        private Button buttonRecovered;
        private PictureBox pictureBoxUnpixellated;
        private Label label4;
    }
}