using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Data;
using MotionStatistic;

namespace ImageStatistic
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ImageStatistic
	{
		public MotionStatistic.StatisticalData StatisticDataa = new MotionStatistic.StatisticalData(0,0,0.0,0.0,0.0);
		double meanb;
		double sigmab;

		private double [,] Hia3D = new double [256,256];
		private double [,] Hib3D = new double [256,256];
		private double [,] Hid3D = new double [256,256];
		private byte [,] GREY; 

		int x,y;
		int pixels;
        
		public Bitmap processImage(Bitmap image)
		{
			Bitmap bmp = null;
			return bmp;
		}
		public void ImageStreamStatistic(Bitmap bmp)
		{
		//	bmp_local = bmp;
			x = bmp.Width;
			y = bmp.Height;
			pixels = x*y;
			StatisticDataa.mean = 0;
			GREY = new byte [y,x];
			BitmapToBWArray(bmp);
			//Calculate 3D Histogram and normalize
			Histogram3D();
			StatisticDataa.K = K();
			
			StatisticDataa.Z = Z();
			StatisticDataa.P = P();
			//Move Ha to Hb array
			ReplaceH3D();
						
			meanb = StatisticDataa.mean;
			sigmab = StatisticDataa.sigma;
		}
		//This code converts color Bitmap to Greysvcale
		private void BitmapToBWArray(Bitmap image)
		{
			Bitmap returnMap = new Bitmap(image.Width, image.Height, 
				PixelFormat.Format32bppArgb);
			BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, 
				image.Width, image.Height), 
				ImageLockMode.ReadOnly, 
				PixelFormat.Format32bppArgb);

			int a = 0;
			unsafe 
			{
				double mean = 0;
				
				byte* imagePointer1 = (byte*)bitmapData1.Scan0;
				for(int i = 0; i < bitmapData1.Height; i++) 
				{
					for(int j = 0; j < bitmapData1.Width; j++) 
					{
						// write the logic implementation here
						a = (imagePointer1[0] + imagePointer1[1] + 
							imagePointer1[2])/3;
						if(a>255)
							a = 255;
						if(a<0)
							a = 0;

						imagePointer1 += 4;
						mean = mean + a;
						GREY[i,j] = (byte)a;
					}
					imagePointer1 += bitmapData1.Stride - 
						(bitmapData1.Width * 4);	
				}	
				mean = mean/pixels;
				StatisticDataa.mean = mean;

				//Calculate sigma
				double grey = 0;
				double sigma = 0;
				for(int i=0;i<y;i++)
				{
					for(int j=0;j<x;j++)
					{
						grey = (double) GREY[i,j];
						sigma = sigma + (grey - mean)*(grey - mean);
					}
				}
				sigma = sigma/((double)pixels-1.0);
				sigma = Math.Sqrt(sigma);
				StatisticDataa.sigma = sigma;

			}
			//end unsafe;

			image.UnlockBits(bitmapData1);

		}//end processImage

		private double P()
		{
			double P=0;
			double tmp;
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
					tmp = (Hia3D[i,j] - Hib3D[i,j]);///(255*255);
					tmp = tmp * tmp;
					if(Hia3D[i,j]>0)
						tmp =tmp/Hia3D[i,j];
					else
						tmp = tmp/0.0000000001;
					P = P + tmp;

				}
			}	
			return System.Math.Sqrt(P);
		}
		private double K()
		{
			double K=0;
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
						Hid3D[i,j] = Math.Abs(
							Hib3D[i,j] - Hia3D[i,j]);
				}
			}
			K = Histogram3DMax()/(255*255);

			return K;
		}

		private double Histogram3DMax()
		{
			double max = 0;
			max = Hid3D[0,0];
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
					if(max < Hid3D[i,j])
						max = Hid3D[i,j];
				}
			}
			return max;
		}
		private void ReplaceH3D()
		{
			//Move Ha to Hb array
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
					Hib3D[i,j] = Hia3D[i,j];
				}
			}
		}
		private double Z()
		{
			double Z=0;
			Z = (double)meanb - (double) StatisticDataa.mean;
			Z = Z/( (double)StatisticDataa.sigma + (double) sigmab);
		//	Z = 1/(double)pixels;
			return Z;
		}
		private void Histogram3D()
		{
			//Calculate 3D Histogram
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
					Hia3D[i,j] = 0;
				}
			}

			for(int i=0;i<y-1;i++)
			{
				for(int j=0;j<x-1;j++)
				{
					Hia3D[GREY[i,j],GREY[i,j+1]]++;
					Hia3D[GREY[i,j],GREY[i+1,j]]++;
				}
			}
			//Normalize histogram
			for(int i=0;i<256;i++)
			{
				for(int j=0;j<256;j++)
				{
	//				Hia3D[i,j] = Hia3D[i,j]/(double)pixels;
				}
			}
		}
	}
}
