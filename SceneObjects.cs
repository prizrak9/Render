using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace render
{
    struct Vector
    {
        public double x, y, z;

        public static Vector operator/(Vector ob1, double ob2)
        {
            double x = ob1.x / ob2, y = ob1.y / ob2, z = ob1.z / ob2;
            return new Vector { x = x, y = y, z = z };
        }
    }

    struct Polyline
    {
        public double[] points;
    }

    struct Camera
    {
        public Vector size;
        public Vector center;

        public Camera(Vector size)
        {
            this.size = size;
            center = size / 2;
        }
    }
}
