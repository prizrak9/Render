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
                DrawWire(ref arr, p.points, scene.camera);
                
            }

            //for(int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = 50;
            //}
            //Line(0, 0, 100, 100, ref arr, 255, 0, 0, 255, scene.camera.size);



            return arr;
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
                /*Line((int)(scene.camera.Center.X + p.points[j]),
                    (int)(scene.camera.Center.Y + p.points[j + 1]), 
                    (int)(scene.camera.Center.X + p.points[i]), 
                    (int)(scene.camera.Center.Y + p.points[i + 1]),
                    ref arr, new Color(255, 0, 0, 255), scene.camera.Size);*/
            }
        }

        void PutPixel(int i, int j, Color color, ref byte[] arr, Vector2 size)
        {
            int pos = (int)(i * size.X * depth + j * depth);
            //size_t coord = posI * bitmap.width * bitmap.depth + posJ * bitmap.depth;

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
