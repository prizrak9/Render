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
        public override string ToString()
        {
            return $"({X:G2};{Y:G2})";
        }

        public static Vector2 operator /(Vector2 ob1, double ob2)
        {
            return new Vector2(ob1.X / ob2, ob1.Y / ob2);
        }
        public static Vector2 operator+(Vector2 ob1, double ob2)
        {
            return new Vector2(ob1.X + ob2, ob1.Y + ob2);
        }

    }
}
