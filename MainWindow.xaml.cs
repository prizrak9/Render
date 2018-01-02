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

        public MainWindow()
        {
            InitializeComponent();

            Prepare();

            ServiceRender.OnUpdate += ServiceRender_OnUpdate;
        }

        private void Prepare()
        {
            ServiceRender.scene = SceneBuilder.GenerateScene();

            ServiceRender.Start(image, new Vector2(10, 10));

            KeyDown += Input.Update.KeyDown;
            KeyUp += Input.Update.KeyUp;
        }


        private void ServiceRender_OnUpdate()
        {
            double alpha;
            double move;
            double dt = ServiceRender.DeltaTime;
            
            if (Input.GetAxe(Input.Axe.HorizontalRot) != 0)
            {
                alpha = -Input.GetAxe(Input.Axe.HorizontalRot) / Math.PI * dt * 2;
                ServiceRender.scene.camera.RotateH(alpha);
            }

            if (Input.GetAxe(Input.Axe.VerticalRot) != 0)
            {
                alpha = Input.GetAxe(Input.Axe.VerticalRot) / Math.PI * dt * 2;
                ServiceRender.scene.camera.RotateV(alpha);
            }

            if (Input.GetAxe(Input.Axe.ForwardMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.ForwardMov) * dt * 40;
                ServiceRender.scene.camera.position += ServiceRender.scene.camera.forward * move;
            }

            if (Input.GetAxe(Input.Axe.SideMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.SideMov) * dt * 40;
                ServiceRender.scene.camera.position += ServiceRender.scene.camera.forward * ServiceRender.scene.camera.up * move;
            }

            Console.WriteLine($"fps {1 / ServiceRender.DeltaTime }");
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ServiceRender.ViewSize = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}
