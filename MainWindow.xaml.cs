using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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


            render = new Render();

            scene = SceneBuilder.GenerateScene();
            scene.camera.position.Y = 0;
            scene.camera.position.Z = 100;

            image.Source = new WriteableBitmap(1, 1, 96, 96, PixelFormats.Bgra32, null);

        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
//            scene.camera = new Camera(new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height));
            image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine(e.NewValue);
            Console.WriteLine("CamposX " + scene.camera.position.X);
            Console.WriteLine("CamposY " + scene.camera.position.Y);
            Console.WriteLine("CamposZ " + scene.camera.position.Z);
            Console.WriteLine("CamupX " + scene.camera.up.X);
            Console.WriteLine("CamupY " + scene.camera.up.Y);
            Console.WriteLine("CamupZ " + scene.camera.up.Z);
            Console.WriteLine("CamfwX " + scene.camera.forward.X);
            Console.WriteLine("CamfwY " + scene.camera.forward.Y);
            Console.WriteLine("CamfwZ " + scene.camera.forward.Z);
            //scene.camera.position.Z = e.NewValue * 10 - 100;
            scene.camera.up.Z = Math.Cos(e.NewValue * Math.PI / 20);
            scene.camera.up.Y = -Math.Sin(e.NewValue * Math.PI / 20);
            //scene.camera.forward.X = Math.Cos(e.NewValue * Math.PI / 20);
            //scene.camera.forward.Z = -Math.Sin(e.NewValue * Math.PI / 20);

            image.Source = BitmapSource.Create((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)image.ActualWidth);
        }
    }
}
