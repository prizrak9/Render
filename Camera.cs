﻿using System;

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
                UpdateFOV();
            }
        }

        /// <summary>
        /// Length is a distance from camera position to its camera view or
        /// its projection surface. It is an important part of camera settings,
        /// otherwise we cannot be sure points we want to be rendered is really rendered.
        /// In other words we should keep a distance from the camera to avoid multiple
        /// possible glitches.
        /// </summary>
        public double Length{ get; set; }

        public Vector2 Center { get; private set; }

        /// <summary>
        /// Stores Cos' of according axe
        /// </summary>
        public Vector3 rotation;
        public Vector3 position;

        public Vector3 forward;
        public Vector3 up;

        private Vector2 size;

        /// <summary>
        /// Field Of View is stored as tans of according axe.
        /// </summary>
        private Vector2 fov;

        public void RotateV(double angle)
        {
            Quaternion q = new Quaternion(Math.Cos(angle / 2), Math.Sin(angle / 2) * (forward * up));
            up = Quaternion.Rotate(up, q).Normalized();
            forward = Quaternion.Rotate(forward, q).Normalized();
            Console.WriteLine($"Cam FW magn {forward.Magnitude}\nCam UP magn {up.Magnitude}");
        }

        public void RotateH(double angle)
        {
            Quaternion q = new Quaternion(Math.Cos(angle / 2), Math.Sin(angle / 2) * up);
            forward = Quaternion.Rotate(forward, q).Normalized();
            Console.WriteLine($"Cam FW magn {forward.Magnitude}\nCam UP magn {up.Magnitude}");
        }


        public void UpdateFOV()
        {
            fov.X = size.X * fov.Y / size.Y;
        }
        public void SetFOV(double angleVertical)
        {
            fov.Y = Math.Tan(angleVertical / 2);
            fov.X = size.X * fov.Y / size.Y;
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
            forward = new Vector3(1, 0, 0);
            up = new Vector3(0, 0, 1);
        }
    }
}
