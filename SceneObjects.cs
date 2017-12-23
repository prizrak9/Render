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
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Magnitude => Math.Sqrt(SquareMagnitude);
        public double SquareMagnitude => X * X + Y * Y + Z * Z;


        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector Up => new Vector(0, 0, 1);
        public static Vector Left => new Vector(0, 1, 0);
        public static Vector Forward => new Vector(1, 0, 0);


        public static Vector operator /(Vector ob1, double ob2) =>
             new Vector(ob1.X / ob2, ob1.Y / ob2, ob1.Z / ob2);

        public static Vector operator *(Vector ob1, Vector ob2) =>
            new Vector(ob1.X * ob2.Z - ob1.Z * ob2.Y, ob1.Z * ob2.X - ob1.X * ob2.Z, ob1.X * ob2.Y - ob1.Y * ob2.X);

        public static Vector FromEuler(double angX, double angY, double angZ) =>
            new Vector(Math.Cos(angX), Math.Cos(angY), Math.Cos(angZ));

        public static double ScalarMultiply(Vector a, Vector b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Vector VectorMultiply(Vector a, Vector b) =>
            a * b;


        public static double AngleBetween(Vector a, Vector b) =>
            Math.Acos(ScalarMultiply(a, b) / a.Magnitude / b.Magnitude);


    }

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
