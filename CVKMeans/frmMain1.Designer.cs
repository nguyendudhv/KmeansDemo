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
            this.components = new System.ComponentModel.Container();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvClusters = new System.Windows.Forms.ListView();
            this.btnBrower = new System.Windows.Forms.Button();
            this.btnCluster = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumClusters = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picProcessed = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.ofdCluster = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSaveDB = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumClusters)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1032, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Query Image";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
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
            this.tabPage3.Controls.Add(this.btnSaveDB);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.btnBrower);
            this.tabPage3.Controls.Add(this.btnCluster);
            this.tabPage3.Controls.Add(this.label1);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvClusters);
            this.groupBox3.Location = new System.Drawing.Point(13, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1013, 254);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kết quả phân cụm";
            // 
            // lvClusters
            // 
            this.lvClusters.CheckBoxes = true;
            this.lvClusters.Location = new System.Drawing.Point(6, 19);
            this.lvClusters.Name = "lvClusters";
            this.lvClusters.Size = new System.Drawing.Size(989, 229);
            this.lvClusters.TabIndex = 0;
            this.lvClusters.UseCompatibleStateImageBehavior = false;
            // 
            // btnBrower
            // 
            this.btnBrower.Location = new System.Drawing.Point(449, 27);
            this.btnBrower.Name = "btnBrower";
            this.btnBrower.Size = new System.Drawing.Size(102, 36);
            this.btnBrower.TabIndex = 5;
            this.btnBrower.Text = "Duyệt ảnh";
            this.btnBrower.UseVisualStyleBackColor = true;
            this.btnBrower.Click += new System.EventHandler(this.btnBrower_Click);
            // 
            // btnCluster
            // 
            this.btnCluster.Location = new System.Drawing.Point(449, 138);
            this.btnCluster.Name = "btnCluster";
            this.btnCluster.Size = new System.Drawing.Size(102, 36);
            this.btnCluster.TabIndex = 4;
            this.btnCluster.Text = "Phân cụm";
            this.btnCluster.UseVisualStyleBackColor = true;
            this.btnCluster.Click += new System.EventHandler(this.btnCluster_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Số cụm:";
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(449, 104);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(61, 20);
            this.txtNumClusters.TabIndex = 2;
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
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSaveDB
            // 
            this.btnSaveDB.Location = new System.Drawing.Point(449, 194);
            this.btnSaveDB.Name = "btnSaveDB";
            this.btnSaveDB.Size = new System.Drawing.Size(102, 36);
            this.btnSaveDB.TabIndex = 7;
            this.btnSaveDB.Text = "Lưu vào database";
            this.btnSaveDB.UseVisualStyleBackColor = true;
            this.btnSaveDB.Click += new System.EventHandler(this.btnSaveDB_Click);
            // 
            // frmMain1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 527);
            this.Controls.Add(this.tabMain);
            this.Name = "frmMain1";
            this.Text = "Tra cứu ảnh theo thuật toán Kmeans";
            this.tabMain.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumClusters)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnBrower;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvClusters;
        private System.Windows.Forms.Button btnSaveDB;

    }
}