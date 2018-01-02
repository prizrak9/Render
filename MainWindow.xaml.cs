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
            Vector3 aa = new Vector3(0.70710678, 0.70710678, 0);
            Vector3 bb = new Vector3(0, 1, 0);
            Quaternion rr = Quaternion.BetweenVectors(new Vector3(1, 0, 0), aa);

            double angleBetween = Vector3.AngleBetween(new Vector3(1, 0, 0), aa);
            double s = Math.Sin(angleBetween / 2);
            Vector3 third = new Vector3(1, 0, 0) * aa;

            Console.WriteLine($"Q {Math.Cos(angleBetween / 2)} {third} {s}");

            Console.WriteLine($"Quat {rr.s} {rr.v}");
            Console.WriteLine($"Quat {rr.s} {rr.v}");
            Console.WriteLine($"After rot {Quaternion.Rotate(aa, rr)}");
            Console.WriteLine($"Axe after rot {Quaternion.Rotate(bb, rr)}");


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
            Vector3 axe;
            double alpha;
            double move;

            if (Input.GetAxe(Input.Axe.HorizontalRot) != 0)
            {
                alpha = -Input.GetAxe(Input.Axe.HorizontalRot) / Math.PI / 2;
                scene.camera.RotateH(alpha);
            }

            if (Input.GetAxe(Input.Axe.VerticalRot) != 0)
            {
                alpha = Input.GetAxe(Input.Axe.VerticalRot) / Math.PI / 2;
                scene.camera.RotateV(alpha);
            }

            if (Input.GetAxe(Input.Axe.ForwardMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.ForwardMov) * 10;
                scene.camera.position += scene.camera.forward * move;
            }

            if (Input.GetAxe(Input.Axe.SideMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.SideMov) * 10;
                scene.camera.position += scene.camera.forward * scene.camera.up * move;
            }

            image.Source = BitmapSource.Create((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)image.ActualWidth);
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
            image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }
        
    }
}
