namespace KMeans
{
    partial class frmMain1
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecision = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnBrowerQuery = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lvImageSimilar = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picQuery = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvClusters = new System.Windows.Forms.ListView();
            this.btnBrower = new System.Windows.Forms.Button();
            this.btnCluster = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIterations = new System.Windows.Forms.NumericUpDown();
            this.txtNumClusters = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picProcessed = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.ofdCluster = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofdImageQuery = new System.Windows.Forms.OpenFileDialog();
            this.lvImageDB = new System.Windows.Forms.ListView();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecision)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQuery)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumClusters)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Controls.Add(this.tabPage3);
            this.tabMain.Location = new System.Drawing.Point(2, 2);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1040, 522);
            this.tabMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtPrecision);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.btnBrowerQuery);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1032, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Query Image";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(28, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Độ chính xác";
            // 
            // txtPrecision
            // 
            this.txtPrecision.Location = new System.Drawing.Point(32, 362);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Size = new System.Drawing.Size(120, 20);
            this.txtPrecision.TabIndex = 5;
            this.txtPrecision.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(32, 403);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm:";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnBrowerQuery
            // 
            this.btnBrowerQuery.Location = new System.Drawing.Point(32, 263);
            this.btnBrowerQuery.Name = "btnBrowerQuery";
            this.btnBrowerQuery.Size = new System.Drawing.Size(85, 28);
            this.btnBrowerQuery.TabIndex = 3;
            this.btnBrowerQuery.Text = "Duyệt ảnh:";
            this.btnBrowerQuery.UseVisualStyleBackColor = true;
            this.btnBrowerQuery.Click += new System.EventHandler(this.btnBrowerQuery_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lvImageSimilar);
            this.groupBox6.Location = new System.Drawing.Point(400, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(626, 484);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Kết quả tìm kiếm";
            // 
            // lvImageSimilar
            // 
            this.lvImageSimilar.Location = new System.Drawing.Point(15, 19);
            this.lvImageSimilar.Name = "lvImageSimilar";
            this.lvImageSimilar.Size = new System.Drawing.Size(605, 448);
            this.lvImageSimilar.TabIndex = 0;
            this.lvImageSimilar.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.picQuery);
            this.groupBox4.Location = new System.Drawing.Point(32, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 232);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ảnh truy vấn";
            // 
            // picQuery
            // 
            this.picQuery.Location = new System.Drawing.Point(6, 13);
            this.picQuery.Name = "picQuery";
            this.picQuery.Size = new System.Drawing.Size(308, 213);
            this.picQuery.TabIndex = 0;
            this.picQuery.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvImageDB);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1032, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Image Database";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtImagePath);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.btnBrower);
            this.tabPage3.Controls.Add(this.btnCluster);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtIterations);
            this.tabPage3.Controls.Add(this.txtNumClusters);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1032, 496);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Phân cụm";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(55, 235);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(694, 20);
            this.txtImagePath.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvClusters);
            this.groupBox3.Location = new System.Drawing.Point(13, 266);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1013, 224);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kết quả phân cụm";
            // 
            // lvClusters
            // 
            this.lvClusters.CheckBoxes = true;
            this.lvClusters.Location = new System.Drawing.Point(6, 19);
            this.lvClusters.Name = "lvClusters";
            this.lvClusters.Size = new System.Drawing.Size(989, 199);
            this.lvClusters.TabIndex = 0;
            this.lvClusters.UseCompatibleStateImageBehavior = false;
            // 
            // btnBrower
            // 
            this.btnBrower.Location = new System.Drawing.Point(779, 231);
            this.btnBrower.Name = "btnBrower";
            this.btnBrower.Size = new System.Drawing.Size(102, 29);
            this.btnBrower.TabIndex = 5;
            this.btnBrower.Text = "Duyệt ảnh";
            this.btnBrower.UseVisualStyleBackColor = true;
            this.btnBrower.Click += new System.EventHandler(this.btnBrower_Click);
            // 
            // btnCluster
            // 
            this.btnCluster.Location = new System.Drawing.Point(433, 132);
            this.btnCluster.Name = "btnCluster";
            this.btnCluster.Size = new System.Drawing.Size(102, 36);
            this.btnCluster.TabIndex = 4;
            this.btnCluster.Text = "Phân cụm";
            this.btnCluster.UseVisualStyleBackColor = true;
            this.btnCluster.Click += new System.EventHandler(this.btnCluster_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số lần lặp:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Số cụm:";
            // 
            // txtIterations
            // 
            this.txtIterations.Location = new System.Drawing.Point(439, 61);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(61, 20);
            this.txtIterations.TabIndex = 2;
            this.txtIterations.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(439, 33);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(61, 20);
            this.txtNumClusters.TabIndex = 2;
            this.txtNumClusters.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picProcessed);
            this.groupBox2.Location = new System.Drawing.Point(673, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 218);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ảnh phân cụm";
            // 
            // picProcessed
            // 
            this.picProcessed.Location = new System.Drawing.Point(14, 21);
            this.picProcessed.Name = "picProcessed";
            this.picProcessed.Size = new System.Drawing.Size(258, 177);
            this.picProcessed.TabIndex = 1;
            this.picProcessed.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picPreview);
            this.groupBox1.Location = new System.Drawing.Point(31, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 218);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ảnh ban đầu";
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(9, 20);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(258, 177);
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // ofdCluster
            // 
            this.ofdCluster.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1054, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "Iterations:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel2.Text = "Duration:";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.MediumOrchid;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(58, 17);
            this.toolStripStatusLabel3.Text = "Precision:";
            // 
            // ofdImageQuery
            // 
            this.ofdImageQuery.FileName = "openFileDialog1";
            // 
            // lvImageDB
            // 
            this.lvImageDB.Location = new System.Drawing.Point(6, 3);
            this.lvImageDB.Name = "lvImageDB";
            this.lvImageDB.Size = new System.Drawing.Size(1020, 475);
            this.lvImageDB.TabIndex = 0;
            this.lvImageDB.UseCompatibleStateImageBehavior = false;
            // 
            // frmMain1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 558);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabMain);
            this.Name = "frmMain1";
            this.Text = "Tra cứu ảnh theo thuật toán Kmeans";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain1_FormClosing);
            this.Load += new System.EventHandler(this.frmMain1_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrecision)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQuery)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumClusters)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtNumClusters;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCluster;
        private System.Windows.Forms.OpenFileDialog ofdCluster;
        private System.Windows.Forms.PictureBox picProcessed;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnBrower;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvClusters;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtIterations;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox picQuery;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListView lvImageSimilar;
        private System.Windows.Forms.Button btnBrowerQuery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.OpenFileDialog ofdImageQuery;
        private System.Windows.Forms.NumericUpDown txtPrecision;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvImageDB;

    }
}