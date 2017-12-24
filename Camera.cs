using System;

namespace render
{
    struct Camera
    {
        /// <summary>
        /// Resolution of camera. Also updates Center position.
        /// </summary>
        public Vector2 Size
        {
            get => size;
            set
            {
                Center = value / 2;
                size = value;
            }
        }
        public Vector2 Center { get; private set; }

        public Vector3 rotation;
        public Vector3 position;


        private Vector2 size;

        /// <summary>
        /// Field Of View is stored as tan of according axe.
        /// </summary>
        private Vector2 fov;



        public void SetFOV(double angleVertical)
        {
            fov.Y = Math.Tan(angleVertical);
            fov.X = Math.Tan(size.X * fov.Y / size.Y);
        }
        public Vector2 GetFov()
        {
            return fov;
        }

        public Camera(Vector2 size, double angleVertical = Math.PI / 6)
        {
            this.size = size;
            rotation = new Vector3(1, 1, 1);
            fov = new Vector2(Math.Tan(angleVertical), Math.Tan(size.X * Math.Tan(angleVertical) / size.Y));
            position = new Vector3(0, 0, 0);
            Center = size/2;
        }
    }
}
