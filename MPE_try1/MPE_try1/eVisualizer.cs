using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MPE_try1
{
    public class eVisualizer
    {
        public ResourceDictionary pathsDict;
        public eVisualizer()
        {
            pathsDict = new ResourceDictionary();
            Random randGen = new Random();
            int[] tabXY = new int[14];

            for (int idx = 1; idx <= 20; idx++)
            {
                for (int position = 0; position < 14; position++)
                {
                    tabXY[position] = randGen.Next(500);
                }
                pathsDict.Add("AnimationPath" + idx.ToString(), "M " + tabXY[0].ToString() + "," + tabXY[1].ToString() + " C " + tabXY[2].ToString() + "," + tabXY[3].ToString() + " "
                    + tabXY[4].ToString() + "," + tabXY[5].ToString() + " " + tabXY[6].ToString() + "," + tabXY[7].ToString() + " " + tabXY[8].ToString() + "," + tabXY[9].ToString()
                    + " " + tabXY[10].ToString() + "," + tabXY[11].ToString() + " " + tabXY[12].ToString() + "," + tabXY[13].ToString());
            }

        }
    }
}
