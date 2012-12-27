using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _2DHistogram
{
    public partial class frmPic : Form
    {
        public Bitmap bmpPic;

        public frmPic()
        {
            InitializeComponent();
        }

        public void LoadBitmap(Bitmap bmp)
        {
            if (bmpPic != null)
                bmpPic.Dispose();
            
            bmpPic = new Bitmap(bmp);
            
            if (picPic.Image != null)
                picPic.Image.Dispose();
                        
            picPic.Image = new Bitmap(bmpPic);

            picPic.SizeMode = PictureBoxSizeMode.AutoSize;// .StretchImage;
         //   picPic.SizeMode = PictureBoxSizeMode.Zoom;

           

        }

        private void frmPic_Load(object sender, EventArgs e)
        {

        }

        private void picPic_Click(object sender, EventArgs e)
        {
           
        }
    }
}