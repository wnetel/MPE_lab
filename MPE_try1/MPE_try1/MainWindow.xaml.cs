using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPE_try1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            eVisualizer pokazywacz = new eVisualizer();
            Application.Current.Resources.MergedDictionaries.Add(pokazywacz.pathsDict);
        }

        private void AppExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is MPE ecoAgent Simulator, version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void Visualize(object sender, RoutedEventArgs e)
        {
            //for (int idx = 1; idx <= 20; idx++)
            //{
            //    Ellipse eli+idx.ToString() = new Ellipse();
            //    MainCanvas.Children.Add(eli)
            //}
            Dictionary<string, Ellipse> ellipseList = new Dictionary<string, Ellipse>();
            for (int idx = 1; idx <= 20; idx++)
            {
                Ellipse eliTmp = new Ellipse();
                eliTmp.Width = 10;
                eliTmp.Height = 10;
                eliTmp.RenderTransform = new TranslateTransform();
                EventTrigger evTr = new EventTrigger();
                //evTr.RoutedEvent = new RoutedEvent("Path.Loaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler));
                eliTmp.Triggers.Add(evTr);
                ellipseList.Add("Ellipse" + idx.ToString(), eliTmp);
                linia.Data = Geometry.Parse((string)TryFindResource("AnimationPath" + idx));
                RuchX.PathGeometry = PathGeometry.Parse((string)TryFindResource("AnimationPath" + idx)).GetWidenedPathGeometry(new Pen());
                RuchY.PathGeometry = PathGeometry.Parse((string)TryFindResource("AnimationPath" + idx)).GetWidenedPathGeometry(new Pen());
                AgentMovement.Storyboard.Begin();
            }



        }

    }

}
