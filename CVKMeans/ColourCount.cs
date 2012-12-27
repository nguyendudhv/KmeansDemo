using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageProcessor
{
    public class ColourCount
    {
        public ColourCount() { }

        public ColourCount(Color clr, int count)
        {
            _colour = clr;
            _count = count;
        }

        public Color Colour
        {
            get {return _colour;}
            set {_colour = value; }
        }

        public int Count
        {
            get {return _count;}
            set {_count = value; }
        }

        private Color _colour;
        private int _count;
    }
}
