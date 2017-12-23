namespace render
{
    struct Camera
    {
        public Vector Size { get; set; }
        public Vector Center { get; set; }
        public Quaternion Rotation { get; set; }


        public Camera(Vector size)
        {
            Size = size;
            Center = size / 2;
            Rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
