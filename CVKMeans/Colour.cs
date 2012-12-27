using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageProcessor
{
    public unsafe class Colour
    {
        public enum Types { RGB, HSV};

        public static PixelData GetPixelData(BGRA* pixel, Types model)
        {
            Color clr = Color.FromArgb(pixel->red, pixel->green, pixel->blue);   
            PixelData pd = new PixelData(0, 0, 0, clr.Name);
            switch (model)
            {
                case Types.RGB:
                    pd.Ch1 = pixel->red;
                    pd.Ch2 = pixel->green;
                    pd.Ch3 = pixel->blue;
                    break;
                case Types.HSV:
                    pd = RGBToHSV(pixel);
                    break;
                default:
                    throw new Exception("Conversion not implemented");
            }
            pd.Name = clr.Name;
            return pd;
        }

        public static PixelData GetPixelData(int R, int G, int B, Types model)
        {
            Color clr = Color.FromArgb(R, G, B);
            PixelData pd = new PixelData(0, 0, 0, clr.Name);
            switch (model)
            {
                case Types.RGB:
                    pd.Ch1 = R;
                    pd.Ch2 = G;
                    pd.Ch3 = B;
                    break;
                case Types.HSV:
                    pd = RGBToHSV(R, G, B);
                    break;
                default:
                    throw new Exception("Conversion not implemented");
            }
            pd.Name = clr.Name;
            return pd;
        }

        public static PixelData RGBToHSV(BGRA* pixel)
        {
            return RGBToHSV(pixel->red, pixel->green, pixel->blue);            
        }

        public static PixelData RGBToHSV(int R, int G, int B)
        {
            // In this function, R, G, and B values must be scaled 
            // to be between 0 and 1.
            // HSV.Hue will be a value between 0 and 360, and 
            // HSV.Saturation and value are between 0 and 1.
            PixelData pd = new PixelData();
            double min;
            double max;
            double delta;

            double r = (double)R / 255;
            double g = (double)G / 255;
            double b = (double)B / 255;

            double h;
            double s;
            double v;

            min = Math.Min(Math.Min(r, g), b);
            max = Math.Max(Math.Max(r, g), b);
            v = max;
            delta = max - min;
            if (max == 0 || delta == 0)
            {
                // R, G, and B must be 0, or all the same.
                // In this case, S is 0, and H is undefined.
                // Using H = 0 is as good as any...
                s = 0;
                h = 0;
            }
            else
            {
                s = delta / max;
                if (r == max)
                {
                    // Between Yellow and Magenta
                    h = (g - b) / delta;
                }
                else if (g == max)
                {
                    // Between Cyan and Yellow
                    h = 2 + (b - r) / delta;
                }
                else
                {
                    // Between Magenta and Cyan
                    h = 4 + (r - g) / delta;
                }

            }
            // Scale h to be between 0 and 360. 
            // This may require adding 360, if the value
            // is negative.
            h *= 60;
            if (h < 0)
            {
                h += 360;
            }

            pd.Ch1 = (float)h;
            pd.Ch2 = (float)s;
            pd.Ch3 = (float)v;

            //adjust the channel range to be between 0 - 255
            pd.Ch1 = pd.Ch1 * 255 / 360;
            pd.Ch2 = pd.Ch2 * 255;
            pd.Ch3 = pd.Ch3 * 255;

            return pd;
        }

        private static float Min(float r, float g, float b)
        {
            float m = Math.Min(r, g);
            m = Math.Min(m, b);
            return m;
        }

        private static float Max(float r, float g, float b)
        {
            float m = Math.Max(r, g);
            m = Math.Max(m, b);
            return m;
        }

        private static float Min(BGRA* pixel)
        {
            return Min((float)pixel->red, (float)pixel->green, (float)pixel->blue);
        }

        private static float Max(BGRA* pixel)
        {
            return Max((float)pixel->red, (float)pixel->green, (float)pixel->blue);
        }

    }
}
