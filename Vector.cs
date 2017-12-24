using System;

namespace render
{
    struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Vector2 operator /(Vector2 ob1, double ob2) =>
            new Vector2(ob1.X / ob2, ob1.Y / ob2);

        public static Vector2 operator+(Vector2 ob1, double ob2)
        {
            return new Vector2(ob1.X + ob2, ob1.Y + ob2);
        }

    }

    struct Vector3
    {
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

        public Vector3 SubstructFrom(Vector3 ob) =>
            ob - this;



        public static Vector3 Up => new Vector3(0, 0, 1);
        public static Vector3 Left => new Vector3(0, 1, 0);
        public static Vector3 Forward => new Vector3(1, 0, 0);


        public static Vector3 operator /(Vector3 ob1, double ob2) =>
             new Vector3(ob1.X / ob2, ob1.Y / ob2, ob1.Z / ob2);

        public static Vector3 operator *(Vector3 ob1, Vector3 ob2) =>
            new Vector3(ob1.X * ob2.Z - ob1.Z * ob2.Y, ob1.Z * ob2.X - ob1.X * ob2.Z, ob1.X * ob2.Y - ob1.Y * ob2.X);

        public static Vector3 operator -(Vector3 ob1, Vector3 ob2) =>
            new Vector3(ob1.X - ob2.X, ob1.Y - ob2.Y, ob1.Z - ob2.Z);


        public static Vector3 FromEuler(double angX, double angY, double angZ) =>
            new Vector3(Math.Cos(angX), Math.Cos(angY), Math.Cos(angZ));

        public static double ScalarMultiply(Vector3 a, Vector3 b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Vector3 VectorMultiply(Vector3 a, Vector3 b) =>
            a * b;


        public static double AngleBetween(Vector3 a, Vector3 b)
        {
            return Math.Acos(ScalarMultiply(a, b) / a.Magnitude / b.Magnitude);
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

        public static Vector3 ProjectVectorToVector(Vector3 ob1, Vector3 ob2)
        {
            //Vector3 flatAngle = FlatAngleBetween(ob1, ob2);
            Vector3 flatCos = FlatCosBetween(ob1, ob2);

            return new Vector3(
                            ob1.X * flatCos.X,
                            ob1.Y * flatCos.Y,
                            ob1.Z * flatCos.Z);

            //return new Vector3(
            //    ob1.X / Math.Cos(flatAngle.X),
            //    ob1.Y / Math.Cos(flatAngle.Y),
            //    ob1.Z / Math.Cos(flatAngle.Z));
        }

    }
}
