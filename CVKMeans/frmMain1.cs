using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace KMeans
{
    public delegate void MyDelegate(string input);

    delegate void SetTextCallback(string text);

    public delegate void DelegateThreadFinished();
    public partial class frmMain1 : Form
    {
        public System.Drawing.Bitmap sourceImage;
        public System.Drawing.Bitmap filteredImage;
        public System.Drawing.Bitmap originalImage;
        private BackgroundWorker backgroundWorker;
        private BindingList<SimilarityImages> similarityImages;
        private bool exit;
        public Stopwatch stopWatch;

        #region "GUI delegates body"
        private readonly UpdateListViewDelegate updateListViewDelegate = delegate(BindingList<SimilarityImages> images, ListView listView)
        {
            images.RaiseListChangedEvents = true;
            ImageList imageList = new ImageList();
            listView.Items.Clear();
            foreach (SimilarityImages image in images)
            {
                Image img = Image.FromFile(image.Destination.File.FullName);
                imageList.Images.Add(img);
                listView.Items.Add(new ListViewItem(image.Destination.File.Name + "=>" + image.Similarity.ToString()));
                listView.Refresh();
            }
            imageList.ImageSize = new Size(100, 100);
            listView.LargeImageList = imageList;
            for (int i = 0; i < listView.Items.Count; i++)
            {
                listView.Items[i].ImageIndex = i;
            }
            listView.View = View.LargeIcon;
            listView.Refresh();
        };

        private readonly SetMaximumDelegate setMaximumDelegate = delegate(ProgressBar progressBar, int value)
        {
            progressBar.Maximum = value;
        };

        private readonly UpdateOperationStatusDelegate updateOperationStatusDelegate = delegate(string format, System.Windows.Forms.Label label, ProgressBar progressBar, int value, DateTime startTime)
        {
            progressBar.Value = value;
            var percentage = Math.Round(((double)progressBar.Value / (double)progressBar.Maximum), 3);
            format += " {0}/{1} ({2}) Elapsed: {3} Estimated: {4}";

            var elapsed = DateTime.Now.Subtract(startTime);
            elapsed = new TimeSpan(elapsed.Days, elapsed.Hours, elapsed.Minutes, elapsed.Seconds, 0);

            var estimatedTicks = (elapsed.Ticks / value) * progressBar.Maximum;
            var estimated = new TimeSpan(estimatedTicks);
            estimated = new TimeSpan(estimated.Days, estimated.Hours, estimated.Minutes, estimated.Seconds, 0);

            label.Text = string.Format(format, progressBar.Value, progressBar.Maximum, percentage.ToString("P"), elapsed.ToString(), estimated.ToString());
        };

        private readonly ShowListViewDelegate showListViewDelegate = delegate(ListView listView)
        {
            listView.ResumeLayout();
            listView.Enabled = true;
        };
        #endregion

        #region "GUI delegates"
        private delegate void ProcessImagesDelegate(DataTable dt);

        private delegate void SetMaximumDelegate(ProgressBar progressBar, int value);

        private delegate void UpdateOperationStatusDelegate(string format, Label label, ProgressBar progressBar, int value, DateTime startTime);

        private delegate void UpdateListViewDelegate(BindingList<SimilarityImages> images, ListView listView);

        private delegate void DeleteImageDelegate(FileInfo fileInfo);

        private delegate void ShowListViewDelegate(ListView listView);
        #endregion
        public frmMain1()
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

        private void ProcessImages(DataTable dt)
        {
            var comparableImages = new List<ComparableImage>();

            //Invoke(setMaximumDelegate, new object[] { workingProgressBar, files.Length });

            var index = 0x0;

            var operationStartTime = DateTime.Now;
            int k = Application.StartupPath.IndexOf("\\bin");
            string sub = Application.StartupPath.Substring(k);
            string ImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - sub.Length) ;
            foreach (DataRow row in dt.Rows)
            {

                var comparableImage = new ComparableImage(new FileInfo(ImagePath+row["Url"].ToString()));
                comparableImages.Add(comparableImage);
                index++;
                //Invoke(updateOperationStatusDelegate, new object[] { "Processed images", workingLabel, workingProgressBar, index, operationStartTime });
            }

           // Invoke(setMaximumDelegate, new object[] { workingProgressBar, (comparableImages.Count * (comparableImages.Count - 1)) / 2 });

            index = 0;

            var similarityImagesSorted = new List<SimilarityImages>();

            operationStartTime = DateTime.Now;
            double maxSimilarity = 0;
            for (var i = 0; i < comparableImages.Count; i++)
            {
                if (exit)
                {
                    return;
                }
                ComparableImage sourceImage = new ComparableImage(new FileInfo(ImageName));
                var destination = comparableImages[i];
                var similarity = sourceImage.CalculateSimilarity(destination);
                 
                var sim = new SimilarityImages(sourceImage, destination, similarity);
                if (sim.Similarity>=double.Parse(txtPrecision.Value.ToString()))
                {
                    if (maxSimilarity < sim.Similarity)
                        maxSimilarity = sim.Similarity;
                    similarityImagesSorted.Add(sim);
                    index++;
                }
            }

            similarityImagesSorted.Sort();
            similarityImagesSorted.Reverse();
            similarityImages = new BindingList<SimilarityImages>(similarityImagesSorted);

            BeginInvoke(updateListViewDelegate, new object[] { similarityImages, lvImageSimilar });
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
                btnCluster.Enabled = true;
            }

            else if (!(e.Error == null))
            {
                toolStripStatusLabel1.Text = ("Error: " + e.Error.Message);
            }

            toolStripProgressBar1.Enabled = false;
            this.btnCluster.Enabled = true;
            this.btnCluster.Enabled = false;
        }



        // This method will run on a thread other than the UI thread.
        // Be sure not to manipulate any Windows Forms controls created
        // on the UI thread from this method.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Database dataBase = new Database();
           
            
            backgroundWorker.ReportProgress(0, "Working...");

            filteredImage = (Bitmap)picPreview.Image.Clone();
            int numClusters = (int)txtNumClusters.Value;
            int maxIterations = (int)txtIterations.Value;
            double accuracy = 0.00001;
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
            /*foreach (ClusterPoint p in points)
            {
                List<SqlParameter> listParam = new List<SqlParameter>(4);
                listParam.Add(new SqlParameter("@x",p.X));
                listParam.Add(new SqlParameter("@y", p.Y));
                listParam.Add(new SqlParameter("@Color", p.PixelColor.ToArgb()));
                listParam.Add(new SqlParameter("@ImageId ", 1));
                dataBase.RunProcedure("usp_ClusterPointsInsert", listParam.ToArray());
            }*/
            KMeansAlgorithm alg = new KMeansAlgorithm(points, centroids, 2, filteredImage, (int)txtNumClusters.Value);
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

                    picProcessed.Image = (Bitmap)alg.getProcessedImage;
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
            picProcessed.Image = (Bitmap)alg.getProcessedImage.Clone();
           


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
            k = Application.StartupPath.IndexOf("\\bin");
            string sub = Application.StartupPath.Substring(k);
            string ImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - sub.Length) + "\\AnhData\\";
            DirectoryInfo dirInfo = new DirectoryInfo(ImagePath);
            if (dirInfo.Exists)
                dirInfo.Create();    
            List<SqlParameter> listParam = new List<SqlParameter>(4);
            string imageName = ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1);
            listParam.Add(new SqlParameter("@ImageName", imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) + "_segmented.png"));
            listParam.Add(new SqlParameter("@Url", "\\AnhData\\" + imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) + "_segmented.png"));
            listParam.Add(new SqlParameter("@Width", picPreview.Image.Width));
            listParam.Add(new SqlParameter("@Height", picPreview.Image.Height));
            dataBase.RunProcedure("usp_ImageInsert", listParam.ToArray());
            alg.getProcessedImage.Save(ImagePath + imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) + "_segmented.png");
            // Save the image for each segmented cluster
            for (int i = 0; i < centroids.Count; i++)
            {
                bmapArray[i].Save(ImagePath + imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) + "_" + i + ".png");
                List<SqlParameter> listParam1 = new List<SqlParameter>(4);
                listParam1.Add(new SqlParameter("@ImageName", imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) + "_" + i + ".png"));
                listParam1.Add(new SqlParameter("@Url", "\\AnhData\\" + imageName.Substring(0, ofdCluster.FileName.Substring(ofdCluster.FileName.LastIndexOf(@"\") + 1).Length - imageName.Substring(imageName.LastIndexOf(".")).Length) +"_" + i + ".png"));
                listParam1.Add(new SqlParameter("@Width", picPreview.Image.Width));
                listParam1.Add(new SqlParameter("@Height", picPreview.Image.Height));
                dataBase.RunProcedure("usp_ImageInsert", listParam1.ToArray());
                /*DataTable dt = dataBase.RunProcedureGet("usp_ImageGetAfterInsert");
                if (dt.Rows.Count > 0)
                {

                    List<SqlParameter> listParam1 = new List<SqlParameter>(4);
                    listParam1.Add(new SqlParameter("@x", centroids[i].X));
                    listParam1.Add(new SqlParameter("@y", centroids[i].Y));
                    listParam1.Add(new SqlParameter("@Color", centroids[i].PixelColor.ToArgb()));
                    listParam1.Add(new SqlParameter("@ImageId ", dt.Rows[0][0]));
                    dataBase.RunProcedure("usp_ClusterCentroidsInsert", listParam1.ToArray());
                }*/
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
        private string ImageName;
        ImageList imglist = new ImageList();
        int iSTT = 1;
   
        private void btnCluster_Click(object sender, EventArgs e)
        {
            lvClusters.Items.Clear();
            lvClusters.Refresh();
            stopWatch.Reset();
            stopWatch.Start();
            backgroundWorker.RunWorkerAsync();
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
                txtImagePath.Text = ofdCluster.FileName;
                ImageName = ofdCluster.FileName;
                picPreview.Image = b;
                picPreview.Refresh();
                originalImage = (Bitmap)picPreview.Image.Clone();
                sourceImage = (Bitmap)picPreview.Image.Clone();
            }
        }

        
        private void btnBrowerQuery_Click(object sender, EventArgs e)
        {
            string dir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            dir = Directory.GetParent(dir).FullName;
            this.ofdImageQuery.InitialDirectory = dir;

            this.ofdImageQuery.Filter = "IMAGES |*.jpg;*.bmp;*.png";
            this.ofdImageQuery.FileName = "";

            if (this.ofdImageQuery.ShowDialog() == DialogResult.OK)
            {
                Image b = Image.FromFile(ofdImageQuery.FileName);
                ImageName = ofdImageQuery.FileName;
                picQuery.Image = b;
                picQuery.Refresh();
                originalImage = (Bitmap)picQuery.Image.Clone();
                sourceImage = (Bitmap)picQuery.Image.Clone();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            lvClusters.Items.Clear();
            lvClusters.Refresh();
            int k = Application.StartupPath.IndexOf("\\bin");
            string sub = Application.StartupPath.Substring(k);
            string ImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - sub.Length) + "\\AnhData";
            //DirectoryInfo directoryInfo;
            //FileInfo[] files;
            try
            {
                //directoryInfo = new DirectoryInfo(ImagePath);
                //files = directoryInfo.GetFiles("*.png", SearchOption.AllDirectories);
                //exit = false;
                Database db = new Database();
                DataTable dt = db.RunProcedureGet("usp_ImageGetAll");
                var processImagesDelegate = new ProcessImagesDelegate(ProcessImages);
                processImagesDelegate.BeginInvoke(dt, null, null);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Path not valid.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Path not valid.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void frmMain1_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
        }

        private void frmMain1_Load(object sender, EventArgs e)
        {
            LoadImageFromDB();
        }

        private void LoadImageFromDB()
        {
            int k = Application.StartupPath.IndexOf("\\bin");
            string sub = Application.StartupPath.Substring(k);
            string ImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - sub.Length);
            Database db = new Database();
            DataTable dt=db.RunProcedureGet("usp_ImageGetAll");
            ImageList imglist = new ImageList();
            foreach (DataRow row in dt.Rows)
            {
                Image image= Image.FromFile(ImagePath+row["Url"].ToString());
                imglist.Images.Add(image);
                lvImageDB.Items.Add(new ListViewItem(row["ImageName"].ToString()));
            }
            imglist.ImageSize = new System.Drawing.Size(200, 200);
            lvImageDB.LargeImageList = imglist;
            for (int i=0; i < lvImageDB.Items.Count; i++)
            {
                lvImageDB.Items[i].ImageIndex = i;
            }
            lvImageDB.Refresh();
        }
    }
}
