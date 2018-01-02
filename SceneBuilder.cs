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
                objects = new SceneObject[]
                {
                    new SceneObject
                    {
                        mesh = GetFunction2(GetFunction1, Func, 1000*3)
                    },
                    new SceneObject
                    {
                        mesh = GetFunction2(GetFunction, Func, 1000*3)
                    },
                    new SceneObject
                    {
                        mesh = GetFunction2(GetFunction, Func1, 1000*3)
                    },
                    new SceneObject
                    {
                        mesh = GetFunction2(GetFunction, Func2, 1000*3)
                    },
                    new SceneObject
                    {
                        mesh = GetFunction2(GetFunction1,Func3, 1000*3)
                    },
                }
                //polyLines = new Polyline[]
                //{
                //    new Polyline
                //    {
                //        points = GetFunction1(Func, 1000*3) //горизонтальная синусоида на х=0
                //    },
                //    new Polyline
                //    {
                //        points = GetFunction(Func, 1000*3, 60, 60)
                //    },
                //    new Polyline
                //    {
                //        points = GetFunction(Func1, 1000*3, 60, 60)
                //    },
                //    new Polyline
                //    {
                //        points = GetFunction(Func2, 1000*3, 60, 60)
                //    },
                //    new Polyline
                //    {
                //        points = GetFunction1(Func3, 1000*3)
                //    }
                //}
            };
        }


        

        static MeshWire GetFunction2(Func<Func<double, double>, int, double[]> func, Func<double, double>func1, int count)
        {
            MeshWire mesh = new MeshWire();

            mesh.points = func(func1, count);
            mesh.links = new int[(mesh.points.Length - 1) / 3][];

            for (int i = 0; i < mesh.links.Length; i++)
            {
                mesh.links[i] = new int[2] { i, i + 1 };
            }

            return mesh;
        }


        static double[] GetFunction1(Func<double, double> func, int count)
        {
            double[] arr = new double[count];

            double x = 0, step = 0.1;

            for (int i = 0; i < count; i += 3, x += step)
            {
                arr[i] = 0;
                arr[i + 1] = x * 5;
                arr[i + 2] = 100 + func(x) * 10;
            }

            return arr;
        }
        static double[] GetFunction(Func<double, double> func, int count)
        {
            double[] arr = new double[count];

            double x = 0, step = 0.1;

            for(int i = 0; i < count; i+=3, x+=step)
            {
                arr[i] = x * 60;
                arr[i + 1] = func(x * 60) * 60;
                arr[i + 2] = 0;
            }

            return arr;
        }

        static double Func(double x) =>
            Math.Sin(x);

        static double Func1(double x) =>
            1;

        static double Func2(double x) =>
            -1;

        static double Func3(double x) =>
            x;

    }

}
