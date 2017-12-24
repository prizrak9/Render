﻿using System;

namespace render
{
    struct Vector3
    {
        public static Vector3 Up => new Vector3(0, 0, 1);
        public static Vector3 Left => new Vector3(0, 1, 0);
        public static Vector3 Forward => new Vector3(1, 0, 0);

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Magnitude => Math.Sqrt(SquareMagnitude);
        public double SquareMagnitude => X * X + Y * Y + Z * Z;


        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalized()
        {
            double magn = Magnitude;
            return new Vector3(X / magn, Y / magn, Z / magn);
        }

        #region Operator

        public static Vector3 operator /(Vector3 ob1, double ob2)
        {
            return new Vector3(ob1.X / ob2, ob1.Y / ob2, ob1.Z / ob2);
        }

        public static Vector3 operator *(Vector3 ob1, Vector3 ob2)
        {
            return new Vector3(ob1.X * ob2.Z - ob1.Z * ob2.Y, ob1.Z * ob2.X - ob1.X * ob2.Z, ob1.X * ob2.Y - ob1.Y * ob2.X);
        }

        public static Vector3 operator *(Vector3 ob1, double ob2)
        {
            return new Vector3(ob1.X * ob2, ob1.Y * ob2, ob1.Z * ob2);
        }

        public static Vector3 operator *(double ob1, Vector3 ob2)
        {
            return new Vector3(ob1 * ob2.X, ob1 * ob2.Y, ob1 * ob2.Z);
        }

        public static Vector3 operator -(Vector3 ob1, Vector3 ob2)
        {
            return new Vector3(ob1.X - ob2.X, ob1.Y - ob2.Y, ob1.Z - ob2.Z);
        }

        #endregion

        #region Static

        public static Vector3 FromEuler(double angX, double angY, double angZ)
        {
            return new Vector3(Math.Cos(angX), Math.Cos(angY), Math.Cos(angZ));
        }

        public static double ScalarMultiply(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector3 VectorMultiply(Vector3 a, Vector3 b)
        {
            return a * b;
        }

        public static double AngleBetween(Vector3 a, Vector3 b)
        {
            return Math.Acos(CosBetween(a, b));
        }

        public static double CosBetween(Vector3 a, Vector3 b)
        {
            return ScalarMultiply(a, b) / a.Magnitude / b.Magnitude;
        }




        public static Vector3 FlatAngleBetween(Vector3 a, Vector3 b) =>
            new Vector3(
                AngleBetween(new Vector3(a.X, 0, 0), new Vector3(b.X, 0, 0)),
                AngleBetween(new Vector3(0, a.Y, 0), new Vector3(0, b.Y, 0)),
                AngleBetween(new Vector3(0, 0, a.Z), new Vector3(0, 0, b.Z)));

        public static Vector3 FlatCosBetween(Vector3 a, Vector3 b) =>
            new Vector3(
                CosBetween(new Vector3(a.X, 0, 0), new Vector3(b.X, 0, 0)),
                CosBetween(new Vector3(0, a.Y, 0), new Vector3(0, b.Y, 0)),
                CosBetween(new Vector3(0, 0, a.Z), new Vector3(0, 0, b.Z)));




        /// <summary>
        /// It is wrong projection but we need right rotation to use the one below
        /// </summary>
        /// <param name="ob1"></param>
        /// <param name="ob2"></param>
        /// <returns></returns>
        public static Vector3 ProjectVectorToVector(Vector3 ob1, Vector3 ob2)
        {
            Vector3 flatCos = FlatCosBetween(ob1, ob2);

            return new Vector3(
                            ob1.X * flatCos.X,
                            ob1.Y * flatCos.Y,
                            ob1.Z * flatCos.Z);
        }

        /// <summary>
        /// It projects vector ob1 to vector ob2
        /// </summary>
        /// <param name="ob1"></param>
        /// <param name="ob2"></param>
        /// <returns></returns>
        //public static Vector3 ProjectVectorToVector(Vector3 ob1, Vector3 ob2)
        //{
        //    return ScalarMultiply(ob1, ob2) / ob2.Magnitude * ob2.Normalized();

        //}

        #endregion
    }
}