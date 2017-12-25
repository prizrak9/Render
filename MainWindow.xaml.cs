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

            // Set maximum value.
            // No need for convertions to radian.
            slider.Maximum = Math.PI;
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
            image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // $ for including value in brackets.
            // @ for ignoring special symbols
            // also used to set output format 
            // in exact way as it is written.
            Console.WriteLine($@"
{e.NewValue}
CamposX {scene.camera.position.X}
CamposY {scene.camera.position.Y}
CamposZ {scene.camera.position.Z}
CamupX {scene.camera.up.X}
CamupY {scene.camera.up.Y}
CamupZ {scene.camera.up.Z}
CamfwX {scene.camera.forward.X}
CamfwY {scene.camera.forward.Y}
CamfwZ {scene.camera.forward.Z}
");
            
            scene.camera.up.Z = Math.Cos(e.NewValue);
            scene.camera.up.Y = -Math.Sin(e.NewValue);
            //scene.camera.up.Y = -Math.Sin(e.NewValue * Math.PI / 20);
            //scene.camera.forward.X = Math.Cos(e.NewValue * Math.PI / 20);
            //scene.camera.forward.Z = -Math.Sin(e.NewValue * Math.PI / 20);

            image.Source = BitmapSource.Create((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)image.ActualWidth);
        }
    }
}
