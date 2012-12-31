namespace EyeOpen.SimilarImagesFinder.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Provide a graph control to plot curves.
    /// </summary>
    internal partial class Graph : PictureBox
    {
        private List<PlotCurve> plotCurves;

        public Graph()
        {
            InitializeComponent();

            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;
        }

        private IList<PlotCurve> PlotCurves
        {
            get
            {
                if (plotCurves == null)
                {
                    plotCurves = new List<PlotCurve>();
                }

                return plotCurves;             
            }
        }

        /// <summary>
        /// Add a new curve to draw.
        /// </summary>
        /// <param name="plotColor">The color to use for curve plotting.</param>
        /// <param name="curve">The curve to plot.</param>
        public void AddPlotCurve(Color plotColor, double[] curve) 
        {
            PlotCurves.Add(new PlotCurve(plotColor, curve));
        }

        /// <summary>
        /// Remove all curves to draw.
        /// </summary>
        public void ClearCurves()
        {
            PlotCurves.Clear();    
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((this.PlotCurves == null) || (this.PlotCurves.Count == 0))
            {
                return;
            }

            var gr = e.Graphics;            
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            foreach (var plotCurve in PlotCurves) 
            {
                var adaptedCurve = CalculateCurveFromPoints(plotCurve.Curve, Width, Height);
                gr.DrawCurve(new Pen(plotCurve.PlotColor), adaptedCurve);
            }
        }

        private static Point[] CalculateCurveFromPoints(double[] points, int width, int height)
        {
            var horizontalIncrement = (double)width / (double)points.Length;
            double currentX = 0;
            var curve = new List<Point>();

            height -= 4;         

            foreach (var t in points)
            {
                var y = height - ((int)Math.Round(t * height)) + 2;
                var x = (int)Math.Round(currentX);
                curve.Add(new Point(x, y));                
                currentX += horizontalIncrement;
            }

            return curve.ToArray();
        }
    }
}
