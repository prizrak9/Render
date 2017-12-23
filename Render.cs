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
            byte[] arr = new byte[(int)(scene.camera.size.X * scene.camera.size.Y * depth)];

            foreach(Polyline p in scene.polyLines)
            {
                for(int i = 3, j = 0; i < p.points.Length; i+=3, j+=3)
                {

                    Line((int)(scene.camera.center.X + p.points[j]),
                        (int)(scene.camera.center.Y + p.points[j + 1]), 
                        (int)(scene.camera.center.X + p.points[i]), 
                        (int)(scene.camera.center.Y + p.points[i + 1]),
                        ref arr, new Color(255, 0, 0, 255), scene.camera.size);
                }
            }

            //for(int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = 50;
            //}
            //Line(0, 0, 100, 100, ref arr, 255, 0, 0, 255, scene.camera.size);



            return arr;
        }

        void PutPixel(int i, int j, Color color, ref byte[] arr, Vector size)
        {
            int pos = (int)(i * size.X * depth + j * depth);
            //size_t coord = posI * bitmap.width * bitmap.depth + posJ * bitmap.depth;

            arr[pos] = color.Blue;
            arr[++pos] = color.Green;
            arr[++pos] = color.Red;
            arr[++pos] = color.Alpha;
        }

        void Line(int x0, int y0, int x1, int y1, ref byte[] arr, Color color, Vector size)
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
