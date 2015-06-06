using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MPE_try1
{
    public class eInitializer
    {

        public eInitializer(int upperLimit, int BoardDimension, ResourceDictionary pathsDict, ResourceDictionary strategyDict, Random randGen)
        {
            for (int idx = 1; idx <= upperLimit; idx++)
            {
                PathFigure myPathFigure = new PathFigure();
                myPathFigure.StartPoint = new Point(randGen.Next(BoardDimension), randGen.Next(BoardDimension));
                PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();

                for (int position = 0; position < 7; position++)
                {
                    LineSegment myLineSegment = new LineSegment();
                    myLineSegment.Point = new Point(randGen.Next(BoardDimension), randGen.Next(BoardDimension));
                    myPathSegmentCollection.Add(myLineSegment);
                }
                myPathFigure.Segments = myPathSegmentCollection;

                PathFigureCollection myPathFigureCollection = new PathFigureCollection();
                myPathFigureCollection.Add(myPathFigure);

                PathGeometry myPathGeometry = new PathGeometry();
                myPathGeometry.Figures = myPathFigureCollection;

                pathsDict.Add("AnimationPath" + idx, myPathGeometry);
            }

            for (int strid = 0; strid < 16; strid++)
            {
                string binarized;
                binarized = Convert.ToString(strid, 2).PadLeft(4,'0');
                strategyDict.Add(strid.ToString(), binarized);
            }

        }
    }
}
