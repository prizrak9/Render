using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;
using System;
using System.Timers;

namespace render
{
    static class ServiceRender
    {
        public static double DeltaTime { get; private set; }

        public static Scene scene;
        public static Vector2 ViewSize
        {
            get => viewSize;
            set
            {
                viewSize = value;
                shouldResize = true;
            }
        }

        public delegate void UpdateDelegate();

        public static event UpdateDelegate OnUpdate;

        private static Render render = new Render();
        private static Image image;
        private static WriteableBitmap bitmap;
        private static Int32Rect rect;
        private static Vector2 viewSize;
        private static Stopwatch stopwatch = new Stopwatch();
        private static DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        private static bool shouldResize;



        public static void Start(Image image, Vector2 viewSize)
        {
            ServiceRender.image = image;
            ViewSize = viewSize;
            //image.Source = bitmap;
            stopwatch.Start();
            timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += (object sender, System.EventArgs e) => Process();

            timer.Start();
        }

        
        public static void Process()
        {
            timer.Stop();

            OnUpdate?.Invoke();
            RunRender();

            DeltaTime = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Restart();

            timer.Start();
        }



        private static void Resize()
        {
            scene.camera.Size = ViewSize;
            bitmap = new WriteableBitmap((int)ViewSize.X, (int)ViewSize.Y, 96, 96, PixelFormats.Bgra32, null);
            rect = new Int32Rect(0, 0, (int)ViewSize.X, (int)ViewSize.Y);
            image.Source = bitmap;
            shouldResize = false;
        }

        private static void RunRender()
        {
            if (shouldResize)
            {
                Resize();
            }

            bitmap.WritePixels(rect, render.GetImage(scene), 4 * (int)ViewSize.X, 0);
        }

    }
}
