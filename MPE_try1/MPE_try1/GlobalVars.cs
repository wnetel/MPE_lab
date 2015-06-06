using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MPE_try1
{
    class GlobalVars
    {
        public int BoardDimension;
        public Dictionary<string, Ellipse> ellipseList;
        public Storyboard storTmp;
        public DispatcherTimer dispatcherTimer;
        public int ileKolizji;
        public ResourceDictionary PathsDict;
        public ResourceDictionary StrategyDict;
        public Random randGen;
        public int x1, x2, y1, y2;

        public GlobalVars()
        {
            BoardDimension = 950;
            ellipseList = new Dictionary<string, Ellipse>();
            storTmp = new Storyboard();
            storTmp.AutoReverse = false;
            ileKolizji = 0;
            PathsDict = new ResourceDictionary();
            StrategyDict = new ResourceDictionary();
            randGen = new Random();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }
    }
}
