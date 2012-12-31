namespace EyeOpen.SimilarImagesFinder.Windows.Forms
{
    internal partial class MainForm
    {
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.Windows.Forms.DataGridView imagesDataGridView;
        private System.Windows.Forms.Label imagesLabel;
        private System.Windows.Forms.ProgressBar workingProgressBar;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.PictureBox destinationPictureBox;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.PictureBox sourcePictureBox;
        private System.Windows.Forms.Label workingLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel showFoldersLinkLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.LinkLabel deleteDestinationLinkLabel;
        private System.Windows.Forms.LinkLabel deleteSourceLinkLabel;
        private System.Windows.Forms.LinkLabel openSourceLinkLabel;
        private System.Windows.Forms.LinkLabel openDestinationLinkLabel;
        private Graph sourceGraph;
        private Graph destinationGraph;

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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.findButton = new System.Windows.Forms.Button();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imagesDataGridView = new System.Windows.Forms.DataGridView();
            this.imagesLabel = new System.Windows.Forms.Label();
            this.workingProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel = new System.Windows.Forms.Panel();
            this.openDestinationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.openSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.deleteDestinationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.deleteSourceLinkLabel = new System.Windows.Forms.LinkLabel();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.destinationPictureBox = new System.Windows.Forms.PictureBox();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.workingLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.showFoldersLinkLabel = new System.Windows.Forms.LinkLabel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceGraph = new Graph();
            this.destinationGraph = new Graph();
            ((System.ComponentModel.ISupportInitialize)(this.imagesDataGridView)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.findButton.Location = new System.Drawing.Point(6, 40);
            this.findButton.Name = "btnFind";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 0;
            this.findButton.Text = "Find similar";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.FindButtonClick);
            // 
            // txtFolder
            // 
            this.folderTextBox.Location = new System.Drawing.Point(186, 43);
            this.folderTextBox.Name = "txtFolder";
            this.folderTextBox.Size = new System.Drawing.Size(554, 20);
            this.folderTextBox.TabIndex = 1;
            this.folderTextBox.Text = "C:\\";
            // 
            // dgvImages
            // 
            this.imagesDataGridView.AllowUserToAddRows = false;
            this.imagesDataGridView.AllowUserToDeleteRows = false;
            this.imagesDataGridView.AllowUserToResizeRows = false;
            this.imagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imagesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.imagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imagesDataGridView.Location = new System.Drawing.Point(9, 392);
            this.imagesDataGridView.MultiSelect = false;
            this.imagesDataGridView.Name = "dgvImages";
            this.imagesDataGridView.ReadOnly = true;
            this.imagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.imagesDataGridView.Size = new System.Drawing.Size(835, 222);
            this.imagesDataGridView.TabIndex = 12;
            this.imagesDataGridView.SelectionChanged += new System.EventHandler(this.DataGridViewSelectionChanged);
            // 
            // lblImages
            // 
            this.imagesLabel.AutoSize = true;
            this.imagesLabel.Location = new System.Drawing.Point(6, 376);
            this.imagesLabel.Name = "lblImages";
            this.imagesLabel.Size = new System.Drawing.Size(41, 13);
            this.imagesLabel.TabIndex = 13;
            this.imagesLabel.Text = "Images";
            // 
            // progressBar1
            // 
            this.workingProgressBar.Location = new System.Drawing.Point(6, 98);
            this.workingProgressBar.Name = "progressBar1";
            this.workingProgressBar.Size = new System.Drawing.Size(834, 23);
            this.workingProgressBar.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel.Controls.Add(this.destinationGraph);
            this.panel.Controls.Add(this.sourceGraph);
            this.panel.Controls.Add(this.openDestinationLinkLabel);
            this.panel.Controls.Add(this.openSourceLinkLabel);
            this.panel.Controls.Add(this.deleteDestinationLinkLabel);
            this.panel.Controls.Add(this.deleteSourceLinkLabel);
            this.panel.Controls.Add(this.destinationLabel);
            this.panel.Controls.Add(this.destinationPictureBox);
            this.panel.Controls.Add(this.sourceLabel);
            this.panel.Controls.Add(this.sourcePictureBox);
            this.panel.Location = new System.Drawing.Point(6, 131);
            this.panel.Name = "panel1";
            this.panel.Size = new System.Drawing.Size(834, 241);
            this.panel.TabIndex = 15;
            // 
            // lklOpenDestination
            // 
            this.openDestinationLinkLabel.AutoSize = true;
            this.openDestinationLinkLabel.Location = new System.Drawing.Point(217, 222);
            this.openDestinationLinkLabel.Name = "lklOpenDestination";
            this.openDestinationLinkLabel.Size = new System.Drawing.Size(33, 13);
            this.openDestinationLinkLabel.TabIndex = 24;
            this.openDestinationLinkLabel.TabStop = true;
            this.openDestinationLinkLabel.Text = "Open";
            this.openDestinationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenDestinationLinkLabelLinkClicked);
            // 
            // lklOpenSource
            // 
            this.openSourceLinkLabel.AutoSize = true;
            this.openSourceLinkLabel.Location = new System.Drawing.Point(6, 222);
            this.openSourceLinkLabel.Name = "lklOpenSource";
            this.openSourceLinkLabel.Size = new System.Drawing.Size(33, 13);
            this.openSourceLinkLabel.TabIndex = 23;
            this.openSourceLinkLabel.TabStop = true;
            this.openSourceLinkLabel.Text = "Open";
            this.openSourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenSourceLinkLabelLinkClicked);
            // 
            // lklDeleteDestination
            // 
            this.deleteDestinationLinkLabel.AutoSize = true;
            this.deleteDestinationLinkLabel.Location = new System.Drawing.Point(256, 222);
            this.deleteDestinationLinkLabel.Name = "lklDeleteDestination";
            this.deleteDestinationLinkLabel.Size = new System.Drawing.Size(38, 13);
            this.deleteDestinationLinkLabel.TabIndex = 22;
            this.deleteDestinationLinkLabel.TabStop = true;
            this.deleteDestinationLinkLabel.Text = "Delete";
            this.deleteDestinationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteDestinationLinkLabel_LinkClicked);
            // 
            // lklDeleteSource
            // 
            this.deleteSourceLinkLabel.AutoSize = true;
            this.deleteSourceLinkLabel.Location = new System.Drawing.Point(45, 222);
            this.deleteSourceLinkLabel.Name = "lklDeleteSource";
            this.deleteSourceLinkLabel.Size = new System.Drawing.Size(38, 13);
            this.deleteSourceLinkLabel.TabIndex = 21;
            this.deleteSourceLinkLabel.TabStop = true;
            this.deleteSourceLinkLabel.Text = "Delete";
            this.deleteSourceLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeleteSourceLinkLabelLinkClicked);
            // 
            // lblDestination
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(217, 4);
            this.destinationLabel.Name = "lblDestination";
            this.destinationLabel.Size = new System.Drawing.Size(60, 13);
            this.destinationLabel.TabIndex = 15;
            this.destinationLabel.Text = "Destination";
            // 
            // pcbDestination
            // 
            this.destinationPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationPictureBox.Location = new System.Drawing.Point(220, 20);
            this.destinationPictureBox.Name = "pcbDestination";
            this.destinationPictureBox.Size = new System.Drawing.Size(180, 199);
            this.destinationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.destinationPictureBox.TabIndex = 14;
            this.destinationPictureBox.TabStop = false;
            // 
            // lblSource
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(6, 4);
            this.sourceLabel.Name = "lblSource";
            this.sourceLabel.Size = new System.Drawing.Size(41, 13);
            this.sourceLabel.TabIndex = 13;
            this.sourceLabel.Text = "Source";
            // 
            // pcbSource
            // 
            this.sourcePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourcePictureBox.Location = new System.Drawing.Point(9, 20);
            this.sourcePictureBox.Name = "pcbSource";
            this.sourcePictureBox.Size = new System.Drawing.Size(180, 199);
            this.sourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sourcePictureBox.TabIndex = 12;
            this.sourcePictureBox.TabStop = false;
            // 
            // lblWorking
            // 
            this.workingLabel.AutoSize = true;
            this.workingLabel.Location = new System.Drawing.Point(3, 82);
            this.workingLabel.Name = "lblWorking";
            this.workingLabel.Size = new System.Drawing.Size(113, 13);
            this.workingLabel.TabIndex = 16;
            this.workingLabel.Text = "Processed images 0/0";
            // 
            // lklShowFolders
            // 
            this.showFoldersLinkLabel.AutoSize = true;
            this.showFoldersLinkLabel.Location = new System.Drawing.Point(746, 50);
            this.showFoldersLinkLabel.Name = "lklShowFolders";
            this.showFoldersLinkLabel.Size = new System.Drawing.Size(16, 13);
            this.showFoldersLinkLabel.TabIndex = 17;
            this.showFoldersLinkLabel.TabStop = true;
            this.showFoldersLinkLabel.Text = "...";
            this.showFoldersLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // btnCancel
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(87, 40);
            this.cancelButton.Name = "btnCancel";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // mnsMain
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mnsMain";
            this.mainMenuStrip.Size = new System.Drawing.Size(857, 24);
            this.mainMenuStrip.TabIndex = 19;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // graph1
            // 
            this.sourceGraph.BackColor = System.Drawing.Color.White;
            this.sourceGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourceGraph.Location = new System.Drawing.Point(441, 20);
            this.sourceGraph.Name = "graph1";
            this.sourceGraph.Size = new System.Drawing.Size(180, 199);
            this.sourceGraph.TabIndex = 20;
            this.sourceGraph.TabStop = false;
            // 
            // graph2
            // 
            this.destinationGraph.BackColor = System.Drawing.Color.White;
            this.destinationGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationGraph.Location = new System.Drawing.Point(640, 20);
            this.destinationGraph.Name = "graph2";
            this.destinationGraph.Size = new System.Drawing.Size(180, 199);
            this.destinationGraph.TabIndex = 25;
            this.destinationGraph.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 626);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.showFoldersLinkLabel);
            this.Controls.Add(this.workingLabel);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.workingProgressBar);
            this.Controls.Add(this.imagesLabel);
            this.Controls.Add(this.imagesDataGridView);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(873, 662);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimilarSearch - EyeOpen";
            ((System.ComponentModel.ISupportInitialize)(this.imagesDataGridView)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.destinationGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
