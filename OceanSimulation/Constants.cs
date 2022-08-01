using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OceanSimulation
{
    public struct Constants
    {
        public const int maxIterations = 10000;

        
        public const int numOceanSizes = 5;
        public const int defaultPickedIndex = 0;

        public static readonly int[] defaultRowsArr = new int[numOceanSizes] { 10, 15, 20, 25, 30 };
        public static readonly int[] defaultColsArr = new int[numOceanSizes] { 15, 25, 35, 45, 50 };

        public static readonly int[] defaultPreysArr = new int[numOceanSizes] { 30, 50, 70, 100, 200 };
        public static readonly int[] defaultPredatorsArr = new int[numOceanSizes] { 10, 20, 30, 45, 90 };
        public static readonly int[] defaultObstaclesArr = new int[numOceanSizes] { 20, 30, 60, 90, 150 };

        public static readonly int[] maxPreysArr = new int[numOceanSizes] { 45, 85, 170, 270, 385 };
        public static readonly int[] maxPredatorsArr = new int[numOceanSizes] { 15, 30, 60, 90, 130 };
        public static readonly int[] maxObstaclesArr = new int[numOceanSizes] { 40, 85, 170, 265, 385 };

        //public static readonly int[] oceanFormWidth = new int[numOceanSizes] { 890, 1200, 1310, 1120, 1260 };
        //public static readonly int[] oceanFormHeight = new int[numOceanSizes] { 745, 875, 910, 780, 905 };

        public static readonly string[] cellBorderStylesText = new string[]{ "None", "Slim", "Wide"};
        public static readonly int[] cellBorderStylesIndex = new int[] { 0, 1, 3 };
        public const int defaultPickedCellBorderStyle = 0;
        //public const string[] cellBorderStyles


        public const int oceanFormWidthAddition = 90;
        public const int oceanFormHeightAddition = 200;

        public static readonly int[] cellSize = new int[numOceanSizes] { 50, 40, 30, 20, 18};

        //private const int numOfSpeeds = 5;
        //public static readonly string[] speedComboBoxName = new int[] {}


        public const int defaultSpeed = 200;
        public const int defaultPickedSpeedIndex = 2;
        public static readonly int[] speedComboBoxValue = new int[] { 800, 400, 200, 100, 50 };

        public static readonly int maxRows = defaultRowsArr.Last();
        public static readonly int maxCols = defaultColsArr.Last();

        public static readonly int defaultRows = defaultRowsArr[defaultPickedIndex];
        public static readonly int defaultCols = defaultColsArr[defaultPickedIndex];

        public static readonly int maxPreys = maxPreysArr[defaultPickedIndex];
        public static readonly int maxPredators = maxPredatorsArr[defaultPickedIndex];
        public static readonly int maxObstacles = maxObstaclesArr[defaultPickedIndex];

        public static readonly int defaultNumPreys = defaultPreysArr[defaultPickedIndex];
        public static readonly int defaultNumPredators = defaultPredatorsArr[defaultPickedIndex];
        public static readonly int defaultNumObstacles = defaultObstaclesArr[defaultPickedIndex];
        public static readonly int defaultNumIterations = 1000;

        public const char defaultImage = '-';
        public const char defaultPredatorImage = 'S';
        public const char defaultPreyImage = 'f';
        public const char defaultObstacleImage = '#';
        public const char defaultBonusImage = '@';

        public const int timeToFeed = 6;
        public const int timeToReproduce = 6;
        public static readonly int[] timeToSpawnBonus = new int[] { 10, 4, 3, 2, 1 };
        public const int BonusTimeToEatForPredator = 20;
        //public const int BonusImmunityForPrey = 20;

        public static readonly Coordinate settingsWindowOffset = new Coordinate(70, 35);
    }
}
