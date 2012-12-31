namespace EyeOpen.SimilarImagesFinder.Windows
{
    using System.Drawing;

    /// <summary>
    /// Defines a curve and its plot color.
    /// </summary>
    internal class PlotCurve
    {
        private readonly Color plotColor;
        private readonly double[] curve;

        public PlotCurve(Color plotColor, double[] curve)
        {
            this.plotColor = plotColor;
            this.curve = curve;
        }

        public Color PlotColor
        {
            get
            {
                return plotColor;
            }
        }

        public double[] Curve
        {
            get
            {
                return this.curve;
            }
        }
    }
}