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
        //Scene scene;
        //Render render;

        public MainWindow()
        {
            InitializeComponent();

            Prepare();



            Time.OnUpdate += Time_OnUpdate;
            //Time.OnUpdate += async (object sender, EventArgs e) => Console.WriteLine("here");
        }

        private void Prepare()
        {
            //render = new Render();

            ServiceRender.scene = SceneBuilder.GenerateScene();
            ServiceRender.scene.camera.position.Y = 0;
            ServiceRender.scene.camera.position.Z = 100;

            //ServiceRender.im
            //image.Source = new WriteableBitmap(1, 1, 96, 96, PixelFormats.Bgra32, null);
            //Task.

            ServiceRender.Start(image, new Vector2(10, 10));

            Time.Start();

            KeyDown += Input.Update.KeyDown;
            KeyUp += Input.Update.KeyUp;
        }


        private void Time_OnUpdate(object sender, EventArgs e)
        {
            //scene.camera.position.Y -= Input.GetAxe(Input.Axe.Horizontal);

            ServiceRender.scene.camera.position += new Vector3(Input.GetAxe(Input.Axe.Vertical), -Input.GetAxe(Input.Axe.Horizontal), 0);

            //Console.WriteLine(Input.GetAxe(Input.Axe.Vertical));

            Vector3 axe = new Vector3(1, 0, 0);
            //double alpha = Input.GetAxe(Input.Axe.Horizontal) / Math.PI / 2;
            //CameraRotateAround(axe, alpha);
            //CameraRotateAround(axe, 1 / Math.PI / 2);

        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ServiceRender.ViewSize = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
            //scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
            //image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }

        private void CameraRotateAround(Vector3 axe, double angle)
        {
            Quaternion q = new Quaternion(Math.Cos(angle / 2), Math.Sin(angle / 2) * axe);
            ServiceRender.scene.camera.up = Quaternion.Rotate(ServiceRender.scene.camera.up, q);
            ServiceRender.scene.camera.forward = Quaternion.Rotate(ServiceRender.scene.camera.forward, q);

            //Console.WriteLine($"\n\n\nAngle to rotate on {angle}");
            //Console.WriteLine($"Campos {ServiceRender.scene.camera.position}");
            //Console.WriteLine($"Camup {ServiceRender.scene.camera.up}");
            //Console.WriteLine($"Camfw {ServiceRender.scene.camera.forward}");

            //image.Source = BitmapSource.Create((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)image.ActualWidth);

        }



    }
}
