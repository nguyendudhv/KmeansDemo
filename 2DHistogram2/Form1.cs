using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Data;
using _2DHistogram;
using AVI_Loader;



//Do not remove (C) lines
//(C) Georgi Kostadinov Petrov
//(C) 2006 gpetrov@nbu.bg
namespace _2DHistogram
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	struct MatrixSize
	{
        int x;
		int y;
	};
	public class Form1 : System.Windows.Forms.Form
	{
        //AVI File manipulation
        public AVI_Loader.AVI avi = new AVI_Loader.AVI();
        public AVI_Loader.AVIReader avi_reader = new AVI_Loader.AVIReader();
        public AVI_Loader.AVIWriter avi_writer = new AVI_Loader.AVIWriter();

        private System.Windows.Forms.OpenFileDialog openAVI = new OpenFileDialog();
        //AVI file manipulation

        public frmPic frmShowPicture;

		public Bitmap bmpLoaded = null;
		public Bitmap histBitmap = null;
		//matrix for Grey level 
		public static byte [,] Grey;
        public static byte[,] GreyTmp;
        public int[,,] H3D; 
		public byte [,] R;
		public byte [,] G;
		public byte [,] B;

        int HiMean;
        int HiSigma;
        int bmpX;
        int bmpY;

        int [] EMaxPos = new int[8];

        //Hold the Entropy maximum position
        int EiPosMax;
        double ÅiGreyMax;
		//This is Grey image histogram array
		public int [,] HiGrey = new int[2,256];
		int HiGreyMax = 0;
		public double [] HiGreyN = new double[256];
        public double[] EiGrey = new double[256];
		MatrixSize matrixsize = new MatrixSize();
		
		/// <summary>
		///
		/// </summary>

		private System.Windows.Forms.Button LoadImage;
		private System.Windows.Forms.PictureBox PicBox;
		private System.Windows.Forms.PictureBox PicHist;
		private System.Windows.Forms.Button Test;
		private System.Windows.Forms.Button SaveImage;
		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Label lblHist;
        private GroupBox groupHE3D;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button btnFilter;
        private Button btnSaveHistogram;
        private Button btnHomogeniza;
        private Button btnSqave3DH;
        private CheckBox ch3DColor;
        private Button btnHEq2D;
        private Button btnEMax;
        private Button btnEMax8;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnLoadAvi;
        private Button btn2;
        private Button btnEntropy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.Button btnSave2DH;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LoadImage = new System.Windows.Forms.Button();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.PicHist = new System.Windows.Forms.PictureBox();
            this.Test = new System.Windows.Forms.Button();
            this.SaveImage = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblHist = new System.Windows.Forms.Label();
            this.groupHE3D = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnSaveHistogram = new System.Windows.Forms.Button();
            this.btnHomogeniza = new System.Windows.Forms.Button();
            this.btnSqave3DH = new System.Windows.Forms.Button();
            this.ch3DColor = new System.Windows.Forms.CheckBox();
            this.btnHEq2D = new System.Windows.Forms.Button();
            this.btnEMax = new System.Windows.Forms.Button();
            this.btnEMax8 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnLoadAvi = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btnEntropy = new System.Windows.Forms.Button();
            btnSave2DH = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicHist)).BeginInit();
            this.groupHE3D.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave2DH
            // 
            btnSave2DH.Location = new System.Drawing.Point(555, 443);
            btnSave2DH.Name = "btnSave2DH";
            btnSave2DH.Size = new System.Drawing.Size(135, 25);
            btnSave2DH.TabIndex = 12;
            btnSave2DH.Text = "Save 2D Histogram TXT";
            btnSave2DH.Click += new System.EventHandler(this.btnSave2DH_Click);
            // 
            // LoadImage
            // 
            this.LoadImage.Location = new System.Drawing.Point(8, 8);
            this.LoadImage.Name = "LoadImage";
            this.LoadImage.Size = new System.Drawing.Size(96, 25);
            this.LoadImage.TabIndex = 0;
            this.LoadImage.Text = "Load Image";
            this.LoadImage.Click += new System.EventHandler(this.LoadImage_Click);
            // 
            // PicBox
            // 
            this.PicBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBox.Location = new System.Drawing.Point(8, 105);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(429, 389);
            this.PicBox.TabIndex = 1;
            this.PicBox.TabStop = false;
            this.PicBox.Click += new System.EventHandler(this.PicBox_Click);
            // 
            // PicHist
            // 
            this.PicHist.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PicHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicHist.Location = new System.Drawing.Point(443, 148);
            this.PicHist.Name = "PicHist";
            this.PicHist.Size = new System.Drawing.Size(259, 260);
            this.PicHist.TabIndex = 2;
            this.PicHist.TabStop = false;
            this.PicHist.Click += new System.EventHandler(this.PicHist_Click);
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(306, 8);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(102, 25);
            this.Test.TabIndex = 3;
            this.Test.Text = "3D Hist";
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Location = new System.Drawing.Point(120, 8);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(96, 25);
            this.SaveImage.TabIndex = 4;
            this.SaveImage.Text = "Save Image";
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // lbl1
            // 
            this.lbl1.Location = new System.Drawing.Point(10, 62);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(290, 37);
            this.lbl1.TabIndex = 5;
            this.lbl1.Text = "Loaded Image";
            // 
            // lblHist
            // 
            this.lblHist.Location = new System.Drawing.Point(443, 109);
            this.lblHist.Name = "lblHist";
            this.lblHist.Size = new System.Drawing.Size(259, 33);
            this.lblHist.TabIndex = 6;
            this.lblHist.Text = "2D Image Histogram";
            // 
            // groupHE3D
            // 
            this.groupHE3D.Controls.Add(this.radioButton2);
            this.groupHE3D.Controls.Add(this.radioButton1);
            this.groupHE3D.Location = new System.Drawing.Point(596, 1);
            this.groupHE3D.Name = "groupHE3D";
            this.groupHE3D.Size = new System.Drawing.Size(157, 43);
            this.groupHE3D.TabIndex = 7;
            this.groupHE3D.TabStop = false;
            this.groupHE3D.Text = "Select Statistic";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(84, 15);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Entropy";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Histogram";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(414, 8);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(78, 25);
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Median Filter";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnSaveHistogram
            // 
            this.btnSaveHistogram.Location = new System.Drawing.Point(555, 414);
            this.btnSaveHistogram.Name = "btnSaveHistogram";
            this.btnSaveHistogram.Size = new System.Drawing.Size(135, 25);
            this.btnSaveHistogram.TabIndex = 10;
            this.btnSaveHistogram.Text = "Save Histogram Bitmap";
            this.btnSaveHistogram.Click += new System.EventHandler(this.btnSaveHistogram_Click);
            // 
            // btnHomogeniza
            // 
            this.btnHomogeniza.Location = new System.Drawing.Point(414, 62);
            this.btnHomogeniza.Name = "btnHomogeniza";
            this.btnHomogeniza.Size = new System.Drawing.Size(78, 25);
            this.btnHomogeniza.TabIndex = 11;
            this.btnHomogeniza.Text = "Homogenize";
            this.btnHomogeniza.Click += new System.EventHandler(this.btnHomogeniza_Click);
            // 
            // btnSqave3DH
            // 
            this.btnSqave3DH.Location = new System.Drawing.Point(555, 474);
            this.btnSqave3DH.Name = "btnSqave3DH";
            this.btnSqave3DH.Size = new System.Drawing.Size(135, 25);
            this.btnSqave3DH.TabIndex = 13;
            this.btnSqave3DH.Text = "Save 3D Histogram TXT";
            this.btnSqave3DH.Click += new System.EventHandler(this.btnSqave3DH_Click);
            // 
            // ch3DColor
            // 
            this.ch3DColor.AutoSize = true;
            this.ch3DColor.Location = new System.Drawing.Point(234, 8);
            this.ch3DColor.Name = "ch3DColor";
            this.ch3DColor.Size = new System.Drawing.Size(66, 17);
            this.ch3DColor.TabIndex = 14;
            this.ch3DColor.Text = "3D color";
            this.ch3DColor.UseVisualStyleBackColor = true;
            this.ch3DColor.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnHEq2D
            // 
            this.btnHEq2D.Location = new System.Drawing.Point(306, 62);
            this.btnHEq2D.Name = "btnHEq2D";
            this.btnHEq2D.Size = new System.Drawing.Size(102, 25);
            this.btnHEq2D.TabIndex = 15;
            this.btnHEq2D.Text = "2D H Ecqulize";
            this.btnHEq2D.Click += new System.EventHandler(this.btnHEq2D_Click);
            // 
            // btnEMax
            // 
            this.btnEMax.Location = new System.Drawing.Point(618, 50);
            this.btnEMax.Name = "btnEMax";
            this.btnEMax.Size = new System.Drawing.Size(67, 25);
            this.btnEMax.TabIndex = 16;
            this.btnEMax.Text = "Entropy 4 MAX";
            this.btnEMax.Click += new System.EventHandler(this.btnEMax_Click);
            // 
            // btnEMax8
            // 
            this.btnEMax8.Location = new System.Drawing.Point(691, 50);
            this.btnEMax8.Name = "btnEMax8";
            this.btnEMax8.Size = new System.Drawing.Size(62, 25);
            this.btnEMax8.TabIndex = 17;
            this.btnEMax8.Text = "Entropy 8 MAX";
            this.btnEMax8.Click += new System.EventHandler(this.btnEMax8_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 25);
            this.button1.TabIndex = 18;
            this.button1.Text = "4";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(691, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 25);
            this.button2.TabIndex = 19;
            this.button2.Text = "8";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(306, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 25);
            this.button3.TabIndex = 20;
            this.button3.Text = "3D Hi to BMP";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnLoadAvi
            // 
            this.btnLoadAvi.Location = new System.Drawing.Point(8, 35);
            this.btnLoadAvi.Name = "btnLoadAvi";
            this.btnLoadAvi.Size = new System.Drawing.Size(96, 25);
            this.btnLoadAvi.TabIndex = 21;
            this.btnLoadAvi.Text = "Load Avi";
            this.btnLoadAvi.Click += new System.EventHandler(this.btnLoadAvi_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(545, 81);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(67, 25);
            this.btn2.TabIndex = 23;
            this.btn2.Text = "2";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btnEntropy
            // 
            this.btnEntropy.Location = new System.Drawing.Point(545, 50);
            this.btnEntropy.Name = "btnEntropy";
            this.btnEntropy.Size = new System.Drawing.Size(67, 25);
            this.btnEntropy.TabIndex = 22;
            this.btnEntropy.Text = "Entropy 1 MAX";
            this.btnEntropy.Click += new System.EventHandler(this.btnEntropy_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(763, 506);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btnEntropy);
            this.Controls.Add(this.btnLoadAvi);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEMax8);
            this.Controls.Add(this.btnEMax);
            this.Controls.Add(this.btnHEq2D);
            this.Controls.Add(this.ch3DColor);
            this.Controls.Add(this.btnSqave3DH);
            this.Controls.Add(btnSave2DH);
            this.Controls.Add(this.btnHomogeniza);
            this.Controls.Add(this.btnSaveHistogram);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.groupHE3D);
            this.Controls.Add(this.lblHist);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.PicHist);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.LoadImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "2D Image Histogram calculation and normalization";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicHist)).EndInit();
            this.groupHE3D.ResumeLayout(false);
            this.groupHE3D.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            H3D = new int[2,256, 256];
		}
		

		private void LoadImage_Click(object sender, System.EventArgs e)
		{
			
			//This function opens OpenFileDialog and loads BMP or JPG file
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Title = "Open Picture";
			dlg.Filter = "bmp files =(*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				//Load picture in Bitmap
				//   PicBox.Image = Image.FromFile(dlg.FileName);
				//Resize loaded image depending on PicBox fixed dimentions
				PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
				//	using (Bitmap bmp = new Bitmap(dlg.FileName))
			{
				//bmp holds a loaded picture
				if(bmpLoaded!=null)
					bmpLoaded.Dispose();
				bmpLoaded = new Bitmap(dlg.FileName);
				lbl1.Text = dlg.FileName;
				//Resize color matrixs
				int y = bmpLoaded.Height;
				int x = bmpLoaded.Width;
				ResizeColorMatrix(y,x);
				//This function copyes color data in to RGB and grey color matrix
				BitmapToMatrix(bmpLoaded);
				
				//Mode filter
			//	DSP3x3(3,y,x);
				//This function calculates 2D histogram
				//From already loaded GreyLevel array
				//GreyLevelHistogram(y,x);
                DrawHistogram();
				

				bmpLoaded = new Bitmap(GreyMatrixToBitmap(y,x));

				//	bmpLoaded = new Bitmap(RedMatrixToBitmap(y,x));
				//	bmpLoaded = new Bitmap(GreenMatrixToBitmap(y,x));
				//	bmpLoaded = new Bitmap(BlueMatrixToBitmap(y,x));
				if(PicBox.Image!=null)
					PicBox.Image.Dispose();

				PicBox.Image = new Bitmap(bmpLoaded);

			}
			}
			dlg.Dispose();
		}
		public void SaveBitmap(Bitmap bmp1)
		{
			Bitmap bmp = new Bitmap(bmp1);
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg";
			dlg.DefaultExt = "bmp";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;
				string filename = dlg.FileName;
				if (Path.GetExtension(filename).ToLower() == ".bmp")
					bmp1.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
				else
					bmp1.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
				Cursor.Current = Cursors.Default;
			}

			bmp.Dispose();
		//	bmp1.Dispose();
		}
        public void SaveHistogram(int type)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt";
            dlg.DefaultExt = "txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string filename = dlg.FileName;
                

                if (type == 1)
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        if (Path.GetExtension(filename).ToLower() == ".txt")
                            for (int i = 0; i < 256; i++)
                            {
                                sw.WriteLine(HiGrey[0,i].ToString());
                            }
                        sw.Close();
                    }
                }
                
                string str = "";
                if (type == 2)
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        if (Path.GetExtension(filename).ToLower() == ".txt")
                           
                            for (int i = 0; i < 256; i++)
                            {
                                for (int j = 0; j < 256; j++)
                                {
                                    str = str + ";" + H3D[0,i,j].ToString();
                                }
                                sw.WriteLine(str);
                                str = "";
                            }
                        sw.Close();
                    }
                }

               Cursor.Current = Cursors.Default;
            }
        }

		public void ResizeColorMatrix(int y, int x)
		{
			//This code resizes 4 color pixel matrix
			//according to y x dimentions
            Grey = new byte[y, x];
			R = new byte[y, x];
			G = new byte[y, x];
			B = new byte[y, x];
		}

		public void BitmapToMatrix(Bitmap BMP)
		{
			//This function makes access to BMP byte matrix
			//And copy data inside R B B and grey color matrix
			//	Bitmap returnBmp = new Bitmap(BMP.Width, BMP.Height,
			//		PixelFormat.Format32bppArgb);
			BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
				BMP.Width, BMP.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			//	BitmapData bitmapData2 = returnBmp.LockBits(new Rectangle(0, 0,
			//		returnBmp.Width, returnBmp.Height),
			//		ImageLockMode.ReadOnly,
			//		PixelFormat.Format32bppArgb);
			int a = 0;
           
			unsafe
			{
				byte* imagePointer1 = (byte*)bitmapData1.Scan0;
				//	byte* imagePointer2 = (byte*)bitmapData2.Scan0;
				//Loop each row
				for (int i = 0; i < bitmapData1.Height; i++)
				{
					//Scan each pixel inside a row
					for (int j = 0; j < bitmapData1.Width; j++)
					{
						// Find the mean color RGB to grey level convertion
						R[i,j]= imagePointer1[0];
						G[i,j]= imagePointer1[1];
						B[i,j]= imagePointer1[2];

						a = (byte)(0.229*(double)imagePointer1[0] + 0.587*(double)imagePointer1[1] +
							0.114*(double)imagePointer1[2]);// / 3;

                        //a = (imagePointer1[0] + imagePointer1[1] +
                        //	imagePointer1[2])/ 3;
						//Load Grey level in to matrix
						Grey[i,j] = (byte)a;
						//B color channel
						//	imagePointer2[0] = (byte)a;
						//G color channel
						//	imagePointer2[1] = (byte)a;
						//R color channel
						//	imagePointer2[2] = (byte)a;
						//	imagePointer2[3] = imagePointer1[3];
						//1 pixel is consested by 4 bytes
						//we increment our pointer position
						//	imagePointer1 = imagePointer1 + 4;
						imagePointer1 += 4;
						//	imagePointer2 += 4;
					}//end for j
					//4 bytes per pixel * number of pixels in a row
					imagePointer1 += bitmapData1.Stride -
						(bitmapData1.Width * 4);
					//	imagePointer2 += bitmapData1.Stride -
					//		(bitmapData1.Width * 4);
				}//end for i
			}
			//end unsafe
			//	returnBmp.UnlockBits(bitmapData2);
			BMP.UnlockBits(bitmapData1);
			//	return returnBmp;

		}//end processImage

		public Bitmap GreyMatrixToBitmap(int y, int x)
		{
			//This function makes access to BMP byte matrix
			//And copy data inside R B B and grey color matrix
			Bitmap returnBmp = new Bitmap(x, y,
				PixelFormat.Format32bppArgb);
			
			BitmapData bitmapData2 = returnBmp.LockBits(new Rectangle(0, 0,
				returnBmp.Width, returnBmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			//	int a = 0;
           
			unsafe
			{
				;
				byte* imagePointer2 = (byte*)bitmapData2.Scan0;
				//Loop each row
				for (int i = 0; i < bitmapData2.Height; i++)
				{
					//Scan each pixel inside a row
					for (int j = 0; j < bitmapData2.Width; j++)
					{
						//B color channel
						imagePointer2[0] = Grey[i,j];
						//G color channel
						imagePointer2[1] = Grey[i,j];
						//R color channel
						imagePointer2[2] = Grey[i,j];
						imagePointer2[3] = 255;//imagePointer2[3];
						//1 pixel is consested by 4 bytes
						//we increment our pointer position
						//	imagePointer1 = imagePointer1 + 4;
						imagePointer2 += 4;
						//	imagePointer2 += 4;
					}//end for j
					//4 bytes per pixel * number of pixels in a row
					imagePointer2 += bitmapData2.Stride -
						(bitmapData2.Width * 4);
				}//end for i
			}
			//end unsafe
			returnBmp.UnlockBits(bitmapData2);
			return returnBmp;
		}//end processImage
		public Bitmap BlueMatrixToBitmap(int y, int x)
		{
			//This function makes access to BMP byte matrix
			//And copy data inside R B B and grey color matrix
			Bitmap returnBmp = new Bitmap(x, y,
				PixelFormat.Format32bppArgb);
			
			BitmapData bitmapData2 = returnBmp.LockBits(new Rectangle(0, 0,
				returnBmp.Width, returnBmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			//	int a = 0;
           
			unsafe
			{
				;
				byte* imagePointer2 = (byte*)bitmapData2.Scan0;
				//Loop each row
				for (int i = 0; i < bitmapData2.Height; i++)
				{
					//Scan each pixel inside a row
					for (int j = 0; j < bitmapData2.Width; j++)
					{
						//B color channel
						imagePointer2[0] = B[i,j];
						//G color channel
						imagePointer2[1] = 0;//Grey[i,j];
						//R color channel
						imagePointer2[2] = 0;//Grey[i,j];
						imagePointer2[3] = 255;//imagePointer2[3];
						//1 pixel is consested by 4 bytes
						//we increment our pointer position
						//	imagePointer1 = imagePointer1 + 4;
						imagePointer2 += 4;
						//	imagePointer2 += 4;
					}//end for j
					//4 bytes per pixel * number of pixels in a row
					imagePointer2 += bitmapData2.Stride -
						(bitmapData2.Width * 4);
				}//end for i
			}
			//end unsafe
			returnBmp.UnlockBits(bitmapData2);
			return returnBmp;
		}//end processImage
		public Bitmap GreenMatrixToBitmap(int y, int x)
		{
			//This function makes access to BMP byte matrix
			//And copy data inside R B B and grey color matrix
			Bitmap returnBmp = new Bitmap(x, y,
				PixelFormat.Format32bppArgb);
			
			BitmapData bitmapData2 = returnBmp.LockBits(new Rectangle(0, 0,
				returnBmp.Width, returnBmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			//	int a = 0;
           
			unsafe
			{
				;
				byte* imagePointer2 = (byte*)bitmapData2.Scan0;
				//Loop each row
				for (int i = 0; i < bitmapData2.Height; i++)
				{
					//Scan each pixel inside a row
					for (int j = 0; j < bitmapData2.Width; j++)
					{
						//B color channel
						imagePointer2[0] = 0;//Grey[i,j];
						//G color channel
						imagePointer2[1] = G[i,j];
						//R color channel
						imagePointer2[2] = 0;//Grey[i,j];
						imagePointer2[3] = 255;//imagePointer2[3];
						//1 pixel is consested by 4 bytes
						//we increment our pointer position
						//	imagePointer1 = imagePointer1 + 4;
						imagePointer2 += 4;
						//	imagePointer2 += 4;
					}//end for j
					//4 bytes per pixel * number of pixels in a row
					imagePointer2 += bitmapData2.Stride -
						(bitmapData2.Width * 4);
				}//end for i
			}
			//end unsafe
			returnBmp.UnlockBits(bitmapData2);
			return returnBmp;
		}//end processImage
		public Bitmap RedMatrixToBitmap(int y, int x)
		{
			//This function makes access to BMP byte matrix
			//And copy data inside R B B and grey color matrix
			Bitmap returnBmp = new Bitmap(x, y,
				PixelFormat.Format32bppArgb);
			
			BitmapData bitmapData2 = returnBmp.LockBits(new Rectangle(0, 0,
				returnBmp.Width, returnBmp.Height),
				ImageLockMode.ReadOnly,
				PixelFormat.Format32bppArgb);
			//	int a = 0;
           
			unsafe
			{
				;
				byte* imagePointer2 = (byte*)bitmapData2.Scan0;
				//Loop each row
				for (int i = 0; i < bitmapData2.Height; i++)
				{
					//Scan each pixel inside a row
					for (int j = 0; j < bitmapData2.Width; j++)
					{
						//B color channel
						imagePointer2[0] = 0;//R[i,j];
						//G color channel
						imagePointer2[1] = 0;//R[i,j];
						//R color channel
						imagePointer2[2] = R[i,j];
						imagePointer2[3] = 255;//imagePointer2[3];
						//1 pixel is consested by 4 bytes
						//we increment our pointer position
						//	imagePointer1 = imagePointer1 + 4;
						imagePointer2 += 4;
						//	imagePointer2 += 4;
					}//end for j
					//4 bytes per pixel * number of pixels in a row
					imagePointer2 += bitmapData2.Stride -
						(bitmapData2.Width * 4);
				}//end for i
			}
			//end unsafe
			returnBmp.UnlockBits(bitmapData2);
			return returnBmp;
		}//end processImage
		/*public void GreyLevelHistogram(int y,int x)
		{
			DrawHistogram();
		}
        */
		public void HiNormalize(int y, int x)
		{
			//This function normalizes the histogram
			//and holds the probability values inside
			//double precision array
			for(int i=0;i<256;i++)
			{
				HiGreyN[i] = (double)HiGrey[0,i]/(double)(x*y);
			}
		}
		public int HiMax()
		{
			int max=HiGrey[0,0];
			for(int i=0;i<256;i++)
			{
				if(max<HiGrey[0,i])
					max = HiGrey[0,i];
			}
			return max;
		}
        public double EiMax()
        {
            double max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (EiGrey[i] >= 0)
                {
                    if (max < EiGrey[i])
                        max = EiGrey[i];
                }
            }
            return max;
        }

        public int H3DMax()
        {
            int max = 0;
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if(max<H3D[0,i,j])
                        max = H3D[0,i, j];
                }
            }
            return max;
        }
        public double H3DLogMax()
        {
            double max = 0;
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (max < System.Math.Log(H3D[0,i, j]))
                        max = H3D[0,i, j];
                }
            }
            return max;
        }

        public int EiMaxPos()
        {
            int pos = 0;
            double max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (EiGrey[i] >= 0)
                {
                    if (max < EiGrey[i])
                    {
                        max = EiGrey[i];
                        pos = i;
                    }
                }
            }
            return pos;
        }

		public double HiNMax()
		{
			double max=HiGreyN[0];
			for(int i=0;i<256;i++)
			{
				if(max<HiGreyN[i])
					max = HiGreyN[i];
			}
			return max;
		}

		public void DSP3x3(int kernel, int y, int x)
		{
			float [,] filter_kernel = new float[3,3];
			filter_kernel[0,0] = -1*3;
			filter_kernel[0,1] = -1*3;
			filter_kernel[0,2] = -1*3;

			filter_kernel[1,0] = -1*3;
			filter_kernel[1,1] = 8*3;
			filter_kernel[1,2] = -1*3;

			filter_kernel[2,0] = -1*3;
			filter_kernel[2,1] = -1*3;
			filter_kernel[2,2] = -1*3;
			
			float mean = 0;
			byte mode=0;
			byte [] mod = new byte[256];

			int k_y = kernel;
			int k_x = kernel;
			for(int i=0;i<y-k_y;i++)
			{
				for(int j=0;j<x-k_x;j++)
				{
					mean = 0;

					for(int k=0;k<k_y;k++)
					{
						for(int l=0;l<k_x;l++)
						{
							//mean = mean + Grey[i+k,j+l]* filter_kernel[k,l];
							//median
							mod[Grey[i+k,j+l]]++; 
							
							//Grey[i+k,j+l] = Grey[i+k,j+l] * filter_kernel[k,l];
						}
					}
					mode = mod[0];
					for(int s=0;s<256;s++)
					{
						if(mod[s]>mode)
							mode=(byte)s;
					}
					
                    for(int s=0;s<256;s++)
						mod[s] = 0;

             //       if (HiGrey[1, mode]==1)// (HiMean - HiSigma) & mode < (HiMean + HiSigma))
    		//			Grey[i+1, j+1] = mode;
              //      else
                        Grey[i, j] = mode;
                /*    if (mode < 140)
                         Grey[i + 1, j + 1] = mode;
                       if (mode <= 100 )
                        Grey[i,j] = mode;
                    if (mode >= 140)
                       Grey[i, j] = mode;
                 */  //Grey[i,j] = (byte)(mean/(k_y*k_x));

				}
			}
		}

        public void Homogenization(int y, int x)
        {
            byte dx = 0;
            byte dy = 0;

            for (int i = 0; i < y - 2; i++)
            {
                for (int j = 0; j < x - 2; j++)
                {
                    if ((Grey[i, j] == Grey[i, j + 1]) & (Grey[i, j] == Grey[i + 1, j]))
                    {
                        dx = (byte)System.Math.Abs(Grey[i, j+1] - Grey[i, j + 2]);
                        dy = (byte)System.Math.Abs(Grey[i+1, j] - Grey[i + 2, j]);
                        //    if (dx == dy)
                        {
                            if (dx <= 2)
                                Grey[i, j + 2] = Grey[i, j];

                            if (dy <= 2)
                                Grey[i + 2, j] = Grey[i, j];
                        }

                        //       if ((Grey[i, j] != Grey[i, j+1]) & (Grey[i, j] != Grey[i+1, j]))
                        //           Grey[i, j] = 255;
                    }
                }
            }
        }

        public void CalculateH3D(int y, int x)
        {
            H3D = new int[2,256,256];
            H3D.Initialize();

            for (int i = 0; i < y - 1; i++)
            {
                for (int j = 0; j < x - 1; j++)
                {
                    H3D[0,Grey[i, j], Grey[i, j + 1]]++;
                    H3D[0,Grey[i, j], Grey[i+1, j]]++;

                    H3D[1, Grey[i, j], Grey[i, j + 1]] = Grey[i, j + 1];
                    H3D[1, Grey[i, j], Grey[i + 1, j]] = Grey[i + 1, j];
                }
            }
        }

        public void Equilize3DHi()
        {
            int ld_mean = 0;
            int la_mean = 0;
            int lb_mean = 0;
            int sum = 0;
            
            byte color = 0;
            int max_pos = 0;
            for (int i = 0; i < 256; i++)
            {
                ld_mean = ld_mean + H3D[0,i, i];
                if(max_pos<H3D[0,i,i])
                    max_pos = i;
              //  if (H3D[0, i, i] > 0)
              //      sum++;
            }
            ld_mean = ld_mean / 255;
            
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                if ((H3D[0,i, i] < ld_mean)&(H3D[0,i,i]>0))
                {
                    color = (byte)i;                    
                    sum = 0;
                    j = i;
                    do
                    {
                        sum = sum + H3D[0,j, j];
                        H3D[1, j, j] =  color;
                        j++;
                    } while ((sum < ld_mean) & (j < 256));
                    j--;
                    i = j;
                }
            }

            //3D upper equalization
            for (int y = 5; y < 255; y++)
            {
                int q = 0;
                la_mean = 0;
                for (q = 1; q < y-1; q++)
                    {
                        la_mean = la_mean + H3D[0, y, q];
                      //  if (H3D[0, y, q] > 0)
                      //      sum++;
                    }
                    la_mean = la_mean / q;

                for (int i = 0; i < y-1; i++)
                {
                    

                    if ((H3D[0, y, i] < la_mean) & (H3D[0, y, i] > 0))
                    {
                        color = (byte)i;

                        sum = 0;
                        j = i;
                        do
                        {
                            sum = sum + H3D[0, y, j];
                            H3D[1, y, j] = color;
                            j++;
                        } while ((sum < la_mean) & (j < 256));
                        j--;
                        i = j;
                    }
                }
            }
            //3D lower equalization
            for (int y = 0; y < 255-5; y++)//int y = 255; y > 12; y--)
            {
                int q = 0;
                lb_mean = 0;
                for (q = 256; q < y; q--)
                {
                    lb_mean = lb_mean + H3D[0, y, q];
                    //  if (H3D[0, y, q] > 0)
                    //      sum++;
                }
                lb_mean = lb_mean / (255-y);

                for (int i = 256; i < y; i--)
                {

                    if ((H3D[0, y, i] < lb_mean) & (H3D[0, y, i] > 0))
                    {
                        color = (byte)i;

                        sum = 0;
                        j = i;
                        do
                        {
                            sum = sum + H3D[0, y, j];
                            H3D[1, y, j] = color;
                            j++;
                        } while ((sum < lb_mean) & (j < 256));
                        j--;
                        i = j;
                    }
                }
            }
     
        }

        public void CalculateGreyMatrixFromH3D()
        {
            //CalculateH3D must be executed before this function
            //This function returns Grey level bitmap matrix for 3D hi
            if (bmpLoaded != null)
            {
                int y = bmpLoaded.Height;
                int x = bmpLoaded.Width;

                GreyTmp = new byte[y,x];

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        GreyTmp[i, j] = Grey[i, j];
                    }
                }

                for (int i = y-2; i > 1; i--)
                {
                    for (int j = x-2; j > 1; j--)
                    {
                        GreyTmp[i, j+1] = (byte)H3D[1,Grey[i, j], Grey[i, j+1]];
                        GreyTmp[i+1, j] = (byte)H3D[1, Grey[i, j], Grey[i+1, j]];

                        if (Grey[i, j] == Grey[i, j + 1])
                        {
                            GreyTmp[i, j] = GreyTmp[i, j+1];
                            //vertical
               //             GreyTmp[i+1, j-1] = (byte)H3D[1, Grey[i, j-1], Grey[i + 1, j-1]];
                            //horizontal
                         //   GreyTmp[i, j-1] = (byte)H3D[1, Grey[i, j-1], Grey[i, j]];
                            j--;
                        }
                        if (Grey[i, j] == Grey[i+1, j])
                        {
                            GreyTmp[i, j] = GreyTmp[i + 1, j];
               //             GreyTmp[i + 1, j - 1] = (byte)H3D[1, Grey[i, j - 1], Grey[i + 1, j - 1]];
               //             GreyTmp[i, j-1] = Grey[i+1, j-1];
                            j--;
                        }
                    }
                }
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        Grey[i, j] = GreyTmp[i, j];
                    }
                }


            }

        }

        private void Test_Click(object sender, System.EventArgs e)
		{
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;

            
            CalculateH3D(y, x);

            GreyMatrixToBitmap(y, x);

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
       //     DrawHistogram();
            Draw3DHistogram();

		}

		public void ClearHistogram()
		{
			for(int i=0;i<256;i++)
			{
				HiGrey[0,i] = 0;
			}

		}
		public void DrawHistogram()
		{
			int x = bmpLoaded.Width;
			int y = bmpLoaded.Height;
			ClearHistogram();

			//This function calculates GreyLevel image histogram
			//From grey level pixel array
			for(int i=0; i<y; i++)
			{
				for(int j=0;j<x; j++)
				{
					HiGrey[0,Grey[i,j]] ++;
				}
			}
            //Calculate the mean value
            HiMean = StatMean(y, x);
            HiSigma = StatSigma(y, x, HiMean);

            lblHist.Text = "Hi[] mean:" + HiMean.ToString() +
                       " Hi[] sigma:" + HiSigma.ToString();

			HiNormalize(y,x);

			//HiGreyMax = HiMax();
			double HiNGreyMax = HiNMax();
			//Draw Histogram
			Bitmap HistogramBmp = new Bitmap(PicHist.Width, PicHist.Height,
				PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(HistogramBmp);

			Pen pen = new Pen(Color.Blue,1);
			int sx=1;
			int sy=PicHist.Height;
			for(int i=0;i<256;i++)
			{
				Point p=new Point(sx+i,sy);
				Point p1;
				//	if(HiGrey[i]>0)
				//		p1 = new Point(sx+i,sy-(int)Math.Log((double)HiGrey[i]));
				//	else
			
				//	int h = HiGrey[i];
				double hd = HiGreyN[i];
				hd = 256*hd;
				if(HiNGreyMax>0)
					hd = hd/HiNGreyMax;
				
				int h = (int)hd;
				p1 = new Point(sx+i,sy-h);
				g.DrawLine(pen,p,p1);
			}
            for(int i=0;i<5;i++)
            {
                //Draw string
                // Create string to draw.
                String drawString = (i*64).ToString();

                // Create font and brush.
                Font drawFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                PointF drawPoint = new Point(i*59, 0);
                // Draw string to screen.
                g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }

            //Draw Mean
            Point p2 = new Point(HiMean, 0);
            Point p3 = new Point(HiMean, 256);
            pen = new Pen(Color.Red, 1);
            g.DrawLine(pen, p2, p3);

            //Draw Sigma
            p2 = new Point(HiMean - HiSigma, 0);
            p3 = new Point(HiMean - HiSigma, 256);
            pen = new Pen(Color.Magenta, 1);
            g.DrawLine(pen, p2, p3);

            p2 = new Point(HiMean + HiSigma, 0);
            p3 = new Point(HiMean + HiSigma, 256);
            g.DrawLine(pen, p2, p3);

			
            PicHist.Image = new Bitmap(HistogramBmp);
			HistogramBmp.Dispose();
			g.Dispose();

       //     H2D_Replace();

		}

        private unsafe void test(int *ptr)
        {
            //*pInt=(*pInt)*3;

            int mean = 0;
            for (int i = 0; i < 256; i++)
            {
                mean = mean + *(ptr+i);//int)ptr.GetValue(i);
            }
            lblHist.Text = "Hi[] mean:" + mean.ToString();
        }
        private int StatSigma(int y, int x,int mean)
        {
            //This function calculates standard deviation from image 2D histogram
            int sigma = 0;
            for (int i = 0; i < 256; i++)
            {
                sigma = sigma + (Math.Abs((i - mean)) * HiGrey[0,i]);

            }
            sigma = sigma / (x * y);
            //sigma = (int)Math.Sqrt((double)sigma);

         //   lbl2.Text = "Hi[] mean:" + mean.ToString() +
         //       " Hi[] sigma:" + sigma.ToString();
            return sigma;
        }
        private int StatMean(int y, int x)
        {
            //This function calculates mean from image 2D histogram
            int mean = 0;
            for (int i = 0; i < 256; i++)
            {
                mean = mean + HiGrey[0,i]*i;
            }
            mean = mean / (x * y);

                 
        //    lbl2.Text = "Hi[] mean:" + mean.ToString() +
        //        " Hi[] sigma:" + sigma.ToString();
            return mean;
        }

        private void SaveImage_Click(object sender, System.EventArgs e)
		{
			if(bmpLoaded!=null)
			SaveBitmap(bmpLoaded);
		
		}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                DrawHistogram();
            }
        }

        public void CalculateEntropy(int start, int end)
        {
            if (start >= end)
                return;

            int x = bmpLoaded.Width;
            int y = bmpLoaded.Height;
            ClearHistogram();

            //This function calculates GreyLevel image histogram
            //From grey level pixel array
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    HiGrey[0, Grey[i, j]]++;
                }
            }
            //This operation converts histogram in to probability space
            HiNormalize(y, x);
            for (int i = 0; i < start; i++)
            {
                HiGreyN[i] = 0;
            }
            for (int i = end; i < 256; i++)
            {
                HiGreyN[i] = 0;
            }
            //Calculate Entropy of 2D histogram
            double Sum_prob_1k = 0, Sum_prob_kl = 0,
                Sum_prob_ln_1k = 0, Sum_prob_ln_kl = 0;

            for (int k = start; k < end; k++)
            {
                Sum_prob_1k = 0; Sum_prob_kl = 0;
                Sum_prob_ln_1k = 0; Sum_prob_ln_kl = 0;
                //i=1 need to be start = 1
                for (int i = 1; i < k; i++)
                {
                    Sum_prob_1k += HiGreyN[i];
                    if (HiGreyN[i] != 0)
                        Sum_prob_ln_1k += (HiGreyN[i] * System.Math.Log(HiGreyN[i]));

                }
                for (int i = k; i < end; i++)
                {
                    Sum_prob_kl += HiGreyN[i];
                    if (HiGreyN[i] != 0)
                        Sum_prob_ln_kl += (HiGreyN[i] * System.Math.Log(HiGreyN[i]));

                }
                //Final equation of entropy for each K
                EiGrey[k] = System.Math.Log(Sum_prob_1k) + System.Math.Log(Sum_prob_kl) -
                    (Sum_prob_ln_1k / Sum_prob_1k) - (Sum_prob_ln_kl / Sum_prob_kl);
                if (EiGrey[k] < 0)
                    EiGrey[k] = 0;
            }
            //End calculating 2D Entropy

        }

        public void DrawEntropy()
        {
          //  CalculateEntropy(0,256);
            
            //Draw Entropy
            Bitmap HistogramBmp = new Bitmap(PicHist.Width, PicHist.Height,
                PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(HistogramBmp);

            Pen pen = new Pen(Color.Blue);
            int sx = 1;
            int sy = PicHist.Height;

            double max = EiMax();
            max = 256 / max;

            for (int i = 0; i < 256; i++)
            {
                Point p = new Point(sx + i, sy);
                Point p1;
                int h;
                if (EiGrey[i] >= 0)
                {
                    h = (int)(EiGrey[i] *max);
                }
                else
                    h = 0;

                p1 = new Point(sx + i, sy - h);
                g.DrawLine(pen, p, p1);
            }

            pen.Width = 6;
            for (int i = 0; i < 5; i++)
            {
                //Draw string
                // Create string to draw.
                String drawString = (i * 64).ToString();

                // Create font and brush.
                Font drawFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                PointF drawPoint = new Point(i * 59, 0);
                // Draw string to screen.
                g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }

            //Draw Max
            lblHist.Text = "Ei[] max:" + EiMax().ToString() +
                "\nEi[]max pos:" + EiMaxPos().ToString();

     //       EMaxPos1 = EiMaxPos();

            Point p2 = new Point(EMaxPos[3], 0);
            Point p3 = new Point(EMaxPos[3], 256);
            pen = new Pen(Color.Red, 1);
            g.DrawLine(pen, p2, p3);

            p2 = new Point(EMaxPos[1], 0);
            p3 = new Point(EMaxPos[1], 256);
            pen = new Pen(Color.Magenta, 1);
            g.DrawLine(pen, p2, p3);

            p2 = new Point(EMaxPos[5], 0);
            p3 = new Point(EMaxPos[5], 256);
            pen = new Pen(Color.Magenta, 1);
            g.DrawLine(pen, p2, p3);


            PicHist.Image = new Bitmap(HistogramBmp);
            HistogramBmp.Dispose();
            g.Dispose();

            

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
               CalculateEntropy(0, 256);
               EMaxPos[3] = EntropyMaxPos(0, 256);
               DrawEntropy(); 
            }
            btnEMax.Enabled = true;

        }

        private void PicBox_Click(object sender, EventArgs e)
        {

            if (bmpLoaded != null)
            {
 
                frmShowPicture = new frmPic();
                frmShowPicture.Show();
                frmShowPicture.LoadBitmap(bmpLoaded);

                //picLarge.Image = new Bitmap(frmShowPicture.bmpPic);
            }
           
        }

        public void Draw3DHistogram()
        {
            int x = bmpLoaded.Width;
            int y = bmpLoaded.Height;
            
            //Draw Histogram
            Bitmap HistogramBmp = new Bitmap(PicHist.Width, PicHist.Height,
                PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(HistogramBmp);

            Pen pen = new Pen(Color.Black);
            Color clr = new Color();

            Point p = new Point();
            Point p1 = new Point();

            double max = H3DMax();
            double H, S, I;    
            int R, G, B;
            I = 0.63;
            S = 0.9;

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    p.X = i;
                    p.Y = j;
                    p1.X = i + 1;
                    p1.Y = j + 1;

                    if (ch3DColor.Checked == false)
                    {                    //Meke it BW
                        H = 255 * ((double)H3D[0,i, j]) / max;
                        R = (int)H;
                        R = R & 0xFF;
                        clr = Color.FromArgb(R, R, R);
                    }
                    if (ch3DColor.Checked == true)
                    {
                        H = 360-360.0 * ((double)H3D[0,i, j]) / max;
                     //   H =  i * 1.44;
                        clr = HSI_RGB(H, S, I);
                    }
                             
                    pen.Color = clr;
                    g.DrawLine(pen, p1, p);
                }
                
            }

            for (int i = 0; i < 5; i++)
            {
                //Draw string
                // Create string to draw.
                String drawString = (i * 64).ToString();

                // Create font and brush.
                Font drawFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                PointF drawPoint = new Point(i * 59, 0);
                // Draw string to screen.
                g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }

            for (int i = 0; i < 5; i++)
            {
                //Draw string
                // Create string to draw.
                String drawString = (i * 64).ToString();

                // Create font and brush.
                Font drawFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.
                PointF drawPoint = new Point(0,i * 59);
                // Draw string to screen.
                g.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }

            PicHist.Image = new Bitmap(HistogramBmp);
            HistogramBmp.Dispose();
            g.Dispose();

        }

        private Color HSI_RGB(double H, double S, double I)
        {
          //  I = 0.63;
          //  S = 0.9;
            Color colr = Color.White;
            double P, Q;
            int R, G, B;
            
            Q = (I < 0.5) ? (I * (1.0 + S)) : (I + S - (I * S));
            P = (2.0 * I) - Q;

            double Hk = H / 360.0;
            double[] T = new double[3];
            T[0] = Hk + (1.0 / 3.0);    // Tr
            T[1] = Hk;                // Tb
            T[2] = Hk - (1.0 / 3.0);    // Tg

            for (int k = 0; k < 3; k++)
            {
                if (T[k] < 0) T[k] += 1.0;
                if (T[k] > 1) T[k] -= 1.0;

                if ((T[k] * 6) < 1)
                {
                    T[k] = P + ((Q - P) * 6.0 * T[k]);
                }
                else if ((T[k] * 2.0) < 1) //(1.0/6.0)<=T[i] && T[i]<0.5
                {
                    T[k] = Q;
                }
                else if ((T[k] * 3.0) < 2) // 0.5<=T[i] && T[i]<(2.0/3.0)
                {
                    T[k] = P + (Q - P) * ((2.0 / 3.0) - T[k]) * 6.0;
                }
                else T[k] = P;
            }
            R = (int)(T[0] * 255.0);
            G = (int)(T[2] * 255.0);
            B = (int)(T[1] * 255.0);

            R = R & 0xFF;
            G = G & 0xFF;
            B = B & 0xFF;
            colr = Color.FromArgb(R,G,B);
            return colr;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;

            //   BitmapToMatrix(bmpLoaded);
           	DSP3x3(5,y,x);

            CalculateH3D(y, x);

       //     Homogenization(y, x);
            /*    for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                        Grey[i, j] = 128;
                }
    */
            GreyMatrixToBitmap(y, x);

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            //	bmpLoaded = new Bitmap(RedMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(GreenMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(BlueMatrixToBitmap(y,x));
            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
            DrawHistogram();

        }

        private void btnSaveHistogram_Click(object sender, EventArgs e)
        {
            histBitmap = new Bitmap(PicHist.Image);
            if (histBitmap != null)
                SaveBitmap(histBitmap);

           

        }

        private void btnHomogeniza_Click(object sender, EventArgs e)
        {
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;

            //   BitmapToMatrix(bmpLoaded);
        //    DSP3x3(3, y, x);

            CalculateH3D(y, x);

            Homogenization(y, x);
            /*    for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                        Grey[i, j] = 128;
                }
    */
            GreyMatrixToBitmap(y, x);

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            //	bmpLoaded = new Bitmap(RedMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(GreenMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(BlueMatrixToBitmap(y,x));
            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
            DrawHistogram();

        }

        private void PicHist_Click(object sender, EventArgs e)
        {
            if (bmpLoaded != null)
            {

            }

        }

        private void btnSave2DH_Click(object sender, EventArgs e)
        {
            SaveHistogram(1);
        }

        private void btnSqave3DH_Click(object sender, EventArgs e)
        {
            SaveHistogram(2);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;


            CalculateH3D(y, x);

            GreyMatrixToBitmap(y, x);

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
            DrawHistogram();
            Draw3DHistogram();
        }

        public void H2D_Replace()
        {
            int HiGreyMean = 0;
            int HiGreySigma = 0;
            int HiNGreyMax = 0;

            for (int i = 0; i < 256; i++)
            {
                HiGreyMean = HiGreyMean + HiGrey[0, i];
                HiGrey[1, i] = i;

                if (HiGrey[0, i] > HiNGreyMax)
                    HiNGreyMax = HiGrey[0, i];
            }
            if (HiNGreyMax < 1)
                HiNGreyMax = 1;
            HiGreyMean = HiGreyMean / HiNGreyMax;

            for (int i = 0; i < 256; i++)
            {
                HiGrey[0, i] = 256 * HiGrey[0, i] / HiNGreyMax;
            }

            for (int i = 0; i < 256; i++)
            {
                HiGreySigma = HiGreySigma + System.Math.Abs(HiGreyMean - HiGrey[0, i]);
            }
            HiGreySigma = HiGreySigma / 256;

            byte color = 0;
            int sum = 0;
            int j = 0;

            for (int i = 0; i < 256; i++)
            {
                if ((HiGrey[0, i] < HiGreyMean)&(HiGrey[0,i]>0))
                {
                    color = (byte)i;
                    sum = 0;
                    j = i;
                    do
                    {
                        sum = sum + HiGrey[0, j];
                        HiGrey[1, j] = color;
                        j++;
                    } while ((sum < HiGreyMean) & (j < 256));
                    j--;
                    i = j;
                  //  HiGrey[1, 1] = 20;
                    if (color == 0)
                    {
                        color = (byte)(j / 2);
                        j = 0;
                        do
                        {
                            HiGrey[1, j] = color;
                            j++;
                        } while (j < color*2);
                        
                    }
                   
                    
                   // HiGrey[1, i] = HiGreyMean;
                }
              // HiGrey[1, j] = i;
            }

            //Remove lower section is process in real time!!!!!!!!!

            lblHist.Text = (HiGreySigma).ToString();
            lblHist.Text = lblHist.Text + "   " + HiGreyMean.ToString();
            //   HiGreySigma /= 256;


            HiGreyMean = 256 - HiGreyMean;
            if (PicHist.Image != null)
            {
                Graphics gr = Graphics.FromImage(PicHist.Image);//HistogramBmp);
                Pen pen = new Pen(Color.Red, 1);
                Point p1 = new Point(0, HiGreyMean);
                Point p2 = new Point(256, HiGreyMean);
                gr.DrawLine(pen, p1, p2);

                pen = new Pen(Color.Magenta, 1);
                p1.Y = HiGreyMean - HiGreySigma;
                p2.Y = p1.Y;
                gr.DrawLine(pen, p1, p2);

                //        PicHist.Image = new Bitmap(HistogramBmp);
                //       HistogramBmp.Dispose();
                gr.Dispose();
                pen.Dispose();
                PicHist.Invalidate();
            }
            
        }

        public void EquilizeH2D()
        {
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Grey[i, j] = (byte)HiGrey[1, Grey[i, j]];
                }
            }
            GreyMatrixToBitmap(y, x);

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            //	bmpLoaded = new Bitmap(RedMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(GreenMatrixToBitmap(y,x));
            //	bmpLoaded = new Bitmap(BlueMatrixToBitmap(y,x));
            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
            DrawHistogram();
        }

        private void btnHEq2D_Click(object sender, EventArgs e)
        {
            H2D_Replace();
            EquilizeH2D();

           
        }

        private void btnEMax_Click(object sender, EventArgs e)
        {
           CalculateEntropy(0,256);
           EMaxPos[3] = EntropyMaxPos(0,256);
           
           CalculateEntropy(0, EMaxPos[3]);
           EMaxPos[1] = EntropyMaxPos(0, EMaxPos[3]);

           CalculateEntropy(EMaxPos[3], 256);
           EMaxPos[5] = EntropyMaxPos(EMaxPos[3], 256);


           for (int i = 0; i < 256; i++)
           {
               if (i < EMaxPos[1])
                   HiGrey[1, i] = 0;
               if ((i < EMaxPos[3]) & (i >=EMaxPos[1]))
                   HiGrey[1, i] = 64;
               if ((i < EMaxPos[5]) & (i >=EMaxPos[3]))
                   HiGrey[1, i] = 128;
               if ((i < 256)& (i>=EMaxPos[5]))
                   HiGrey[1, i] = 255;

           }
           
           EquilizeH2D();
            DrawEntropy();
        }

        private int EntropyMaxPos(int start, int end)
        {
            int max_pos = 0;
            double tmp = 0;

            for (int i = start; i < end; i++)
            {
                if (EiGrey[i] > tmp)
                {
                    tmp = EiGrey[i];
                    max_pos = i;
                }
            }
            return max_pos;
        }

        private void btnEMax8_Click(object sender, EventArgs e)
        {
            CalculateEntropy(0, 256);
            EMaxPos[3] = EntropyMaxPos(0, 256);

            CalculateEntropy(0, EMaxPos[3]);
            EMaxPos[1] = EntropyMaxPos(0, EMaxPos[3]);

            CalculateEntropy(EMaxPos[3], 256);
            EMaxPos[5] = EntropyMaxPos(EMaxPos[3], 256);

            // 0-1
            CalculateEntropy(0, EMaxPos[1]);
            EMaxPos[0] = EntropyMaxPos(0, EMaxPos[1]);
            // 1-3
            CalculateEntropy(EMaxPos[1], EMaxPos[3]);
            EMaxPos[2] = EntropyMaxPos(EMaxPos[1], EMaxPos[3]);
            // 3-5
            CalculateEntropy(EMaxPos[3], EMaxPos[5]);
            EMaxPos[4] = EntropyMaxPos(EMaxPos[3], EMaxPos[5]);
            // 5-6
            CalculateEntropy(EMaxPos[5], 256);
            EMaxPos[6] = EntropyMaxPos(EMaxPos[5], 256);

            for (int i = 0; i < 256; i++)
            {
                if (i < EMaxPos[0])
                    HiGrey[1, i] = 0;
                if ((i < EMaxPos[1]) & (i >= EMaxPos[0]))
                    HiGrey[1, i] = 32;
                if ((i < EMaxPos[2]) & (i >= EMaxPos[1]))
                    HiGrey[1, i] = 64;
                if ((i < EMaxPos[3]) & (i >= EMaxPos[2]))
                    HiGrey[1, i] = 96;
                if ((i < EMaxPos[4]) & (i >= EMaxPos[3]))
                    HiGrey[1, i] = 128;
                if ((i < EMaxPos[5]) & (i >= EMaxPos[4]))
                    HiGrey[1, i] = 160;
                if ((i < EMaxPos[6]) & (i >= EMaxPos[5]))
                    HiGrey[1, i] = 192;
                if ((i < 256) & (i >= EMaxPos[6]))
                    HiGrey[1, i] = 255;
            }

            EquilizeH2D();
            DrawEntropy();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            { 
                if (i < 64)
                    HiGrey[1, i] = 0;
                if ((i < 128) & (i >= 64))
                    HiGrey[1, i] = 64;
                if ((i < 192) & (i >= 128))
                    HiGrey[1, i] = 128;
                if ((i < 256) & (i >= 192))
                    HiGrey[1, i] = 255;

            }

            EquilizeH2D();
            DrawEntropy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        /*    CalculateEntropy(0, 256);
            EMaxPos[3] = EntropyMaxPos(0, 256);

            CalculateEntropy(0, EMaxPos[3]);
            EMaxPos[1] = EntropyMaxPos(0, EMaxPos[3]);

            CalculateEntropy(EMaxPos[3], 256);
            EMaxPos[5] = EntropyMaxPos(EMaxPos[3], 256);

            // 0-1
            CalculateEntropy(0, EMaxPos[1]);
            EMaxPos[0] = EntropyMaxPos(0, EMaxPos[1]);
            // 1-3
            CalculateEntropy(EMaxPos[1], EMaxPos[3]);
            EMaxPos[2] = EntropyMaxPos(EMaxPos[1], EMaxPos[3]);
            // 3-5
            CalculateEntropy(EMaxPos[3], EMaxPos[5]);
            EMaxPos[4] = EntropyMaxPos(EMaxPos[3], EMaxPos[5]);
            // 5-6
            CalculateEntropy(EMaxPos[5], 256);
            EMaxPos[6] = EntropyMaxPos(EMaxPos[5], 256);
            */

            for (int i = 0; i < 256; i++)
            {
                if (i < 32)
                    HiGrey[1, i] = 0;
                if ((i < 64) & (i >= 32))
                    HiGrey[1, i] = 32;
                if ((i < 96) & (i >= 64))
                    HiGrey[1, i] = 64;
                if ((i < 128) & (i >= 96))
                    HiGrey[1, i] = 96;
                if ((i < 160) & (i >= 128))
                    HiGrey[1, i] = 128;
                if ((i < 192) & (i >= 160))
                    HiGrey[1, i] = 192;
                if ((i < 224) & (i >= 192))
                    HiGrey[1, i] = 224;
                if ((i < 256) & (i >= 224))
                    HiGrey[1, i] = 255;
            }

            EquilizeH2D();
            DrawEntropy();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int y = bmpLoaded.Height;
            int x = bmpLoaded.Width;
            CalculateH3D(y, x);
            //This is important function
            Equilize3DHi();   
            CalculateGreyMatrixFromH3D();

            bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));

            if (PicBox.Image != null)
                PicBox.Image.Dispose();

            PicBox.Image = new Bitmap(bmpLoaded);
            DrawHistogram();
        //    Draw3DHistogram();
        }

        private void btnLoadAvi_Click(object sender, EventArgs e)
        {
            //This function open AVI file for DSP and 
            //stream redirection to new avi file
            Cursor.Current = Cursors.WaitCursor;
            //    avi.OpenAvi();

            openAVI.Title = "Open AVI";
            string str = "";
            string filename_open = "";
        //    string filename_save = "";
            int FileLenght = 0;
            //Filters to open only AVI files
            openAVI.Filter = "AVI files (*.avi)|*.avi|Motion Statistic files (*.stat)|*stat";
            if (openAVI.ShowDialog() == DialogResult.OK)
            {
                //Here we need to check is there already Ststistic.bin file
                filename_open = openAVI.FileName;
                //Open AVI file to read all frames
                avi_reader.Open(filename_open);

                int y = avi_reader.height;
                int x = avi_reader.width;
                ResizeColorMatrix(y, x);

                avi_writer.Open("DSP.avi", x, y);
                //Recalculate stat structure to holld all frames statistis
                //	StatisticalData [] stat = new StatisticalData[avi_reader.length];
                FileLenght = avi_reader.length;
                Cursor.Current = Cursors.WaitCursor;

                for (int i = 0; i < FileLenght; i++)//FileLenght
                {
                    if (bmpLoaded != null)
                        bmpLoaded.Dispose();


                    bmpLoaded = avi_reader.GetNextFrame();

                    //BITMAP processing
                    BitmapToMatrix(bmpLoaded);
                    //Equilize 2D Histogram
                    //     {
                             H2D_Replace();
                             EquilizeH2D();
                    //         bmpLoaded = new Bitmap(GreyMatrixToBitmap(y, x));
                    //     }
                    //Calculate H3D equalize
                    {
                    //    CalculateH3D(y, x);
                        //This is important function
                    //    Equilize3DHi();
                    //    CalculateGreyMatrixFromH3D();
                    }
                    //Calculate 8 levels standard treshoding
                    button2_Click(sender, e);
                    //     BitmapToMatrix(bmpLoaded);
                    //Calculate 8 levels entropy
                    //btnEMax8_Click(sender, e);

                    //Save DSP frame
                    avi_writer.AddFrame(bmpLoaded);
                }
                

                avi_reader.Close();
                avi_writer.Close();

                str.Remove(0, str.Length);

            }
        }

        private void btnEntropy_Click(object sender, EventArgs e)
        {
            CalculateEntropy(0, 256);
            EMaxPos[3] = EntropyMaxPos(0, 256);

            for (int i = 0; i < 256; i++)
            {
                if (i <= EMaxPos[3])
                    HiGrey[1, i] = 0;
                if ((i > EMaxPos[3]))
                    HiGrey[1, i] = 255;
            }
            EquilizeH2D();
            DrawEntropy();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                if (i <= 128)
                    HiGrey[1, i] = 0;
                if (i >128)
                    HiGrey[1, i] = 255;
            }
            EquilizeH2D();
            DrawEntropy();
        }

    }
}
