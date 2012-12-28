using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KMeans
{
    public partial class frmMain1 : Form
    {
        public frmMain1()
        {
            InitializeComponent();
        }
        ImageList imglist = new ImageList();
        int iSTT = 1;
        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCluster frm = new frmCluster();
            frm.Show();
        }

        private void btnCluster_Click(object sender, EventArgs e)
        {
            iSTT = 1;
            lvClusters.Items.Clear();
            lvClusters.Refresh();
            _kMeans = new KMeans((Bitmap)picPreview.Image, Convert.ToInt32(txtNumClusters.Text), ImageProcessor.Colour.Types.RGB);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            string dir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            dir = Directory.GetParent(dir).FullName;
            this.ofdCluster.InitialDirectory = dir;

            this.ofdCluster.Filter = "IMAGES |*.jpg;*.bmp";
            this.ofdCluster.FileName = "";

            if (this.ofdCluster.ShowDialog() == DialogResult.OK)
            {
                Image b = Image.FromFile(ofdCluster.FileName);
                //txtImagePath.Text = ofdCluster.FileName;
                picPreview.Image = b;
                picPreview.Refresh();
            }
        }


        #region "CONTROL EVENTS"

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            ListViewItem lvitem;
            
            if (!_kMeans.Converged)
            {
                _kMeans.Iterate();
                //Image img= Image.
                //imageList.Images.Add(new Image.
                picProcessed.Image = _kMeans.ProcessedImage;
                imglist.Images.Add(picProcessed.Image);
                imglist.ImageSize = new System.Drawing.Size(100, 100);
                picProcessed.Refresh();
                _count++;
                lvitem = new ListViewItem("Ảnh thứ  " + _count.ToString());
                /*lvClusters.Items.Add(lvitem);
                lvClusters.LargeImageList = imglist;
                lvClusters.View = View.LargeIcon;
                lvClusters.Refresh();*/
            }
            else
            {
               
                _count = 0;
                timer1.Enabled = false;
                timer1.Stop();
            }
            if (timer1.Enabled == false)
            {
               
                picProcessed.Image = _kMeans.ProcessedImage;
                picProcessed.Refresh();
            }

            
            while ( _count>0)
            {
                lvitem = new ListViewItem("Ảnh thứ  " + iSTT.ToString());
                lvClusters.Items.Add(lvitem);
                iSTT++;
                _count--;
            }
            lvClusters.LargeImageList = imglist;
            for (int i = 0; i < lvClusters.Items.Count; i++)
            {
                lvClusters.Items[i].ImageIndex = i;
            }
            lvClusters.View = View.LargeIcon;
            lvClusters.Refresh();
            
            
        }
        #endregion

        KMeans _kMeans;
        int _count = 0;

        private void btnSaveDB_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvClusters.Items.Count; i++)
            {
                if (lvClusters.Items[i].Checked)
                {
                    picProcessed.Image = lvClusters.Items[i].ImageList.Images[i];
                    picProcessed.SizeMode = PictureBoxSizeMode.StretchImage;
                    picProcessed.Refresh();
                    i++;
                }

            }
        }
    }
}
