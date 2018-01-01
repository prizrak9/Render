using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace render
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Scene scene;
        Render render;

        public MainWindow()
        {
            InitializeComponent();

            Prepare();



            Time.OnUpdate += Time_OnUpdate;

        }

        private void Prepare()
        {
            render = new Render();

            scene = SceneBuilder.GenerateScene();
            scene.camera.position.Y = 0;
            scene.camera.position.Z = 100;

            image.Source = new WriteableBitmap(1, 1, 96, 96, PixelFormats.Bgra32, null);

            Time.Start();

            KeyDown += Input.Update.KeyDown;
            KeyUp += Input.Update.KeyUp;
        }


        private void Time_OnUpdate(object sender, EventArgs e)
        {
            Vector3 axe = new Vector3(1, 0, 0);
            double alpha = Input.GetAxe(Input.Axe.Horizontal) / Math.PI / 2;
            CameraRotateAround(axe, alpha);
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
            image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }

        private void CameraRotateAround(Vector3 axe, double angle)
        {
            Quaternion q = new Quaternion(Math.Cos(angle / 2), Math.Sin(angle / 2) * axe);
            scene.camera.up = Quaternion.Rotate(scene.camera.up, q);
            scene.camera.forward = Quaternion.Rotate(scene.camera.forward, q);

            Console.WriteLine($"\n\n\nAngle to rotate on {angle}");
            Console.WriteLine($"Campos {scene.camera.position}");
            Console.WriteLine($"Camup {scene.camera.up}");
            Console.WriteLine($"Camfw {scene.camera.forward}");

            image.Source = BitmapSource.Create((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)image.ActualWidth);

        }



    }
}
