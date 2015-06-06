using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        GlobalVars globals;
        BackgroundWorker bw;

        public static MainWindow Current { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Current = this;
            globals = new GlobalVars();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            bw = new BackgroundWorker();

            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
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
            eInitializer pokazywacz = new eInitializer(int.Parse(AgentCount.Text), globals.BoardDimension, globals.PathsDict, globals.StrategyDict, globals.randGen);
            Application.Current.Resources.MergedDictionaries.Add(globals.PathsDict);
            Application.Current.Resources.MergedDictionaries.Add(globals.StrategyDict);

            for (int idx = 1; idx <= int.Parse(AgentCount.Text); idx++)
            {
                Ellipse eliTmp = new Ellipse();
                Path linTmp = new Path();
                DoubleAnimationUsingPath aniXTmp = new DoubleAnimationUsingPath();
                DoubleAnimationUsingPath aniYTmp = new DoubleAnimationUsingPath();

                eliTmp.Name = "Ellipse" + idx;
                linTmp.Name = "Line" + idx;

                eliTmp.Width = 10;
                eliTmp.Height = 10;
                eliTmp.Fill = PickRandomBrush(globals.randGen);
                eliTmp.RenderTransform = new TranslateTransform();
                globals.ellipseList.Add("Ellipse" + idx.ToString(), eliTmp);

                linTmp.Data = (Geometry)TryFindResource("AnimationPath" + idx);
                linTmp.Stroke = PickRandomBrush(globals.randGen);

                aniXTmp.Name = "MovementX" + idx;
                aniXTmp.Source = PathAnimationSource.X;
                aniXTmp.Duration = TimeSpan.FromSeconds(20);
                Storyboard.SetDesiredFrameRate(aniXTmp, 5);
                Storyboard.SetTarget(aniXTmp, eliTmp);
                Storyboard.SetTargetProperty(aniXTmp, new PropertyPath(Canvas.LeftProperty));

                aniYTmp.Name = "MovementY" + idx;
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
                globals.storTmp.Children.Add(aniXTmp);
                globals.storTmp.Children.Add(aniYTmp);

            }
            foreach (var entry in globals.StrategyDict.Keys)
            {
                LoggingField.Text = LoggingField.Text + entry.ToString() + " - " 
                    + globals.StrategyDict[entry].ToString() + "\n";
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
            //storTmp.Resume(this);
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;


            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                globals.storTmp.Pause(this);
                for (int index = 0; index < globals.ellipseList.Count; index++)
                {
                    var myEllipse = globals.ellipseList.ElementAt(index).Value;
                    var myEllipse2 = globals.ellipseList.ElementAt(index).Value;
                    MainWindow.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
        {
            globals.y1 = (int)Canvas.GetTop(myEllipse);
            globals.x1 = (int)Canvas.GetLeft(myEllipse);
        }
        ));
                    EllipseGeometry meg1 = new EllipseGeometry(new Point(globals.x1, globals.y1), 2, 2);

                    for (int index2 = index + 1; index2 < globals.ellipseList.Count; index2++)
                    {
                        myEllipse2 = globals.ellipseList.ElementAt(index2).Value;
                        MainWindow.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
        {
            globals.y2 = (int)Canvas.GetTop(myEllipse2);
            globals.x2 = (int)Canvas.GetLeft(myEllipse2);
        }
        ));
                        EllipseGeometry meg2 = new EllipseGeometry(new Point(globals.x2, globals.y2), 2, 2);
                        IntersectionDetail d1 = meg1.FillContainsWithDetail(meg2, 0.1, ToleranceType.Absolute);
                        if (d1 != IntersectionDetail.Empty)
                        {
                            globals.ileKolizji++;
                            MainWindow.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
        {
            LoggingField.Text = LoggingField.Text + "Kolizja: " + myEllipse.Name + " z " + myEllipse2.Name + "\n";
        }
        ));
                        }
                    }
                    //worker.ReportProgress(index);
                }
                MainWindow.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
        {
            CollisionsCount.Content = globals.ileKolizji;
        }));
                globals.storTmp.Resume(this);
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.LoggingField.Text = this.LoggingField.Text + (e.ProgressPercentage.ToString() + "\n");
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                this.LoggingField.Text = this.LoggingField.Text + "Canceled!" + "\n";
            }
            else if (!(e.Error == null))
            {
                this.LoggingField.Text = this.LoggingField.Text + ("Error: " + e.Error.Message + "\n");
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Populate_agents();
            globals.storTmp.Completed += new EventHandler(StopMyDispatcher);
            StartButton.IsEnabled = false;
            ShowPaths.IsEnabled = false;
            globals.storTmp.Begin(this, true);
            globals.dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            globals.dispatcherTimer.Start();
        }

        private void StopMyDispatcher(object sender, EventArgs e)
        {
            globals.dispatcherTimer.Stop();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (globals.storTmp.GetIsPaused(this) == false)
            {
                globals.storTmp.Pause(this);
                globals.dispatcherTimer.Stop();
            }
            else
            {
                globals.storTmp.Resume(this);
                globals.dispatcherTimer.Start();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            globals.storTmp.Stop(this);
            globals.ileKolizji = 0;
            CollisionsCount.Content = "None";
            globals.dispatcherTimer.Stop();
            StartButton.IsEnabled = true;
            ShowPaths.IsEnabled = true;
            globals.ellipseList = new Dictionary<string, Ellipse>();
            globals.storTmp = new Storyboard();
            globals.storTmp.AutoReverse = false;
            LoggingField.Text = "";
        }

    }

}
