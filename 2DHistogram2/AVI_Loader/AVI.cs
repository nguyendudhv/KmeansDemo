using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data;
using System.IO;

namespace AVI_Loader
{
	/// <summary>
	/// Summary description for AVI.
	/// </summary>
	public class AVI
	{
		private StatisticStream statstream;
	//	private AVIReader avi_reader = new AVIReader();
		public string s_AVI_FileName;
		public string s_STAT_FileName;
		public string s_STAT_FileName_txt;
		private System.Windows.Forms.OpenFileDialog openAVI = new OpenFileDialog();
	//	private System.Windows.Forms.ProgressBar prbAVI_Statistic = new ProgressBar();
		
		public string OpenAvi()
		{
			openAVI.Title = "Open AVI";
			string str = "";
			//Filters to open only AVI files
			openAVI.Filter = "AVI files (*.avi)|*.avi|Motion Statistic files (*.stat)|*stat" ;
			if( openAVI.ShowDialog() == DialogResult.OK)
			{
				//Here we need to check is there already Ststistic.bin file
				int result=0;
				s_AVI_FileName = openAVI.FileName;
				result = CheckForStatisticFile(s_AVI_FileName);
				if(result == 1)
				{
					//Load Statistic File
					StatisticFileLoading();
			
				//	MessageBox.Show("Statistic File Loading");
				}
				else
				{
					//Calculate Statistic File
					StatisticFileCalculating();
				//	MessageBox.Show("Statistic File Calculating");
				}
				
				//Get AVI File Info
			/*	avi_reader.Open(openAVI.FileName);
				str = "Frame rate: " +	avi_reader.FrameRate.ToString();
				str = str + "\n" + "Total frames: " + avi_reader.Length;
				str = str + "\n" + "Codec: " +	avi_reader.codec;
				avi_reader.Close();
			*/
			}
			str = " ";
			return str;
		}
		private int CheckForStatisticFile(string fname)
		{
			//This function try to find a binary file
			//that have the same name as opened AVI file
			int length = fname.Length;
			fname = fname.Remove(length-4,4);
			s_STAT_FileName = fname + ".stat";
			s_STAT_FileName_txt = fname + "1" + ".txt";
			//MessageBox.Show(fname);
			//now fname is "*.stat" so try to open it
			if (File.Exists(s_STAT_FileName)) 
			{
			//	MessageBox.Show("Statistic File With Such a Name already exist!");
				return 1;
			}
			return 0;
		}
		private int StatisticFileLoading()
		{
			//This function will read an existing Statistic file
			//Easy to use function just reads Statistic file
			MessageBox.Show("Statistic File Loading");
			ReadStatisticFile();
			return 1;
		}
		private int StatisticFileCalculating()
		{
			//This function will calculate a Statistic file
			//It is very complecated function and will be done in separate class
			MessageBox.Show("Calculating Statistic File");
			SaveStatisticFile();
			return 1;
		}
		private int SaveStatisticFile()
		{
			statstream = new StatisticStream();
		//	statstream.StatisticWriteTXT(s_STAT_FileName_txt);
			statstream.StatisticWrite(s_STAT_FileName,s_AVI_FileName,s_STAT_FileName_txt);
			//save
			return 1;
		}
		private int ReadStatisticFile()
		{
			statstream = new StatisticStream();
			statstream.StatisticRead(s_STAT_FileName);
			return 1;
		}
	}
}
