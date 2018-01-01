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
            ServiceRender.scene.camera.position.Y = 0;
            ServiceRender.scene.camera.position.Z = 100;


            ServiceRender.Start(image, new Vector2(10, 10));


            KeyDown += Input.Update.KeyDown;
            KeyUp += Input.Update.KeyUp;
        }


        private void ServiceRender_OnUpdate()
        {
            Vector3 axe;
            double alpha;
            double move;
            double dt = ServiceRender.DeltaTime;

            if (Input.GetAxe(Input.Axe.HorizontalRot) != 0)
            {
                axe = new Vector3(0, 0, 1);
                alpha = -Input.GetAxe(Input.Axe.HorizontalRot) / Math.PI * dt * 2;
                Console.WriteLine($"Angle to rotate vertical {alpha}");
                ServiceRender.scene.camera.Rotate(axe, alpha);
            }

            if (Input.GetAxe(Input.Axe.VerticalRot) != 0)
            {
                axe = new Vector3(0, 1, 0);
                alpha = -Input.GetAxe(Input.Axe.VerticalRot) / Math.PI * dt * 2;
                Console.WriteLine($"Angle to rotate vertical {alpha}");
                ServiceRender.scene.camera.Rotate(axe, alpha);
            }

            if (Input.GetAxe(Input.Axe.ForwardMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.ForwardMov) * dt * 40;
                Console.WriteLine($"Forward step {move}");
                ServiceRender.scene.camera.position.X += move;
            }

            if (Input.GetAxe(Input.Axe.SideMov) != 0)
            {
                move = Input.GetAxe(Input.Axe.SideMov) * dt * 40;
                Console.WriteLine($"Forward step {move}");
                ServiceRender.scene.camera.position.Y -= move;
            }
            Console.WriteLine($"fps {1 / ServiceRender.DeltaTime }");

            //ServiceRender.scene.camera.position += new Vector3(Input.GetAxe(Input.Axe.Vertical), -Input.GetAxe(Input.Axe.Horizontal), 0);
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ServiceRender.ViewSize = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

       



    }
}
