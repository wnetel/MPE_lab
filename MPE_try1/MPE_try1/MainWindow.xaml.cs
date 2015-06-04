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
using System.Windows.Threading;

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
        private DispatcherTimer dispatcherTimer;
        private int ileKolizji;


        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            ellipseList = new Dictionary<string, Ellipse>();
            storTmp = new Storyboard();
            storTmp.AutoReverse = false;
            ileKolizji = 0;
        }

        private void AppExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is MPE ecoAgent Simulator, version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private Brush PickRandomBrush(Random rnd)
        {
            Brush result = Brushes.Transparent;
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);
            return result;
        }

        private void Populate_agents()
        {
            Random rnd = new Random();
            eVisualizer pokazywacz = new eVisualizer(int.Parse(AgentCount.Text));
            Application.Current.Resources.MergedDictionaries.Add(pokazywacz.pathsDict);

            for (int idx = 1; idx <= int.Parse(AgentCount.Text); idx++)
            {
                Ellipse eliTmp = new Ellipse();
                Path linTmp = new Path();
                DoubleAnimationUsingPath aniXTmp = new DoubleAnimationUsingPath();
                DoubleAnimationUsingPath aniYTmp = new DoubleAnimationUsingPath();

                eliTmp.Name = "ellipse" + idx;
                linTmp.Name = "linia" + idx;

                eliTmp.Width = 10;
                eliTmp.Height = 10;
                eliTmp.Fill = PickRandomBrush(rnd);
                //new SolidColorBrush(Colors.Red);
                eliTmp.RenderTransform = new TranslateTransform();
                //EventTrigger evTr = new EventTrigger();
                //evTr.RoutedEvent = EventManager.RegisterRoutedEvent("Path.Loaded" + idx, RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Ellipse));
                //eliTmp.Triggers.Add(evTr);
                ellipseList.Add("Ellipse" + idx.ToString(), eliTmp);

                linTmp.Data = (Geometry)TryFindResource("AnimationPath" + idx);
                linTmp.Stroke = PickRandomBrush(rnd);

                aniXTmp.Name = "RuchX" + idx;
                aniXTmp.Source = PathAnimationSource.X;
                aniXTmp.Duration = TimeSpan.FromSeconds(20);
                Storyboard.SetDesiredFrameRate(aniXTmp, 5);
                Storyboard.SetTarget(aniXTmp, eliTmp);
                Storyboard.SetTargetProperty(aniXTmp, new PropertyPath(Canvas.LeftProperty));

                aniYTmp.Name = "RuchY" + idx;
                aniYTmp.Source = PathAnimationSource.Y;
                aniYTmp.Duration = TimeSpan.FromSeconds(20);
                Storyboard.SetDesiredFrameRate(aniYTmp, 5);
                Storyboard.SetTarget(aniYTmp, eliTmp);
                Storyboard.SetTargetProperty(aniYTmp, new PropertyPath(Canvas.TopProperty));

                aniXTmp.PathGeometry = (PathGeometry)TryFindResource("AnimationPath" + idx);
                aniYTmp.PathGeometry = (PathGeometry)TryFindResource("AnimationPath" + idx);

                MainCanvas.Children.Add(eliTmp);
                if (ShowPaths.IsChecked == true)
                {
                    MainCanvas.Children.Add(linTmp);
                }
                storTmp.Children.Add(aniXTmp);
                storTmp.Children.Add(aniYTmp);

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int x1,x2,y1,y2;
            //storTmp.Pause(this);

            //for (int index = 0; index < ellipseList.Count; index++)
            //{
            //    var myEllipse = ellipseList.ElementAt(index).Value;
            //    var myEllipse2 = ellipseList.ElementAt(index).Value;
                
            //    //if (index < ellipseList.Count - 1)
            //    //{
            //    //    myEllipse2 = ellipseList.ElementAt(index + 1).Value;
            //    //}
            //    //else { break; }

            //    y1 = (int)Canvas.GetTop(myEllipse);
            //    x1 = (int)Canvas.GetLeft(myEllipse);
            //    EllipseGeometry meg1 = new EllipseGeometry(new Point(x1,y1),2,2);

            //    for (int index2 = index + 1; index2 < ellipseList.Count; index2++)
            //    {
            //        myEllipse2 = ellipseList.ElementAt(index2).Value;
            //        y2 = (int)Canvas.GetTop(myEllipse2);
            //        x2 = (int)Canvas.GetLeft(myEllipse2);
            //        EllipseGeometry meg2 = new EllipseGeometry(new Point(x2, y2), 2, 2);
            //        IntersectionDetail d1 = meg1.FillContainsWithDetail(meg2, 0.1,ToleranceType.Absolute);
            //        if (d1 != IntersectionDetail.Empty)
            //        {
            //            ileKolizji++;
            //            LoggingField.Text = LoggingField.Text + "\n"+ myEllipse.Name + " z " + myEllipse2.Name;
            //        }

            //        //if (Math.Abs(y1 - y2) <= 2 && Math.Abs(x1 - x2) <= 2)
            //        //{
            //        //    ileKolizji++;
            //        //}
            //    }
            //}

            //CollisionsCount.Content = ileKolizji;
            //storTmp.Resume(this);
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Populate_agents();
            storTmp.Completed += new EventHandler(StopMyDispatcher);
            StartButton.IsEnabled = false;
            ShowPaths.IsEnabled = false;
            storTmp.Begin(this, true);
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void StopMyDispatcher(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Pause_Button_Click(object sender, RoutedEventArgs e)
        {
            if (storTmp.GetIsPaused(this) == false)
            {
                storTmp.Pause(this);
                dispatcherTimer.Stop();
            }
            else
            {
                storTmp.Resume(this);
                dispatcherTimer.Start();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            storTmp.Stop(this);
            ileKolizji = 0;
            CollisionsCount.Content = "None";
            dispatcherTimer.Stop();
            StartButton.IsEnabled = true;
            ShowPaths.IsEnabled = true;
            ellipseList = new Dictionary<string, Ellipse>();
            storTmp = new Storyboard();
            storTmp.AutoReverse = false;
        }

    }

}
