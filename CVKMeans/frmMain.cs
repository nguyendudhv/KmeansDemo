using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace KMeans
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
            
        private void Form1_Load(object sender, EventArgs e)
        {         
        }
                
           
        #region "CONTROL EVENTS"
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_kMeans.Converged)
            {
                _kMeans.Iterate();
                picProcessed.Image = _kMeans.ProcessedImage;
                picProcessed.Refresh();
                _count++;
                lblInfo.Text = "Iteration: " + _count.ToString();
            }
            else
            {
                lblInfo.Text = "Converged After " + _count.ToString() + " Iterations";
                btnKMeans.Text = "Start KMeans";
                _count = 0;
                timer1.Enabled = false;
                timer1.Stop();
            }
            if (timer1.Enabled == false)
            {
                picProcessed.Image = _kMeans.ProcessedImage;
                picProcessed.Refresh();
                //Image b = Image.FromStream(new StreamReader(
            }
        }
   
        private void btnLoadSequence_Click(object sender, EventArgs e)
        {
            string dir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            dir = Directory.GetParent(dir).FullName;
            this.openFileDialog1.InitialDirectory = dir;
            
            this.openFileDialog1.Filter="IMAGES |*.jpg;*.bmp";
            this.openFileDialog1.FileName = "";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picPreview.Image = null;
                picPreview.Refresh();
                lblInfo.Text = "";

                Image b = Image.FromFile(openFileDialog1.FileName);

                picPreview.Image = b;
                picProcessed.Image = null;
                picProcessed.Refresh();
            }            
        }
               
        private void btnTrack_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                _kMeans = new KMeans((Bitmap)picPreview.Image, Convert.ToInt32(txtNumClusters.Text), ImageProcessor.Colour.Types.RGB);
            else
                _kMeans = new KMeans((Bitmap)picPreview.Image, Convert.ToInt32(txtNumClusters.Text), ImageProcessor.Colour.Types.HSV);
            //timer1.Interval = Convert.ToInt32(txtNumClusters.Text);
            timer1.Enabled = true;
            timer1.Start();
        }
        #endregion

        KMeans _kMeans;
        int _count = 0;                
    }
}