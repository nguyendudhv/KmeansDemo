namespace _2DHistogram
{
    partial class frmPic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).BeginInit();
            this.SuspendLayout();
            // 
            // picPic
            // 
            this.picPic.Location = new System.Drawing.Point(0, 0);
            this.picPic.Name = "picPic";
            this.picPic.Size = new System.Drawing.Size(292, 273);
            this.picPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPic.TabIndex = 0;
            this.picPic.TabStop = false;
            this.picPic.Click += new System.EventHandler(this.picPic_Click);
            // 
            // frmPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.picPic);
            this.Name = "frmPic";
            this.Text = "Picture";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPic;
    }
}