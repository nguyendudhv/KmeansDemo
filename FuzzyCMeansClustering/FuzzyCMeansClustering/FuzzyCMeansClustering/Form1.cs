using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace FuzzyCMeansClustering
{
    public delegate void MyDelegate(string input);

    delegate void SetTextCallback(string text);

    public delegate void DelegateThreadFinished();


    public partial class Form1 : Form
    {
        public System.Drawing.Bitmap sourceImage;
        public System.Drawing.Bitmap filteredImage;
        public System.Drawing.Bitmap originalImage;
    
        private BackgroundWorker backgroundWorker;
        public Stopwatch stopWatch;


        public Form1()
        {
            InitializeComponent();
 
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            stopWatch = new Stopwatch();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;

            stopWatch.Reset();
            stopWatch.Start();
            backgroundWorker.RunWorkerAsync();
        }


    
       private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		toolStripProgressBar1.Value = e.ProgressPercentage;
		toolStripStatusLabel1.Text = e.UserState as String;
	}

       private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {

           
           if ((e.Cancelled == true))
           {
               toolStripStatusLabel1.Text = "Canceled!";
               button2.Enabled = true;
               button3.Enabled = false;
           }

           else if (!(e.Error == null))
           {
               toolStripStatusLabel1.Text = ("Error: " + e.Error.Message);
           }

            toolStripProgressBar1.Enabled = false;
            this.button2.Enabled = true;
            this.button3.Enabled = false;
       }



           // This method will run on a thread other than the UI thread.
           // Be sure not to manipulate any Windows Forms controls created
           // on the UI thread from this method.
       private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0, "Working...");
 
            filteredImage = (Bitmap)pictureBox1.Image.Clone();
            int numClusters = (int)numericUpDown2.Value;
            int maxIterations = (int)numericUpDown3.Value;
            double accuracy = (double)numericUpDown4.Value;

  

            List<ClusterPoint> points = new List<ClusterPoint>();


            for (int row = 0; row < originalImage.Width; ++row)
            {
                for (int col = 0; col < originalImage.Height; ++col)
                {

                    Color c2 = originalImage.GetPixel(row, col);
                    points.Add(new ClusterPoint(row, col, c2));

                }
            }



            List<ClusterCentroid> centroids = new List<ClusterCentroid>();
           
            //Create random points to use a the cluster centroids
            Random random = new Random();
            for (int i = 0; i < numClusters; i++)
            {
                int randomNumber1 = random.Next(sourceImage.Width);
                int randomNumber2 = random.Next(sourceImage.Height);
                centroids.Add(new ClusterCentroid(randomNumber1, randomNumber2, filteredImage.GetPixel(randomNumber1, randomNumber2))); 
            }
            FCM alg = new FCM(points, centroids, 2, filteredImage, (int)numericUpDown2.Value);

 
            int k = 0;
            do
            {
                if ((backgroundWorker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {

                    k++;
                    alg.J = alg.CalculateObjectiveFunction();
                    alg.CalculateClusterCentroids();
                    alg.Step();
                    double Jnew = alg.CalculateObjectiveFunction();
                    Console.WriteLine("Run method i={0} accuracy = {1} delta={2}", k, alg.J, Math.Abs(alg.J - Jnew));
                    toolStripStatusLabel2.Text = "Precision " + Math.Abs(alg.J - Jnew);

                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", stopWatch.Elapsed.Hours, stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10);
                    toolStripStatusLabel3.Text = "Duration: " + elapsedTime;

                    pictureBox2.Image = (Bitmap)alg.getProcessedImage;
                    backgroundWorker.ReportProgress((100 * k) / maxIterations, "Iteration " + k);

                    if (Math.Abs(alg.J - Jnew) < accuracy) break;
                }
            }
            while (maxIterations > k);
            Console.WriteLine("Done.");

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

           // Save the segmented image
           pictureBox2.Image = (Bitmap)alg.getProcessedImage.Clone(); 
           alg.getProcessedImage.Save("segmented.png");
           

           // Create a new image for each cluster in order to extract the features from the original image
           double[,] Matrix = alg.U;
           Bitmap[] bmapArray = new Bitmap[centroids.Count]; 
           for (int i = 0; i < centroids.Count; i++)
           {
               bmapArray[i] = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppRgb);
           }

           for (int j = 0; j < points.Count; j++)
           {
               for (int i = 0; i < centroids.Count; i++)
               {
                   ClusterPoint p = points[j];
                   if (Matrix[j, i] == p.ClusterIndex)
                   {
                        bmapArray[i].SetPixel((int)p.X, (int)p.Y, p.OriginalPixelColor);
                   }
               }
            }

           // Save the image for each segmented cluster
           for (int i = 0; i < centroids.Count; i++)
           {
               bmapArray[i].Save("Cluster" + i + ".png");
           }
           
           
           // Resource cleanup...more work to do here to avoid memory problems!!!
           backgroundWorker.ReportProgress(100, "Done in " + k + " iterations.");
           ////alg.Dispose();
           for (int i = 0; i < points.Count; i++)
           {
               points[i] = null;
           }
           for (int i = 0; i < centroids.Count; i++)
           {
               centroids[i] = null;
           }
           alg = null;
            //centroids.Clear();
            //points.Clear();
        }


        // Open image file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    button2.Enabled = true;
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                    originalImage = (Bitmap)pictureBox1.Image.Clone();
                    sourceImage = (Bitmap)pictureBox1.Image.Clone();
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
               }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show("Image format is not supported: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Invalid image: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Failed loading the image", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker != null)
            {
                backgroundWorker.CancelAsync();
            }

            toolStripStatusLabel1.Text = "Aborting, please wait...";

        }

 

    

    }
}
