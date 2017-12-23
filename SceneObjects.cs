using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace render
{

    struct Color
    {
        public byte Blue { get; set; }
        public byte Green { get; set; }
        public byte Red { get; set; }
        public byte Alpha { get; set; }

        public Color(byte blue, byte green, byte red, byte alpha)
        {
            Blue = blue;
            Green = green;
            Red = red;
            Alpha = alpha;
        }
    }

    struct Polyline
    {
        public double[] points;
    }
}
