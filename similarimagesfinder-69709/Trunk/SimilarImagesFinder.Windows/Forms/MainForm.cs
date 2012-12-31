namespace EyeOpen.SimilarImagesFinder.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using EyeOpen.Imaging;

    internal partial class MainForm : Form
    {
        #region "GUI delegates body"
        private readonly UpdateDataGridViewDelegate updateDataGridViewDelegate = delegate(BindingList<SimilarityImages> images, DataGridView dataGridView, IList<Button> buttons, TextBox textBox)
        {
            images.RaiseListChangedEvents = true;
            dataGridView.DataSource = images;

            dataGridView.Columns["Similarity"].DisplayIndex = 0;
            dataGridView.Columns["Source"].DisplayIndex = 1;
            dataGridView.Columns["Destination"].DisplayIndex = 2;

            if (images.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
            }

            buttons[0].Enabled = true;
            buttons[1].Enabled = !buttons[0].Enabled;
            textBox.Enabled = buttons[0].Enabled;
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

        private readonly ShowGridDelegate showGridDelegate = delegate(DataGridView dataGridView)
        {
            dataGridView.ResumeLayout();
            dataGridView.Enabled = true;
        };
        #endregion

        private BindingList<SimilarityImages> similarityImages;
        private bool exit;

        public MainForm()
        {
            InitializeComponent();
        }

        #region "GUI delegates"
        private delegate void ProcessImagesDelegate(FileInfo[] files);

        private delegate void SetMaximumDelegate(ProgressBar progressBar, int value);

        private delegate void UpdateOperationStatusDelegate(string format, Label label, ProgressBar progressBar, int value, DateTime startTime);

        private delegate void UpdateDataGridViewDelegate(BindingList<SimilarityImages> images, DataGridView dataGridView, IList<Button> buttons, TextBox textBox);

        private delegate void DeleteImageDelegate(FileInfo fileInfo);

        private delegate void ShowGridDelegate(DataGridView dataGridView);
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            exit = true;
        }

        private static void ShowProjections(Graph graph, ComparableImage comparable)
        {
            graph.ClearCurves();
            graph.AddPlotCurve(Color.Red, comparable.Projections.HorizontalProjection);
            graph.AddPlotCurve(Color.Green, comparable.Projections.VerticalProjection);
            graph.Invalidate();
        }

        #region "Start/stop search"
        private void FindButtonClick(object sender, EventArgs e)
        {
            var folder = folderTextBox.Text;

            DirectoryInfo directoryInfo;
            FileInfo[] files;
            try
            {
                directoryInfo = new DirectoryInfo(folder);
                files = directoryInfo.GetFiles("*.png", SearchOption.AllDirectories);
                exit = false;
                findButton.Enabled = false;
                cancelButton.Enabled = !findButton.Enabled;
                folderTextBox.Enabled = findButton.Enabled;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Path not valid.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderTextBox.Enabled = true;
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Path not valid.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var processImagesDelegate = new ProcessImagesDelegate(ProcessImages);
            processImagesDelegate.BeginInvoke(files, null, null);
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            exit = true;
            findButton.Enabled = true;
            cancelButton.Enabled = !findButton.Enabled;
            folderTextBox.Enabled = findButton.Enabled;
        }
        #endregion

        private void ProcessImages(FileInfo[] files)
        {
            var comparableImages = new List<ComparableImage>();

            Invoke(setMaximumDelegate, new object[] { workingProgressBar, files.Length });

            var index = 0x0;

            var operationStartTime = DateTime.Now;

            foreach (var file in files)
            {
                if (exit)
                {
                    return;
                }

                var comparableImage = new ComparableImage(file);
                comparableImages.Add(comparableImage);
                index++;
                Invoke(updateOperationStatusDelegate, new object[] { "Processed images", workingLabel, workingProgressBar, index, operationStartTime });
            }

            Invoke(setMaximumDelegate, new object[] { workingProgressBar, (comparableImages.Count * (comparableImages.Count - 1)) / 2 });

            index = 0;

            var similarityImagesSorted = new List<SimilarityImages>();

            operationStartTime = DateTime.Now;

            for (var i = 0; i < comparableImages.Count - 1; i++)
            {
                for (var j = i + 1; j < comparableImages.Count; j++)
                {
                    if (exit)
                    {
                        return;
                    }

                    var source = comparableImages[i];
                    var destination = comparableImages[j];
                    var similarity = source.CalculateSimilarity(destination);
                    var sim = new SimilarityImages(source, destination, similarity);

                    similarityImagesSorted.Add(sim);
                    index++;

                    Invoke(updateOperationStatusDelegate, new object[] { "Compared images", workingLabel, workingProgressBar, index, operationStartTime });
                }
            }

            similarityImagesSorted.Sort();
            similarityImagesSorted.Reverse();
            similarityImages = new BindingList<SimilarityImages>(similarityImagesSorted);

            var buttons = 
                new List<Button>
                    {
                        findButton, 
                        cancelButton
                    };
            
            BeginInvoke(updateDataGridViewDelegate, new object[] { similarityImages, imagesDataGridView, buttons, folderTextBox });
        }

        private void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            folderTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowSelectedImages()
        {
            if (imagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var sim = (SimilarityImages)imagesDataGridView.SelectedRows[0].DataBoundItem;

            var streamSource = new System.IO.FileStream(sim.Source.File.FullName, FileMode.Open, FileAccess.Read);
            var streamDestination = new System.IO.FileStream(sim.Destination.File.FullName, FileMode.Open, FileAccess.Read);
            var source = Image.FromStream(streamSource);
            var destination = Image.FromStream(streamDestination);
            streamSource.Close();
            streamDestination.Close();

            const string InfoFormat = "Resolution: {0}x{1}\nSize: {2}kb\nFull path: {3}";
            const string ResolutionFormat = "{0} ({1}x{2})";

            sourcePictureBox.Image = source;
            mainToolTip.SetToolTip(sourcePictureBox, string.Format(InfoFormat, source.Width, source.Height, Math.Round((double)(sim.Source.File.Length / 1024), 1), sim.Source.File.FullName));
            sourceLabel.Text = string.Format(ResolutionFormat, "Source", source.Width, source.Height);

            destinationPictureBox.Image = destination;
            mainToolTip.SetToolTip(destinationPictureBox, string.Format(InfoFormat, destination.Width, destination.Height, Math.Round((double)(sim.Destination.File.Length / 1024), 1), sim.Destination.File.FullName));
            destinationLabel.Text = string.Format(ResolutionFormat, "Destination", destination.Width, destination.Height);

            ShowProjections(sourceGraph, sim.Source);
            ShowProjections(destinationGraph, sim.Destination);
        }

        private void DeleteSourceLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (imagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var result = MessageBox.Show("Delete the image?", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            var sim = (SimilarityImages)imagesDataGridView.SelectedRows[0].DataBoundItem;
            DeleteImageDelegate deleteImageDelegate = DeleteImage;
            imagesDataGridView.SuspendLayout();
            imagesDataGridView.Enabled = false;
            deleteImageDelegate.BeginInvoke(sim.Source.File, ShowDataGrid, null);
        }

        private void ShowDataGrid(IAsyncResult result)
        {
            Invoke(showGridDelegate, imagesDataGridView);
        }

        private void DeleteDestinationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (imagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var result = MessageBox.Show("Delete the image?", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            var sim = (SimilarityImages)imagesDataGridView.SelectedRows[0].DataBoundItem;
            var deleteImageDelegate = new DeleteImageDelegate(DeleteImage);
            imagesDataGridView.SuspendLayout();
            imagesDataGridView.Enabled = false;
            deleteImageDelegate.BeginInvoke(sim.Destination.File, ShowDataGrid, null);
        }

        private void DeleteImage(FileInfo fileInfo)
        {
            try
            {
                var toDelete = new List<SimilarityImages>();
                
                for (var index = 0; index < similarityImages.Count; index++)
                {
                    var item = similarityImages[index];
                    
                    if (item.Source.File.FullName.Equals(fileInfo.FullName, StringComparison.InvariantCultureIgnoreCase) || item.Destination.File.FullName.Equals(fileInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        toDelete.Add(item);
                    }
                }

                similarityImages.RaiseListChangedEvents = false;
                
                for (var index = 0; index < toDelete.Count; index++)
                {
                    var item = toDelete[index];
                    similarityImages.Remove(item);
                }

                similarityImages.RaiseListChangedEvents = true;
                similarityImages.ResetBindings();
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenSourceLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (imagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var sim = (SimilarityImages)imagesDataGridView.SelectedRows[0].DataBoundItem;
            Process.Start(sim.Source.File.FullName);
        }

        private void OpenDestinationLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (imagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var sim = (SimilarityImages)imagesDataGridView.SelectedRows[0].DataBoundItem;
            Process.Start(sim.Destination.File.FullName);
        }

        private void DataGridViewSelectionChanged(object sender, EventArgs e)
        {
            ShowSelectedImages();
        }
    }
}