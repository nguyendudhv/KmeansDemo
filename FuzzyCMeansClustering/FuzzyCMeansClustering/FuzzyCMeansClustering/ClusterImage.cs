using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FuzzyCMeansClustering
{
    public class ClusterImage
    {
        public Bitmap M { get; set; }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">Centroid x-coord</param>
        /// <param name="y">Centroid y-coord</param>
        /// <param name="z">Centroid gray-level intensity</param>


        public ClusterImage(Bitmap myImage)
        // : base(z)
        {
            this.M = myImage;
        }
    }


}
