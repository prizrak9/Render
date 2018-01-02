using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace render
{

    class Color
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

    public class SceneObject
    {
        public Mesh mesh;
    }

    public abstract class Mesh
    {
        public double[] points;
    }

    public class MeshWire : Mesh
    {
        public int[][] links;
    }

    public class MeshFilled : Mesh
    {
        public int[][] links;
    }

    //public class Polyline
    //{
    //    public double[] points;
    //}
}
