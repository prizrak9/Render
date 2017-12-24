using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace render
{
    class Render
    {
        const int depth = 4;

        public byte[] GetImage(Scene scene)
        {
            // Bgra32
            byte[] arr = new byte[(int)(scene.camera.Size.X * scene.camera.Size.Y * depth)];

            foreach(Polyline p in scene.polyLines)
            {
                DrawWire2(ref arr, p.points, scene.camera);
            }

            //for(int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = 50;
            //}
            //Line(0, 0, 100, 100, ref arr, 255, 0, 0, 255, scene.camera.size);



            return arr;
        }

        Vector3 RotateToCameraRotation(Vector3 position, Vector3 cameraRotation)
        {
            Vector3 local = Vector3.ProjectVectorToVector(position, cameraRotation);
            return new Vector3(
                double.IsNaN(local.X) ? 0 : (position.X < 0 ? local.X : -local.X),
                double.IsNaN(local.Y) ? 0 : (position.Y < 0 ? local.Y : -local.Y), 
                double.IsNaN(local.Z) ? 0 : (position.Z < 0 ? local.Z : -local.Z));
        }

        Vector3 GetLocalPosition(Vector3 position, Vector3 cameraPosition)
        {
            return position - cameraPosition;
        }

        Vector2 GetFlatTan(Vector3 localPosition, Vector2 cameraFov, double cameraLength)
        {
            return new Vector2(localPosition.Y  / localPosition.X, localPosition.Z  / localPosition.X);
        }

        Vector2 GetCameraViewposition(Vector2 tan, Vector2 fov, Vector3 localPosition)
        {
            Vector2 value = new Vector2((fov.X - tan.X) / 2 / fov.X, (fov.Y - tan.Y) / 2 / fov.Y);
            //value += 0.5;

            //if (localPosition.Y < 0)
            //    value.X += 0.5;

            //if (localPosition.Z < 0)
            //    value.Y += 0.5;

            return value;
        }

        /// <summary>
        /// Transforms position in cameraView to position in bitmap.
        /// Also reflects Y axe.
        /// </summary>
        /// <param name="cameraViewPosition">Position in cameraView [0;1].</param>
        /// <param name="cameraResolution">Bitmap size.</param>
        /// <returns>Coordinates in bitmap</returns>
        Vector2 GetBitmapPosition(Vector2 cameraViewPosition, Vector2 cameraResolution)
        {
            return new Vector2(cameraViewPosition.X * cameraResolution.X, cameraResolution.Y - cameraViewPosition.Y * cameraResolution.Y);
        }

        void DrawWire2(ref byte[] buffer, double[] points, Camera camera)
        {
            Vector3 point0, point1;
            Vector3 localPosition0, localPosition1;
            Vector2 flatTan0, flatTan1;
            Vector2 cameraViewPosition0, cameraViewPosition1;
            Vector2 bitmapPosition0, bitmapPosition1;

            for (int i = 3, j = 0; i < points.Length; i += 3, j += 3)
            {
                // Get points of line.
                point0 = new Vector3(points[j], points[j + 1], points[j + 2]);
                point1 = new Vector3(points[i], points[i + 1], points[i + 2]);

                // Get local position relative to camera.
                localPosition0 = GetLocalPosition(point0, camera.position);
                localPosition1 = GetLocalPosition(point1, camera.position);

                // Project local position to camera view.
                point0 = RotateToCameraRotation(localPosition0, camera.rotation);
                point1 = RotateToCameraRotation(localPosition1, camera.rotation);

                // If point is located behind camera view than skip this line.
                if (point0.X >= -camera.Length || point1.X >= -camera.Length) continue;

                // Get tan of local position relative to camera fov.
                flatTan0 = GetFlatTan(point0, camera.GetFov(), camera.Length);
                flatTan1 = GetFlatTan(point1, camera.GetFov(), camera.Length);

                // Transform local position to camera view position (transform local from [-inf;+inf] to [0;1]).
                cameraViewPosition0 = GetCameraViewposition(flatTan0, camera.GetFov(), localPosition0);
                cameraViewPosition1 = GetCameraViewposition(flatTan1, camera.GetFov(), localPosition1);

                // Transform camera view position to bitmap position (camera view position*height(*width)).
                bitmapPosition0 = GetBitmapPosition(cameraViewPosition0, camera.Size);
                bitmapPosition1 = GetBitmapPosition(cameraViewPosition1, camera.Size);

                // Build line.
                Line((int)bitmapPosition0.X, (int)bitmapPosition0.Y, (int)bitmapPosition1.X, (int)bitmapPosition1.Y, ref buffer, new Color(255, 0, 0, 255), camera.Size);
            }
        }

        void DrawWire(ref byte[] buffer, double[] points, Camera camera)
        {
            double dx, dy, dz;
            double x1, x2, y1, y2;
            double centX = camera.Center.X, centY = camera.Center.Y;
            double fovX = camera.GetFov().X, fovY = camera.GetFov().Y;

            for (int i = 3, j = 0; i < points.Length; i += 3, j += 3)
            {
                dx = points[j] - camera.position.X;
                dy = points[j + 1] - camera.position.Y;
                dz = points[j + 2] - camera.position.Z;

                if (dz > 0)
                {
                    x1 = centX / dz / fovX * dx;
                    x2 = centX / points[i + 2] / fovX * points[i];
                    y1 = centY / dz / fovY * dy;
                    y2 = centY / points[i + 2] / fovY * points[i + 1];
                    Line((int)(centX + x1), (int)(centY + y1),
                        (int)(centX + x2), (int)(centY + y2),
                        ref buffer, new Color(255, 0, 0, 255), camera.Size);
                }
            }
        }

        void PutPixel(int i, int j, Color color, ref byte[] arr, Vector2 size)
        {
            int pos = (int)(i * size.X * depth + j * depth);

            arr[pos] = color.Blue;
            arr[++pos] = color.Green;
            arr[++pos] = color.Red;
            arr[++pos] = color.Alpha;
        }

        void Line(int x0, int y0, int x1, int y1, ref byte[] arr, Color color, Vector2 size)
        {
            for (double t = 0.0; t < 1.0; t += 0.01)
            {
                int x = (int)(x0 * (1.0 - t) + x1 * t);
                int y = (int)(size.Y - (y0 * (1.0 - t) + y1 * t)) - 1;
                if (!(y < 0 || y >= size.Y || x < 0 || x > size.X))
                    PutPixel(y, x, color, ref arr, size);
            }
        }
    }
}
