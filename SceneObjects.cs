using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace render
{

    public class Color
    {
        public static Color _Red => new Color(0, 0, 255, 255);
        public static Color _Green => new Color(0, 255, 0, 255);
        public static Color _Blue => new Color(255, 0, 0, 255);


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

    public class Point
    {
        public Color Color;
        public Vector3 Pos;

        public Point(Color color, Vector3 position)
        {
            Color = color;
            Pos = position;
        }
    }

    public class SceneObject
    {
        public Mesh mesh;
    }

    public abstract class Mesh
    {
        public Point[] points;
    }

    public class MeshWire : Mesh
    {
        public int[][] links;
    }

    public class MeshFilled : Mesh
    {
        public int[][] links;
    }

    public class MeshTriangled : Mesh
    {
        public int[][] facets;
    }

    //public class Polyline
    //{
    //    public double[] points;
    //}
}
