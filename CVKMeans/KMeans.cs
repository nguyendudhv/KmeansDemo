using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;

using ImageProcessor;

namespace KMeans
{
    public unsafe class KMeans
    {
        public class Distance
        {
            public Distance(float d) { _d = d; }
            public float Measure
            {
                get { return _d; }
                set { _d = value; }
            }
            private float _d;
        }

        public class Cluster
        {
            public Cluster(float R, float G, float B) 
            {
                _centroid1 = R;
                _centroid2 = G;
                _centroid3 = B;
            }
            public float CentroidR
            {
                get { return _centroid1; }
                set { _centroid1 = value; }
            }
            public float CentroidG
            {
                get { return _centroid2; }
                set { _centroid2 = value; }
            }
            public float CentroidB
            {
                get { return _centroid3; }
                set { _centroid3 = value; }
            }
            private float _centroid1;
            private float _centroid2;
            private float _centroid3;
        }

        public KMeans(Bitmap bmp, int numCluster, Colour.Types model) 
        {
            _image = (Bitmap)bmp.Clone();
            _processedImage = (Bitmap)bmp.Clone();
            _model = model;

            _previousCluster = new Dictionary<string, Cluster>();
            _currentCluster = new Dictionary<string, Cluster>();
            FindTopXColours(numCluster); //find top X colours in the image
            //create clusters for top X colours
            for (int i = 0; i < _topColours.Length; i++)
            {
                PixelData pd = Colour.GetPixelData(_topColours[i].R, _topColours[i].G, _topColours[i].B, model);
                _previousCluster.Add(_topColours[i].Name, new Cluster(pd.Ch1, pd.Ch2, pd.Ch3));
                _currentCluster.Add(_topColours[i].Name, new Cluster(pd.Ch1, pd.Ch2, pd.Ch3));
            }
        }

        public Bitmap ProcessedImage { get { return _processedImage; } }

        public bool Converged { get { return _converged; } }

        public void Iterate()
        {
            _colourClusterAllocation = new Hashtable(); //for keeping track of colour<->cluster allocation
            _pixelDataClusterAllocation = new Hashtable();
            _clusterColours = new Hashtable();

            UnsafeBitmap fastBitmap = new UnsafeBitmap(_image);
            fastBitmap.LockBitmap();
            Point size = fastBitmap.Size;
            BGRA* pPixel;

            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    PixelData pd = Colour.GetPixelData(pPixel, _model);
                    AllocateToCluster(pd);                    
                    //increment the pointer
                    pPixel++;
                }
            }
            fastBitmap.UnlockBitmap();

            CalculateClusterCentroids();

            _processedImage = (Bitmap)_image.Clone();

            //segment the image based on the cluster
            fastBitmap = new UnsafeBitmap(_processedImage);
            fastBitmap.LockBitmap();
            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    PixelData pd = Colour.GetPixelData(pPixel, _model);
                    Color newClr = (Color)_clusterColours[pd.Name];
                    pPixel->red = newClr.R;
                    pPixel->green = newClr.G;
                    pPixel->blue = newClr.B;
                   //increment the pointer
                    pPixel++;
                }
            }

            fastBitmap.UnlockBitmap();

            CheckConvergence();

        }

        private void CheckConvergence()
        {
            //if current and previous cluster centroids are the same then converged
            bool match = true;
            foreach (KeyValuePair<string, Cluster> cluster in _currentCluster)
            {
                if (((int)cluster.Value.CentroidR != (int)_previousCluster[cluster.Key].CentroidR)
                    && ((int)cluster.Value.CentroidG != (int)_previousCluster[cluster.Key].CentroidG)
                    && ((int)cluster.Value.CentroidB != (int)_previousCluster[cluster.Key].CentroidB))
                {
                    match = false;
                    break;
                }
            }
            if (!match)
            {
                foreach (KeyValuePair<string, Cluster> cluster in _currentCluster)
                {
                    _previousCluster[cluster.Key].CentroidR=cluster.Value.CentroidR;
                        _previousCluster[cluster.Key].CentroidG=cluster.Value.CentroidG;
                        _previousCluster[cluster.Key].CentroidB = cluster.Value.CentroidB;                    
                }
            }
            _converged = match;
        }

        private void CalculateClusterCentroids()
        {
            foreach (KeyValuePair<string, Cluster> cluster in _currentCluster)
            {  
                List<PixelData> clrList = (List<PixelData>)_pixelDataClusterAllocation[cluster.Key];
                float cR = 0;
                float cG = 0;
                float cB = 0;
                foreach (PixelData clr in clrList)
                {
                    cR += clr.Ch1;
                    cG += clr.Ch2;
                    cB += clr.Ch3;

                    if (!_clusterColours.ContainsKey(clr.Name))
                    {
                        _clusterColours.Add(clr.Name, Color.FromArgb((int)cluster.Value.CentroidR, (int)cluster.Value.CentroidG, (int)cluster.Value.CentroidB));                        
                    }
                }
                float count = clrList.Count + 1; //total of colours plus 1 for the existing centroid
                cluster.Value.CentroidR = (cluster.Value.CentroidR + cR) / count; //average to find new centroid
                cluster.Value.CentroidG = (cluster.Value.CentroidG + cG) / count;
                cluster.Value.CentroidB = (cluster.Value.CentroidB + cB) / count;
            }
        }

        private void AllocateToCluster(PixelData pd)
        {
            //find distance of this colour from each cluster centroid
            Dictionary<string, Distance> distances = new Dictionary<string, Distance>();

            foreach (KeyValuePair<string, Cluster> c in _currentCluster)
            {
                float d = (float)Math.Sqrt(
                    (double)Math.Pow((c.Value.CentroidR - pd.Ch1), 2) +
                    (double)Math.Pow((c.Value.CentroidG - pd.Ch2), 2) +
                    (double)Math.Pow((c.Value.CentroidB - pd.Ch3), 2)
                );
                distances.Add(c.Key, new Distance(d));
            }

            //allocate this colour to the closest cluster based on distance
            List<KeyValuePair<string, Distance>> list = new List<KeyValuePair<string, Distance>>();
            list.AddRange(distances);

            list.Sort(delegate(KeyValuePair<string, Distance> kvp1, KeyValuePair<string, Distance> kvp2)
            { return Comparer<float>.Default.Compare(kvp1.Value.Measure, kvp2.Value.Measure); });

            //assign to closest cluster
            if (_pixelDataClusterAllocation.ContainsKey(list[0].Key))
            {
                ((List<PixelData>)_pixelDataClusterAllocation[list[0].Key]).Add(pd);
            }
            else
            {
                List<PixelData> clrList = new List<PixelData>();
                clrList.Add(pd);
                _pixelDataClusterAllocation.Add(list[0].Key, clrList);
            }
        }

        private void FindTopXColours(int numColours)
        {
            Dictionary<string, ColourCount> colours = new Dictionary<string, ColourCount>();
            UnsafeBitmap fastBitmap = new UnsafeBitmap(_image);
            fastBitmap.LockBitmap();
            Point size = fastBitmap.Size;
            BGRA* pPixel;

            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    //get the bin index for the current pixel colour
                    Color clr = Color.FromArgb(pPixel->red, pPixel->green, pPixel->blue);

                    if (colours.ContainsKey(clr.Name))
                    {
                        ((ColourCount)colours[clr.Name]).Count++;
                    }
                    else
                        colours.Add(clr.Name, new ColourCount(clr, 1));

                    //increment the pointer
                    pPixel++;
                }
            }

            fastBitmap.UnlockBitmap();

            //instantiate using actual colours found - which might be less than numColours
            if (colours.Count < numColours)
                numColours = colours.Count;

            _topColours = new Color[numColours];

            List<KeyValuePair<string, ColourCount>> summaryList = new List<KeyValuePair<string, ColourCount>>();
            summaryList.AddRange(colours);

            summaryList.Sort(delegate(KeyValuePair<string, ColourCount> kvp1, KeyValuePair<string, ColourCount> kvp2)
            { return Comparer<int>.Default.Compare(kvp2.Value.Count, kvp1.Value.Count); });


            for (int i = 0; i < _topColours.Length; i++)
            {
                _topColours[i] = Color.FromArgb(summaryList[i].Value.Colour.R, summaryList[i].Value.Colour.G, summaryList[i].Value.Colour.B);
            }
        }

        /*private Color[] _topColours;
        private Colour.Types _model;
        private Dictionary<string, Cluster> _previousCluster;
        private Dictionary<string, Cluster> _currentCluster;
        private Hashtable _colourClusterAllocation;
        private Hashtable _pixelDataClusterAllocation;
        private Hashtable _clusterColours;
        private bool _converged = false;
        private Bitmap _image;
        private Bitmap _processedImage;*/
        public Color[] _topColours;
        public Colour.Types _model;
        public Dictionary<string, Cluster> _previousCluster;
        public Dictionary<string, Cluster> _currentCluster;
        public Hashtable _colourClusterAllocation;
        public Hashtable _pixelDataClusterAllocation;
        public Hashtable _clusterColours;
        public bool _converged = false;
        public Bitmap _image;
        public Bitmap _processedImage;
    }
}
