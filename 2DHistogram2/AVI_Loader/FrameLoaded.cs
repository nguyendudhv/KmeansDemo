using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVI_Loader
{
    public partial class frmLoaded : Form
    {
        public frmLoaded()
        {
            InitializeComponent();
        }

        public void LoadBitmap(Bitmap bmp)
        {
           if (picBox.Image != null)
                picBox.Image.Dispose();

            picBox.Image = new Bitmap(bmp);
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;

            picBox.Invalidate();
            //   picPic.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void picBox_Click(object sender, EventArgs e)
        {

        }

        private void frmLoaded_Load(object sender, EventArgs e)
        {

        }
    }
}