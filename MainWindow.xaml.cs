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
            double rotK = dt * 2 / Math.PI;
            double movK = dt * 40;
            Camera camera = ServiceRender.scene.camera;
            

            alpha = Input.GetAxe(Input.Axe.HorizontalRot);
            if (alpha != 0)
                camera.RotateH(-alpha * rotK);

            alpha = Input.GetAxe(Input.Axe.VerticalRot);
            if (alpha != 0)
                camera.RotateV(alpha * rotK);

            move = Input.GetAxe(Input.Axe.ForwardMov);
            if (move != 0)
                camera.position += camera.Forward * move * movK;

            move = Input.GetAxe(Input.Axe.SideMov);
            if (move != 0)
                camera.position += camera.Left * move * movK;

            Console.WriteLine($"fps { 1 / dt }");
        }

        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ServiceRender.ViewSize = new Vector2((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}
