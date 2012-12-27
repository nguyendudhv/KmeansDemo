using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FuzzyCMeansClustering
{
	public class ClusterCentroid //: ClusterPoint
	{
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">Centroid x-coord</param>
        /// <param name="y">Centroid y-coord</param>
        /// <param name="PixelCount">The number of pixels in the cluster, used to find the average</param>
        /// <param name="RSum">The sum of all the cluster pixels Red values, used to find the average</param>
        /// <param name="GSum">The sum of all the cluster pixels Green values, used to find the average</param>
        /// <param name="BSum">The sum of all the cluster pixels Blue values, used to find the average</param>
        /// <param name="MembershipSum">The sum of all the cluster pixels Red values, used to find the average</param>
        /// <param name="PixelColor">The sum of all the cluster pixels Red values, used to find the average</param>
        /// <param name="OriginalPixelColor">The sum of all the cluster pixels Red values, used to find the average</param>
        public double X { get; set; }
        public double Y { get; set; }
        public double PixelCount { get; set; }
        public double RSum { get; set; }
        public double GSum { get; set; }
        public double BSum { get; set; }
        public double MembershipSum { get; set; }
        public Color PixelColor { get; set; }
        public Color OriginalPixelColor { get; set; }

        public ClusterCentroid(double x, double y, Color col)
         {
            this.X = x;
            this.Y = y; 
            this.RSum = 0;
            this.GSum = 0;
            this.BSum = 0;
            this.PixelCount = 0;
            this.MembershipSum = 0;
            this.PixelColor = col;
            this.OriginalPixelColor = col;
        }
	}


}
