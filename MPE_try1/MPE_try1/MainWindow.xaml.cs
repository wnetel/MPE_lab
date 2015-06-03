using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
        private static Dictionary<string, Ellipse> ellipseList;
        private static Storyboard storTmp;
        private static int ileAgentow;

        public MainWindow()
        {
            InitializeComponent();
            ileAgentow = 30;
            eVisualizer pokazywacz = new eVisualizer(ileAgentow);
            Application.Current.Resources.MergedDictionaries.Add(pokazywacz.pathsDict);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            ellipseList = new Dictionary<string, Ellipse>();
            storTmp = new Storyboard();
            
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


            for (int idx = 1; idx <= ileAgentow; idx++)
            {
                Ellipse eliTmp = new Ellipse();
                Path linTmp = new Path();
                DoubleAnimationUsingPath aniXTmp = new DoubleAnimationUsingPath();
                DoubleAnimationUsingPath aniYTmp = new DoubleAnimationUsingPath();

                storTmp.AutoReverse = true;
                storTmp.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
                eliTmp.Name = "ellipse" + idx;
                linTmp.Name = "linia" + idx;
                aniXTmp.Name = "animationX" + idx;
                aniYTmp.Name = "animationY" + idx;
                //storTmp.Name = "storyboard" + idx;

                eliTmp.Width = 10;
                eliTmp.Height = 10;
                eliTmp.Fill = new SolidColorBrush(Colors.Red);
                eliTmp.RenderTransform = new TranslateTransform();
                //EventTrigger evTr = new EventTrigger();
                //evTr.RoutedEvent = EventManager.RegisterRoutedEvent("Path.Loaded" + idx, RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Ellipse));
                //eliTmp.Triggers.Add(evTr);
                ellipseList.Add("Ellipse" + idx.ToString(), eliTmp);

                linTmp.Data = Geometry.Parse((string)TryFindResource("AnimationPath" + idx));
                linTmp.Stroke = new SolidColorBrush(Colors.Yellow);

                aniXTmp.Name = "RuchX" + idx;
                aniXTmp.Source = PathAnimationSource.X;
                aniXTmp.Duration = TimeSpan.FromSeconds(20);
                Storyboard.SetTarget(aniXTmp, eliTmp);
                Storyboard.SetTargetProperty(aniXTmp, new PropertyPath(Canvas.LeftProperty));
                aniYTmp.Name = "RuchY" + idx;
                aniYTmp.Source = PathAnimationSource.Y;
                aniYTmp.Duration = TimeSpan.FromSeconds(20);
                Storyboard.SetTarget(aniYTmp, eliTmp);
                Storyboard.SetTargetProperty(aniYTmp, new PropertyPath(Canvas.TopProperty));

                aniXTmp.PathGeometry = PathGeometry.Parse((string)TryFindResource("AnimationPath" + idx)).GetWidenedPathGeometry(new Pen());
                aniYTmp.PathGeometry = PathGeometry.Parse((string)TryFindResource("AnimationPath" + idx)).GetWidenedPathGeometry(new Pen());

                MainCanvas.Children.Add(eliTmp);
                MainCanvas.Children.Add(linTmp);
                storTmp.Children.Add(aniXTmp);
                storTmp.Children.Add(aniYTmp);

            }
            storTmp.Begin(this.MainCanvas, true);


        }

        private void PauseV(object sender, RoutedEventArgs e)
        {
            if (storTmp.GetCurrentState() == System.Windows.Media.Animation.ClockState.Active)
            { storTmp.Pause(this.MainCanvas); }
            else { storTmp.Resume(this.MainCanvas); }
        }

    }

}
