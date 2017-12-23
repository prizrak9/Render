using System;

namespace render
{
    struct Quaternion
    {
        public double W { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        
        public Quaternion(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }


        public static Quaternion FromVector(Vector ob)
        {
            double angleBetweenVectorAndUp = Vector.AngleBetween(ob, Vector.Up);
            double s = Math.Sin(angleBetweenVectorAndUp / 2);

            return new Quaternion(Math.Cos(angleBetweenVectorAndUp / 2), ob.X * s, ob.Y * s, ob.Z * s);
        }
    }
}
