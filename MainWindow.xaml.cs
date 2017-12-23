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

            image.Source = new WriteableBitmap(1, 1, 96, 96, PixelFormats.Bgra32, null);

        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scene.camera.Size = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
//            scene.camera = new Camera(new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height));
            image.Source = BitmapSource.Create((int)e.NewSize.Width, (int)e.NewSize.Height, 96, 96, PixelFormats.Bgra32, null, render.GetImage(scene), 4 * (int)e.NewSize.Width);
        }

    }
}
