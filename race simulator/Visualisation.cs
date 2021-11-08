using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Controller;
using Model;

namespace race_simulator
{
    public static class Visualisation
    {
        #region graphics

        private static readonly string[] _StartGrid0 = {"║  ║", "║--║", "║--║", "║12║"};
        private static readonly string[] _StartGrid1 = {"════", "1-- ", "2-- ", "════"};
        private static readonly string[] _StartGrid2 = {"║12║", "║--║", "║--║", "║  ║"};
        private static readonly string[] _StartGrid3 = {"════", " --1", " --2", "════"};

        private static readonly string[] _Straight0 = {"║  ║", "║12║", "║  ║", "║  ║"};
        private static readonly string[] _Straight1 = {"════", " 1  ", " 2  ", "════"};
        private static readonly string[] _Straight2 = {"║  ║", "║  ║", "║12║", "║  ║"};
        private static readonly string[] _Straight3 = {"════", "  1 ", "  2 ", "════"};

        private static readonly string[] _RightCorner0 = {"╔═══", "║   ", "║1 2", "║  ╔"};
        private static readonly string[] _RightCorner1 = {"═══╗", "   ║", "1 2║", "╗  ║"};
        private static readonly string[] _RightCorner2 = {"╝  ║", "1 2║", "   ║", "═══╝"};
        private static readonly string[] _RightCorner3 = {"║  ╚", "║1 2", "║   ", "╚═══"};

        private static readonly string[] _LeftCorner0 = {"═══╗ ", "   ║", "1 2║", "═  ║"};
        private static readonly string[] _LeftCorner1 = {"═  ║", "1 2║", "   ║", "═══╝"};
        private static readonly string[] _LeftCorner2 = {"║  ═", "║1 2", "║   ", "╚═══"};
        private static readonly string[] _LeftCorner3 = {"╔═══", "║   ", "║1  2", "║  ╔"};

        private static readonly string[] _Finish0 = {"║12║", "║##║", "║##║", "║  ║"};
        private static readonly string[] _Finish1 = {"════", "  #1", "  #2", "════"};
        private static readonly string[] _Finish2 = {"║  ║", "║##║", "║##║", "║12║"};
        private static readonly string[] _Finish3 = {"════", " 1# ", " 2# ", "════"};

        #endregion graphics

        private static int CursorLeft;
        private static int CursorTop;
        private static int CurrentDirection;
        private static Dictionary<string, string[]> Graphics;

        public static void Initialize()
        {
            CursorLeft = Console.CursorLeft;
            CursorTop = Console.CursorTop;
            CurrentDirection = 0;
            Graphics = new Dictionary<string, string[]>();
            FillGraphicsDictionary();

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceIsFinnished += Data.Competition.OnRaceIsFinished;
            Data.CurrentRace.NextRace += StartNextRace;
        }

        public static void OnDriversChanged(object sender, DriverChangeEventArgs e)
        {
            DrawTrack(e.Track);
        }


        public static void DrawTrack(Track track)
        {
            Console.Clear();
            foreach (Section section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case Section.SectionTypes.LeftCorner:
                        DrawSection(section);

                        CurrentDirection--;
                        if (CurrentDirection == -1)
                            CurrentDirection = 3;

                        UpdateCursorPosition();
                        break;

                    case Section.SectionTypes.RightCorner:
                        DrawSection(section);

                        CurrentDirection++;
                        if (CurrentDirection == 4)
                            CurrentDirection = 0;

                        UpdateCursorPosition();
                        break;

                    default:
                        DrawSection(section);
                        UpdateCursorPosition();
                        break;
                }
            }
        }


        public static void StartNextRace(object sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceIsFinnished -= Data.Competition.OnRaceIsFinished;
            Data.CurrentRace.NextRace -= StartNextRace;

            Data.NextRace();
            if (Data.CurrentRace != null)
            {
                Initialize();
                DrawTrack(Data.CurrentRace.track);
            }
        }

        private static void DrawSection(Section section)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            string[] lines =
                AddParticipantsToGraphics(Graphics[$"_{section.SectionType}{CurrentDirection}"], sectionData);

            foreach (string line in lines)
            {
                Console.SetCursorPosition(CursorTop, CursorLeft);
                Console.Write(line);
                CursorLeft++;
            }

            CursorLeft -= 4;
        }

//todo refactor
        private static string[] AddParticipantsToGraphics(string[] graphics, SectionData sectionData)
        {
            string[] newGraphics = new string[graphics.Length];

            for (int i = 0; i < graphics.Length; i++)
                newGraphics[i] = graphics[i];

            if (sectionData.Left != null)
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("1",
                        sectionData.Left.Equipment.IsBroken ? "x" : sectionData.Left.Name.Substring(0, 1));
            else
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("1", " ");

            if (sectionData.Right != null)
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("2",
                        sectionData.Right.Equipment.IsBroken ? "x" : sectionData.Right.Name.Substring(0, 1));
            else
                for (int i = 0; i < newGraphics.Length; i++)
                    newGraphics[i] = newGraphics[i].Replace("2", " ");

            return newGraphics;
        }

        private static void UpdateCursorPosition()
        {
            if (CurrentDirection == 0)
                CursorLeft -= 4;
            else if (CurrentDirection == 1)
                CursorTop += 4;
            else if (CurrentDirection == 2)
                CursorLeft += 4;
            else if (CurrentDirection == 3)
                CursorTop -= 4;
        }

        private static void FillGraphicsDictionary()
        {
            //todo refactor
            Graphics.Add("_StartGrid0", _StartGrid0);
            Graphics.Add("_StartGrid1", _StartGrid1);
            Graphics.Add("_StartGrid2", _StartGrid2);
            Graphics.Add("_StartGrid3", _StartGrid3);

            Graphics.Add("_Straight0", _Straight0);
            Graphics.Add("_Straight1", _Straight1);
            Graphics.Add("_Straight2", _Straight2);
            Graphics.Add("_Straight3", _Straight3);

            Graphics.Add("_RightCorner0", _RightCorner0);
            Graphics.Add("_RightCorner1", _RightCorner1);
            Graphics.Add("_RightCorner2", _RightCorner2);
            Graphics.Add("_RightCorner3", _RightCorner3);

            Graphics.Add("_LeftCorner0", _LeftCorner0);
            Graphics.Add("_LeftCorner1", _LeftCorner1);
            Graphics.Add("_LeftCorner2", _LeftCorner2);
            Graphics.Add("_LeftCorner3", _LeftCorner3);

            Graphics.Add("_Finish0", _Finish0);
            Graphics.Add("_Finish1", _Finish1);
            Graphics.Add("_Finish2", _Finish2);
            Graphics.Add("_Finish3", _Finish3);
        }
    }
}