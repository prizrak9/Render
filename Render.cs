using System;

namespace render
{

    class Render
    {
        const int depth = 4;

        public byte[] GetImage(Scene scene)
        {
            // Bgra32
            byte[] arr = new byte[(int)(scene.camera.Size.X * scene.camera.Size.Y * depth)];

            foreach (SceneObject p in scene.objects)
            {
                DrawWire4(ref arr, p, scene.camera);
            }

            return arr;
        }

        

        Vector3 ProjectToCameraViewSurface(Vector3 position, Vector3 cameraFW, Vector3 cameraUP)
        {
            return new Vector3(Vector3.ProjectVectorToAxe(position, cameraFW), 
                               Vector3.ProjectVectorToAxe(position, cameraUP * cameraFW), 
                               Vector3.ProjectVectorToAxe(position, cameraUP));
        }

        Vector3 GetLocalPosition(Vector3 position, Vector3 cameraPosition)
        {
            return position - cameraPosition;
        }

        Vector2 GetFlatTan(Vector3 localPosition)
        {
            return new Vector2(localPosition.Y  / localPosition.X, localPosition.Z  / localPosition.X);
        }

        Vector2 GetCameraViewPosition(Vector2 tan, Vector2 fov)
        {
            return new Vector2((fov.X - tan.X) / 2 / fov.X, (fov.Y - tan.Y) / 2 / fov.Y);
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
            return new Vector2(cameraViewPosition.X * cameraResolution.X, 
                cameraResolution.Y - cameraViewPosition.Y * cameraResolution.Y);
        }

        void DrawWire3(ref byte[] buffer, double[] points, Camera camera)
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
                point0 = ProjectToCameraViewSurface(localPosition0, camera.forward, camera.up);
                point1 = ProjectToCameraViewSurface(localPosition1, camera.forward, camera.up);
                

                // If line is located behind or crosses camera view then skip this line.
                // Length is a distance from camera position to its camera view or
                // its projection surface. It is an important part of camera settings,
                // otherwise we cannot be sure points we want to be rendered is really rendered.
                // In other words we should keep a distance from the camera to avoid multiple
                // possible glitches.
                if (point0.X <= camera.Length || point1.X <= camera.Length) continue;

                // Get tan of local position relative to camera fov.
                flatTan0 = GetFlatTan(point0);
                flatTan1 = GetFlatTan(point1);

                // Transform local position to camera view position 
                // (transform local from [-inf;+inf] to [0;1]).
                cameraViewPosition0 = GetCameraViewPosition(flatTan0, camera.GetFov());
                cameraViewPosition1 = GetCameraViewPosition(flatTan1, camera.GetFov());

                // Transform camera view position to bitmap position 
                // (camera view position*height(*width)).
                bitmapPosition0 = GetBitmapPosition(cameraViewPosition0, camera.Size);
                bitmapPosition1 = GetBitmapPosition(cameraViewPosition1, camera.Size);

                // Build line.
                Line((int)bitmapPosition0.X, (int)bitmapPosition0.Y,
                    (int)bitmapPosition1.X, (int)bitmapPosition1.Y,
                    ref buffer, new Color(255, 0, 0, 255), camera.Size);
            }
        }

        void DrawWire4(ref byte[] buffer, SceneObject obj, Camera camera)
        {
            Vector3 point0;
            Vector3 localPosition0;
            Vector2 flatTan0;
            Vector2 cameraViewPosition0;

            Vector2[] temp = new Vector2[(obj.mesh.points.Length)/3];

            for (int i = 0; i < obj.mesh.points.Length; i += 3)
            {
                point0 = new Vector3(obj.mesh.points[i], obj.mesh.points[i + 1], obj.mesh.points[i + 2]);
                localPosition0 = GetLocalPosition(point0, camera.position);
                point0 = ProjectToCameraViewSurface(localPosition0, camera.forward, camera.up);

                if (point0.X <= camera.Length) continue;

                flatTan0 = GetFlatTan(point0);
                cameraViewPosition0 = GetCameraViewPosition(flatTan0, camera.GetFov());
                temp[i/3] = GetBitmapPosition(cameraViewPosition0, camera.Size);
            }

            int[][] links = (obj.mesh as MeshWire).links;

            for(int i = 0; i < links.GetLength(0); i++)
            {
                Vector2 p1 = temp[links[i][0]], p2 = temp[links[i][1]];
                if(p1!=null && p2 != null)
                {
                    if(!(p1.X < 0 && p2.X < 0 || p1.X > camera.Size.X && p2.X > camera.Size.X ||
                        p1.Y < 0 && p2.Y < 0 || p1.Y > camera.Size.Y && p2.Y > camera.Size.Y))
                    {
                        Line((int)p1.X, (int)p1.Y,
                            (int)p2.X, (int)p2.Y,
                            ref buffer, new Color(255, 0, 0, 255), camera.Size);
                    }

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

            double step = 1 / Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
            for (double t = 0.0; t < 1.0; t = t+step > 1.0 ? 1.0 : t + step)
            {
                int x = (int)(x0 * (1.0 - t) + x1 * t);
                int y = (int)(size.Y - (y0 * (1.0 - t) + y1 * t)) - 1;

                if (!(y < 0 || y >= size.Y || x < 0 || x >= size.X))
                {
                    PutPixel(y, x, color, ref arr, size);
                }
            }
        }
    }
}
