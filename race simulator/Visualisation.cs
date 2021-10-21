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

        private static string[] _cornerRightHorinzontalDown = {@"---\", @"   |", @"   |", @"   |"};
        private static string[] _cornerRightHorinzontalUp = {@"|  \", @"|   ", @"|   ", @"\---"};
        
        private static string[] _cornerRightVertical = {"/---", "|   ", "|   ", "|  /"};


        private static string[] _cornerLeftHorizontal = {"  1 ", "  / ", "  2 ", "  3 "};
        private static string[] _cornerLefVertical = {"/  |", @"   |", "   |", "--/ "};

        private static string[] _emptySquare = {"    ", "    ", "    ", "    "};

        #endregion

        public static Array[,] Track3dArray =

        {
            {_cornerRightVertical, _finishHorizontal, _startHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _straightHorizontal, _cornerRightHorinzontalDown},
            {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_straightVertical, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _emptySquare, _straightVertical},
            {_cornerRightHorinzontalUp, _straightHorizontal,_straightHorizontal,_straightHorizontal,_straightHorizontal,_straightHorizontal,_straightHorizontal, _cornerLefVertical},
            
        };


        public static void Initialize()
        {
        }

        public static void DrawTrack(Track t)
        {
            Draw3dTrackArray(Track3dArray);
        }


        public static void Draw3dTrackArray(Array[,] arr)
        {
            //     StringBuilder lijntje = new StringBuilder();
            //     foreach (Array square in arr)
            //     {
            //         lijntje.Append(square.GetValue(1));
            //         foreach (string line in square)
            //         {
            //             Console.WriteLine(line);
            //         }
            //
            //         lijntje.Append("\n");
            //         Console.WriteLine($"lijnte = {lijntje}");
            //     }
            // }
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

        public static void FillTrackArray(Track t)
        {
            int index = 0;
            foreach (Section section in t.Sections)
            {
                Directions facing = Directions.Right;

                switch (section.SectionType)
                {
                    case Section.SectionTypes.Straight:

                        break;
                    case Section.SectionTypes.RightCorner:

                        break;
                    case Section.SectionTypes.Finish:

                        break;
                    case Section.SectionTypes.StartGrid:

                        break;
                }

                index++;
            }
        }
    }
}