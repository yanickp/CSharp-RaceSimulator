using System;
using System.Diagnostics;
using System.Text;
using Model;

namespace race_simulator
{
    public static class Visualisation
    {
        #region graphics

        private static string[] _startHorizontal = {" > ", "---", " > "};
        private static string[] _startVertical = {" ^ ", " | ", " ^ "};

        private static string[] _finishHorizontal = {" X ", "---", " X "};
        private static string[] _finishVertical = {" X ", " | ", " X "};

        private static string[] _straightHorizontal = {" 1 ", "---", " 2 "};
        private static string[] _straightVertical = {" 1 ", " | ", " 2 "};

        private static string[] _cornerRightHorinzontal = {" 2 ", @" \ ", " 1 "};
        private static string[] _cornerRightVertical = {" 1 ", " / ", " 2 "};


        private static string[] _cornerLeftHorizontal = {" 1 ", " / ", " 2 "};
        private static string[] _cornerLefVertical = {" 1 ", @" \ ", " 2 "};

        private static string[] _emptySquare = {"000", "000", "000"};

        #endregion

        public static Array[,,] Track3dArray = 
        {
            {
                {_emptySquare, _startHorizontal, _straightHorizontal, _cornerRightHorinzontal},
                {_emptySquare, _emptySquare, _emptySquare, _emptySquare},
                {_emptySquare, _emptySquare, _emptySquare, _emptySquare},
                {_emptySquare, _emptySquare, _emptySquare, _emptySquare}
            }
        };


        public static void Initialize()
        {
        }

        public static void DrawTrack(Track t)
        {
            Draw3dTrackArray(Track3dArray);
        }

        public static void Draw3dTrackArray(Array[,,] arr)
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
                for (int j = 0; j < arr.GetLength(1); j++) {
                    Console.Write("{0} ", arr[i, j, 0]);
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
