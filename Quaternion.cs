﻿using System;

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
            Vector third = ob * Vector.Up;

            return new Quaternion(Math.Cos(angleBetweenVectorAndUp / 2), third.X * s, third.Y * s, third.Z * s);
        }

        public static Quaternion FromVector(Vector ob1, Vector ob2)
        {
            double angleBetween = Vector.AngleBetween(ob1, ob2);
            double s = Math.Sin(angleBetween / 2);
            Vector third = ob1 * ob2;

            return new Quaternion(Math.Cos(angleBetween / 2), third.X * s, third.Y * s, third.Z * s);
        }
    }
}
