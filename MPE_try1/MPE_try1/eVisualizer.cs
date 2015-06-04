using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MPE_try1
{
    public class eVisualizer
    {
        public ResourceDictionary pathsDict;
        private static int dimension;
        public eVisualizer(int upperLimit)
        {
            pathsDict = new ResourceDictionary();
            Random randGen = new Random();
            dimension = 950;
            //int[] tabXY = new int[14];

            for (int idx = 1; idx <= upperLimit; idx++)
            {
                PathFigure myPathFigure = new PathFigure();
                myPathFigure.StartPoint = new Point(randGen.Next(dimension), randGen.Next(dimension));
                PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();

                for (int position = 0; position < 7; position++)
                {
                    LineSegment myLineSegment = new LineSegment();
                    myLineSegment.Point = new Point(randGen.Next(dimension), randGen.Next(dimension));
                    myPathSegmentCollection.Add(myLineSegment);
                    //tabXY[position] = randGen.Next(690);
                    //tabXY[position+1] = randGen.Next(690);
                }
                myPathFigure.Segments = myPathSegmentCollection;

                PathFigureCollection myPathFigureCollection = new PathFigureCollection();
                myPathFigureCollection.Add(myPathFigure);

                PathGeometry myPathGeometry = new PathGeometry();
                myPathGeometry.Figures = myPathFigureCollection;

                pathsDict.Add("AnimationPath" + idx, myPathGeometry);
                //pathsDict.Add("AnimationPath" + idx.ToString(), "M " + tabXY[0].ToString() + "," + tabXY[1].ToString() + " C " + tabXY[2].ToString() + "," + tabXY[3].ToString() + " "
                //    + tabXY[4].ToString() + "," + tabXY[5].ToString() + " " + tabXY[6].ToString() + "," + tabXY[7].ToString() + " " + tabXY[8].ToString() + "," + tabXY[9].ToString()
                //    + " " + tabXY[10].ToString() + "," + tabXY[11].ToString() + " " + tabXY[12].ToString() + "," + tabXY[13].ToString());
            }

        }
    }
}
