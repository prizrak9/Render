using System;

namespace render
{
    struct Quaternion
    {
        public double s { get; set; }
        public Vector3 v { get; set; }

        public double SquareMagnitude => s * s + v.SquareMagnitude;
        public double Magnitude => Math.Sqrt(SquareMagnitude);
        public Quaternion Conjugate => new Quaternion(s, -1 * v);
        public Quaternion Inverse => Conjugate / SquareMagnitude;

        public Quaternion(double w, double x, double y, double z)
        {
            s = w;
            v = new Vector3(x, y, z);
        }

        public Quaternion(double ss, Vector3 vv)
        {
            s = ss;
            v = new Vector3(vv.X, vv.Y, vv.Z);
        }

        public Quaternion(Vector3 vv)
        {
            s = 0;
            v = new Vector3(vv.X, vv.Y, vv.Z);
        }

        #region Operator

        public static Quaternion operator +(Quaternion ob1, Quaternion ob2)
        {
            return new Quaternion(ob1.s + ob2.s, ob1.v + ob2.v);
        }

        public static Quaternion operator *(Quaternion ob1, Quaternion ob2)
        {
            return new Quaternion(ob1.s * ob2.s - Vector3.ScalarMultiply(ob1.v, ob2.v), ob1.s * ob2.v + ob2.s * ob1.v + ob1.v * ob2.v);
        }

        public static Quaternion operator /(Quaternion ob, double a)
        {
            return new Quaternion(ob.s / a, ob.v / a);
        }

        #endregion

        #region Static

        public static Vector3 Rotate(Vector3 v, Quaternion q)
        {
            return (q * new Quaternion(v) * q.Inverse).v;
        }

        #endregion


        /*
         public static Quaternion FromVector(Vector3 ob)
        {
            double angleBetweenVectorAndUp = Vector3.AngleBetween(ob, Vector3.Up);
            double s = Math.Sin(angleBetweenVectorAndUp / 2);
            Vector3 third = ob * Vector3.Up;

            return new Quaternion(Math.Cos(angleBetweenVectorAndUp / 2), third.X * s, third.Y * s, third.Z * s);
        }

        public static Quaternion FromVector(Vector3 ob1, Vector3 ob2)
        {
            double angleBetween = Vector3.AngleBetween(ob1, ob2);
            double s = Math.Sin(angleBetween / 2);
            Vector3 third = ob1 * ob2;

            return new Quaternion(Math.Cos(angleBetween / 2), third.X * s, third.Y * s, third.Z * s);
        }
        */
    }
}
