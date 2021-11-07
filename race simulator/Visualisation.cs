using System;
using System.Diagnostics;
using System.Text;
using Model;

namespace race_simulator
{
    public static class Visualisation
    {
        #region graphics

        private static string[] _startHorizontal = {"----", " >  ", "  > ", "----"};
        private static string[] _startVertical = {"|  ", " ^  ", " ^  ", "   |"};

        private static string[] _finishHorizontal = {"----", " XX ", " XX ", "----"};
        private static string[] _finishVertical = {"|  ", " XX  ", " XX ", "   |"};

        private static string[] _straightHorizontal = {"----", "    ", "    ", "----"};
        private static string[] _straightVertical = {"|  |", "|  |", "|  |", "|  |"};

        private static string[] _cornerRightHorinzontalDown = {@"---\", @"   |", @"   |", @"\  |"};
        private static string[] _cornerRightHorinzontalUp = {@"|  \", @"|   ", @"|   ", @"\---"};
        private static string[] _cornerRightVerticalDown = {@"|  |", @"\   ", @"\    ", @"   \"};
        private static string[] _cornerRightVertical = {"/---", "|   ", "|   ", "|  /"};


        private static string[] _cornerLeftHorizontalUp = {"  1 ", "  / ", "  2 ", "  3 "};
        private static string[] _cornerLeftHorizontalDown = {"  4 ", "  / ", "  5 ", "  6 "};
        private static string[] _cornerLeftVerticalDown = {"  7 ", "  / ", "  8 ", "  9 "};
        private static string[] _cornerLefVertical = {"/  |", @"   |", "   |", "--/ "};

        private static string[] _emptySquare = {"    ", "    ", "    ", "    "};

        #endregion

        public static Array[,] Track3dArray =

        {
            {_cornerRightVertical, _finishHorizontal, _startHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _cornerRightHorinzontalDown}, {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_cornerRightHorinzontalUp, _straightHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _cornerLefVertical},
        };

        public static Array[,] empetyTrackArray =
        {
            {_emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare},
            {_emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare},
            {_emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare},
            {_emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare}
        };


        public static void Initialize()
        {
        }

        public static void DrawTrack(Track t)
        {
            FillTrackArray(t, empetyTrackArray);
            Draw3dTrackArray(empetyTrackArray);
            // Draw3dTrackArray(Track3dArray);
        }


        public static void Draw3dTrackArray(Array[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j].GetValue(0));
                }

                Console.WriteLine();

                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j].GetValue(1));
                }

                Console.WriteLine();

                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j].GetValue(2));
                }

                Console.WriteLine();

                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j].GetValue(3));
                }

                Console.WriteLine();
            }
        }

        public static void FillTrackArray(Track t, Array[,] arr)
            //start point is [2, 0], so a little left of top right
            // so the finish can get before it
        {
            Directions facing = Directions.Right;
            int x = 2;
            int y = 0;
            foreach (Section section in t.Sections)
            {
                switch (section.SectionType)
                {
                    case Section.SectionTypes.Straight:
                        switch (facing)
                        {
                            case Directions.Right:
                                arr[y, x] = _straightHorizontal;
                                x++;
                                break;
                            case Directions.Left:
                                arr[y, x] = _straightHorizontal;
                                x--;
                                break;
                            case Directions.Down:
                                arr[y, x] = _straightVertical;
                                y++;
                                break;
                            case Directions.Up:
                                arr[y, x] = _startVertical;
                                y--;
                                break;
                        }

                        break;
                    case Section.SectionTypes.RightCorner:
                        switch (facing)
                        {
                            case Directions.Right:
                                arr[y, x] = _cornerRightHorinzontalDown;
                                facing = Directions.Down;
                                y++;
                                break;
                            case Directions.Left:
                                arr[y, x] = _cornerRightHorinzontalUp;
                                facing = Directions.Up;
                                y--;
                                break;
                            case Directions.Up:
                                arr[y, x] = _cornerRightVertical;
                                facing = Directions.Right;
                                x++;
                                break;
                            case Directions.Down:
                                arr[y, x] = _cornerRightVerticalDown;
                                facing = Directions.Left;
                                x++;
                                break;
                        }
                        break;
                    case Section.SectionTypes.LeftCorner:
                        switch (facing)
                        {
                            case Directions.Right:
                                arr[y, x] = _cornerLeftHorizontalUp;
                                facing = Directions.Up;
                                y--;
                                break;
                            case Directions.Left:
                                arr[y, x] = _cornerLeftHorizontalDown;
                                facing = Directions.Down;
                                y++;
                                break;
                            case Directions.Up:
                                arr[y, x] = _cornerLefVertical;
                                facing = Directions.Left;
                                x--;
                                break;
                            case Directions.Down:
                                arr[y, x] = _cornerLeftVerticalDown;
                                facing = Directions.Left;
                                x--;
                                break;
                        }
                        break;
                    case Section.SectionTypes.Finish:
                        switch (facing)
                        {
                            case Directions.Right:
                                arr[y, x] = _finishHorizontal;
                                x++;
                                break;
                            case Directions.Left:
                                arr[y, x] = _finishHorizontal;
                                x--;
                                break;
                            case Directions.Down:
                                arr[y, x] = _finishVertical;
                                y++;
                                break;
                            case Directions.Up:
                                arr[y, x] = _finishVertical;
                                y--;
                                break;
                        }

                        break;
                    case Section.SectionTypes.StartGrid:
                        arr[y, x] = _startHorizontal;
                        x++;
                        break;
                }
            }
        }
    }
}