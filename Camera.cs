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

        /// <summary>
        /// Distance from camera position to projective surface.
        /// </summary>
        public double Length{ get; set; }

        public Vector2 Center { get; private set; }

        /// <summary>
        /// Stores Cos' of according axe
        /// </summary>
        public Vector3 rotation;
        public Vector3 position;


        private Vector2 size;

        /// <summary>
        /// Field Of View is stored as tans of according axe.
        /// </summary>
        private Vector2 fov;



        public void SetFOV(double angleVertical)
        {
            fov.Y = Math.Tan(angleVertical / 2);
            fov.X = Math.Tan(size.X * fov.Y / size.Y);
        }
        public Vector2 GetFov()
        {
            return fov;
        }

        public Camera(Vector2 size, double angleVertical = Math.PI / 3)
        {
            Length = 1;
            this.size = size;
            rotation = new Vector3(1, 1, 1);
            fov = new Vector2(Math.Tan(angleVertical / 2), Math.Tan(size.X * Math.Tan(angleVertical / 2) / size.Y));
            position = new Vector3(-100, 0, 0);
            Center = size/2;
        }
    }
}
