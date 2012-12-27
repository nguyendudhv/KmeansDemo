using System;

namespace MotionStatistic
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>

	public struct StatisticalData
	{
		//This structire containf statistical data
		//for immage
		public double mean;//mean bightnes
		public double sigma;//brightnes standard deviation
		public double K;//Kolmogorov
		public double P;//Pirson
		public double Z;//Complex

		public StatisticalData(byte mean1,byte sigma1,
			double K1,double P1,double Z1) 
		{
			mean = mean1;
			sigma = sigma1;	
			K = K1;
			P = P1;
			Z = Z1;
		}
	};
	public class Class1
	{
		public Class1()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
