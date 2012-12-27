using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using MotionStatistic;
using ImageStatistic;

namespace AVI_Loader
{
	/// <summary>
	/// Summary description for StatisticStream.
	/// </summary>
	public class StatisticStream
	{
        public frmLoaded FormLoaded;
        public Bitmap bmpTmp;

		private ImageStatistic.ImageStatistic imagestatistic = new ImageStatistic.ImageStatistic();
		private AVIReader avi_reader = new AVIReader();
        private AVIWriter avi_writer = new AVIWriter();
		//This structure will store statistical data for motion
		private StatisticalData [] stat = new StatisticalData[0];
		
		private int FileLenght = 0;
		
		private void StatisticWriteTXT(string FileName)
		{
			using (StreamWriter sw = new StreamWriter(FileName)) 
			{
			//	StatisticalData [] stat = new StatisticalData[FileLenght];
				StatisticalData stat1 = new StatisticalData();
				string str = "";
				for (int i = 0; i < FileLenght; i++) 
				{
				//	stat[i].K = (double) 100;
					str = "Frm " + i.ToString() + ";";
					str = str + stat[i].mean.ToString() + ";";
					str = str + stat[i].sigma.ToString() + ";";
					str = str + stat[i].K.ToString() + ";";
					str = str + stat[i].P.ToString() + ";";
					str = str + stat[i].Z.ToString();
					sw.WriteLine(str);
					str = "";
				}
				// Add some text to the file.
				sw.WriteLine("-------------------");
                sw.WriteLine("mean;sigma;K;P;Z");
				// Arbitrary objects can also be written to the file.
				sw.Write("File Created: ");
				sw.WriteLine(DateTime.Now);
			}	
		}
		public void StatisticWrite(string FileName, string AViFileName, string TXtFileName)
		{
			// Create the new, empty data file.
			FileStream fs = new FileStream(FileName, FileMode.CreateNew);
			// Create the writer for data.
			BinaryWriter w = new BinaryWriter(fs);
			// Write data to Test.data.

			//Open AVI file to read all frames
			avi_reader.Open(AViFileName);
            
            avi_writer.Open("DSP.avi", avi_reader.width, avi_reader.height);
			//Recalculate stat structure to holld all frames statistis
		//	StatisticalData [] stat = new StatisticalData[avi_reader.length];
			FileLenght = avi_reader.length;
			stat = new StatisticalData[FileLenght];

			string str = "";
			StatisticalData stat1 = new StatisticalData();
            
            //Open window to show opened frames
	//		FormLoaded = new frmLoaded();
     //       FormLoaded.Show();
            int count = 0;
            Cursor.Current = Cursors.WaitCursor;
			for (int i = 0; i < FileLenght; i++) 
			{
                if(bmpTmp != null)
                    bmpTmp.Dispose();
               bmpTmp = avi_reader.GetNextFrame();
               avi_writer.AddFrame(bmpTmp);
               
               imagestatistic.ImageStreamStatistic(bmpTmp);
                //Here we can add DSP features
                //Visualize frames

        //       if (count > 10)
        //       {
        //          count = 0;
        //           FormLoaded.LoadBitmap(bmpTmp);  
        //       }
       //       count++;

				stat[i] = imagestatistic.StatisticDataa;
				stat1 = stat[i];
		//		str = str + ";" + i.ToString();
				w.Write(stat1.mean);
				w.Write(stat1.sigma);
				w.Write(stat1.K);
				w.Write(stat1.P);
				w.Write(stat1.Z);
				
			}
			avi_reader.Close();
            avi_writer.Close();
            
      //      FormLoaded.LoadBitmap(bmpTmp);

            //Close preview window
         //   FormLoaded.Close();
		//	MessageBox.Show("Writed data is: " + str);
			str.Remove(0,str.Length);
			w.Close();
			fs.Close();

			StatisticWriteTXT(TXtFileName);
        
		}

		public void StatisticRead(string FileName)
		{
			// Read the file.
			FileStream fs;
			// Create the reader for data.
			fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
			BinaryReader r = new BinaryReader(fs);
			// Read data from Test.data.
			double[] ar = new double [FileLenght];

			string str = "";
	//		StatisticalData [] stat = new StatisticalData[FileLenght];
			for (int i = 0; i < FileLenght; i++) 
			{
		//		ar[i] = r.ReadDouble();
				stat[i].mean = r.ReadByte();
				stat[i].sigma = r.ReadByte();
				stat[i].K = r.ReadDouble();
				stat[i].P = r.ReadDouble();
				stat[i].Z = r.ReadDouble();
				//		str = str + ";" + ar[i].ToString();
				//	Console.WriteLine(r.ReadInt32());
			}
				
		//	MessageBox.Show("Loaded data is: " + str);
			str.Remove(0,str.Length);
			r.Close();
			fs.Close();
		}
	}
}
