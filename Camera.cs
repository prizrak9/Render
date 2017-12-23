using System;

namespace render
{
    struct Camera
    {
        public Vector Size { get; set; }
        public Vector Center { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector angle;
        public Vector pos;
        public double alphaX;
        public double alphaY;


        public Camera(Vector size)
        {
            Size = size;
            Center = size / 2;
            Rotation = new Quaternion(0, 0, 0, 0);
            angle = new Vector(1, 1, 1);
            alphaX = Math.PI / 6;
            alphaY = Math.PI / 10;
            pos = new Vector(0, 0, 0);
        }
    }
}
