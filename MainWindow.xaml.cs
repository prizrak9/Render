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
            Vector3 axe;
            double alpha;
            double move;

            if (Input.GetAxe(Input.Axe.HorizontalRot) != 0)
            {
                axe = new Vector3(0, 0, 1);
                alpha = -Input.GetAxe(Input.Axe.HorizontalRot) / Math.PI / 2;
                Console.WriteLine($"Angle to rotate vertical {alpha}");
                scene.camera.Rotate(axe, alpha);
            }

            if (Input.GetAxe(Input.Axe.VerticalRot) != 0)
            {
                axe = new Vector3(0, 1, 0);
                alpha = -Input.GetAxe(Input.Axe.VerticalRot) / Math.PI / 2;
                Console.WriteLine($"Angle to rotate vertical {alpha}");
                scene.camera.Rotate(axe, alpha);
            }

            if (Input.GetAxe(Input.Axe.ForwardMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.ForwardMov) * 10;
                Console.WriteLine($"Forward step {move}");
                scene.camera.position.X += move;
            }

            if (Input.GetAxe(Input.Axe.SideMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.SideMov) * 10;
                Console.WriteLine($"Forward step {move}");
                scene.camera.position.Y -= move;
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
