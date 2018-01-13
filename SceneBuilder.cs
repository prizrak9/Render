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
                    /*new SceneObject
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
                    },*/
                    new SceneObject
                    {//Z
                        mesh = GetPolyline(new [] {new Vector3(-400, 100, 750), new Vector3(-400, -400, 750), new Vector3(400, 100, 750), new Vector3(400, -400, 750), }, Color._Red)
                    },
                    new SceneObject
                    {//-Z
                        mesh = GetPolyline(new [] {new Vector3(400, 100, -750), new Vector3(400, -400, -750), new Vector3(-400, 100, -750), new Vector3(-400, -400, -750), }, Color._Red)
                    },
                    new SceneObject
                    {//-Z -
                        mesh = GetPolyline(new [] {new Vector3(0, 400, -750), new Vector3(0, 250, -750),}, Color._Red)
                    },
                    new SceneObject
                    {//Y1
                        mesh = GetPolyline(new [] {new Vector3(-100, 750, 400), new Vector3(150, 750, 0), new Vector3(150, 750, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//Y2
                        mesh = GetPolyline(new [] {new Vector3(400, 750, 400), new Vector3(150, 750, 0),}, Color._Red)
                    },
                    new SceneObject
                    {//-Y1
                        mesh = GetPolyline(new [] {new Vector3(100, -750, 400), new Vector3(-150, -750, 0), new Vector3(-150, -750, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//-Y2
                        mesh = GetPolyline(new [] {new Vector3(-400, -750, 400), new Vector3(-150, -750, 0),}, Color._Red)
                    },
                    new SceneObject
                    {//-Y -
                        mesh = GetPolyline(new [] {new Vector3(400, -750, 0), new Vector3(250, -750, 0),}, Color._Red)
                    },
                    new SceneObject
                    {//X1
                        mesh = GetPolyline(new [] {new Vector3(750, 100, 400), new Vector3(750, -400, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//X2
                        mesh = GetPolyline(new [] {new Vector3(750, -400, 400), new Vector3(750, 100, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//-X1
                        mesh = GetPolyline(new [] {new Vector3(-750, -100, 400), new Vector3(-750, 400, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//-X2
                        mesh = GetPolyline(new [] {new Vector3(-750, 400, 400), new Vector3(-750, -100, -400),}, Color._Red)
                    },
                    new SceneObject
                    {//-X -
                        mesh = GetPolyline(new [] {new Vector3(-750, -400, 0), new Vector3(-750, -250, 0),}, Color._Red)
                    },
                    new SceneObject
                    {//UP
                        mesh = GetPolygon(new [] {new Vector3(-500, 500, 750), new Vector3(500, 500, 750), new Vector3(500, -500, 750), new Vector3(-500, -500, 750),}, Color._Blue)
                    },
                    new SceneObject
                    {//DOWN
                        mesh = GetPolygon(new [] {new Vector3(-500, 500, -750), new Vector3(500, 500, -750), new Vector3(500, -500, -750), new Vector3(-500, -500, -750),}, Color._Blue)
                    },
                    new SceneObject
                    {//N
                        mesh = GetPolygon(new [] {new Vector3(750, 500, 500), new Vector3(750, 500, -500), new Vector3(750, -500, -500), new Vector3(750, -500, 500),}, Color._Blue)
                    },
                    new SceneObject
                    {//S
                        mesh = GetPolygon(new [] {new Vector3(-750, 500, 500), new Vector3(-750, 500, -500), new Vector3(-750, -500, -500), new Vector3(-750, -500, 500),}, Color._Blue)
                    },
                    new SceneObject
                    {//W
                        mesh = GetPolygon(new [] {new Vector3(500, 750, 500), new Vector3(500, 750, -500), new Vector3(-500, 750, -500), new Vector3(-500, 750, 500),}, Color._Blue)
                    },
                    new SceneObject
                    {//E
                        mesh = GetPolygon(new [] {new Vector3(500, -750, 500), new Vector3(500, -750, -500), new Vector3(-500, -750, -500), new Vector3(-500, -750, 500),}, Color._Blue)
                    },
                }
            };
        }


        static MeshWire GetPolyline(Vector3[] points, Color color)
        {
            MeshWire mesh = new MeshWire()
            {
                points = new Point[points.Length],
                links = new int[points.Length - 1][]
            };

            for (int i = 0; i < points.Length; i++)
            {
                mesh.points[i] = new Point(color, points[i]);
            }

            for (int i = 0; i < mesh.links.Length; i++)
            {
                mesh.links[i] = new int[2] { i, i + 1 };
            }

            return mesh;
        }

        static MeshWire GetPolygon(Vector3[] points, Color color)
        {
            MeshWire mesh = new MeshWire()
            {
                points = new Point[points.Length],
                links = new int[points.Length][]
            };

            for (int i = 0; i < points.Length; i++)
            {
                mesh.points[i] = new Point(color, points[i]);
            }

            for (int i = 0; i < mesh.links.Length - 1; i++)
            {
                mesh.links[i] = new int[2] { i, i + 1 };
            }
            mesh.links[mesh.links.Length - 1] = new int[2] { mesh.links.Length - 1, 0 };

            return mesh;
        }

        /*static MeshWire GetFunction2(Func<Func<double, double>, int, double[]> func, Func<double, double>func1, int count)
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
                arr[i + 2] = -100;
            }

            return arr;
        }*/

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
