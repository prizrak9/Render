using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace render
{
    class SceneBuilder
    {
        public static Scene GenerateScene()
        {
            return new Scene
            {
                camera = new Camera(new Vector2(640, 480)),
                polyLines = new Polyline[1]
                {
                    new Polyline
                    {
                        points = GetFunction(Func, 1000*3, 60, 60)
                    }
                }
            };
        }

        static double[] GetFunction(Func<double, double> func, int count, double kx, double ky)
        {
            double[] arr = new double[count];

            double x = 0, step = 0.1;

            for(int i = 0; i < count; i+=3, x+=step)
            {
                arr[i] = x * kx;
                arr[i + 1] = func(x * kx) * ky;
                arr[i + 2] = 70 + i * 3.5;
            }

            return arr;
        }

        static double Func(double x) =>
            Math.Sin(x);
    }

}
