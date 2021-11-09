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

        private static int XOffset;
        private static int YOffset;
        private static int Compas;
        private static Dictionary<string, string[]> trackDictionary;

        public static void Initialize()
        {
            XOffset = Console.CursorLeft;
            YOffset = Console.CursorTop;
            Compas = 0;
            trackDictionary = new Dictionary<string, string[]>();
            MakeTrackDictionary();

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

                        Compas--;
                        if (Compas == -1)
                            Compas = 3;

                        UpdateCursorPosition();
                        break;

                    case Section.SectionTypes.RightCorner:
                        DrawSection(section);

                        Compas++;
                        if (Compas == 4)
                            Compas = 0;

                        UpdateCursorPosition();
                        break;

                    default:
                        DrawSection(section);
                        UpdateCursorPosition();
                        break;
                }
            }
        }


        private static void StartNextRace(object sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceIsFinnished -= Data.Competition.OnRaceIsFinished;
            Data.CurrentRace.NextRace -= StartNextRace;

            Data.NextRace();
            if (Data.CurrentRace == null) return;
            Initialize();
            DrawTrack(Data.CurrentRace.track);
        }

        private static void DrawSection(Section section)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            string[] lines =
                DrawParticipants(trackDictionary[$"_{section.SectionType}{Compas}"], sectionData);

            foreach (string line in lines)
            {
                Console.SetCursorPosition(YOffset, XOffset);
                Console.Write(line);
                XOffset++;
            }

            XOffset -= 4;
        }

        private static string[] DrawParticipants(string[] trackArray, SectionData sectionData)
        {
            string[] newTrackArray = new string[trackArray.Length];

            for (int i = 0; i < trackArray.Length; i++)
                newTrackArray[i] = trackArray[i];

            if (sectionData.Left != null)
                for (int i = 0; i < newTrackArray.Length; i++)
                    newTrackArray[i] = newTrackArray[i].Replace("1",
                        sectionData.Left.Equipment.IsBroken ? "*" : sectionData.Left.Name[..1]);
            else
                for (int i = 0; i < newTrackArray.Length; i++)
                    newTrackArray[i] = newTrackArray[i].Replace("1", " ");

            if (sectionData.Right != null)
                for (int i = 0; i < newTrackArray.Length; i++)
                    newTrackArray[i] = newTrackArray[i].Replace("2",
                        sectionData.Right.Equipment.IsBroken ? "*" : sectionData.Right.Name[..1]);
            else
                for (int i = 0; i < newTrackArray.Length; i++)
                    newTrackArray[i] = newTrackArray[i].Replace("2", " ");

            return newTrackArray;
        }

        private static void UpdateCursorPosition()
        {
            switch (Compas)
            {
                case 0:
                    XOffset -= 4;
                    break;
                case 1:
                    YOffset += 4;
                    break;
                case 2:
                    XOffset += 4;
                    break;
                case 3:
                    YOffset -= 4;
                    break;
            }
        }

        private static void MakeTrackDictionary()
        {
            trackDictionary.Add("_StartGrid0", _StartGrid0);
            trackDictionary.Add("_StartGrid1", _StartGrid1);
            trackDictionary.Add("_StartGrid2", _StartGrid2);
            trackDictionary.Add("_StartGrid3", _StartGrid3);

            trackDictionary.Add("_Straight0", _Straight0);
            trackDictionary.Add("_Straight1", _Straight1);
            trackDictionary.Add("_Straight2", _Straight2);
            trackDictionary.Add("_Straight3", _Straight3);

            trackDictionary.Add("_RightCorner0", _RightCorner0);
            trackDictionary.Add("_RightCorner1", _RightCorner1);
            trackDictionary.Add("_RightCorner2", _RightCorner2);
            trackDictionary.Add("_RightCorner3", _RightCorner3);

            trackDictionary.Add("_LeftCorner0", _LeftCorner0);
            trackDictionary.Add("_LeftCorner1", _LeftCorner1);
            trackDictionary.Add("_LeftCorner2", _LeftCorner2);
            trackDictionary.Add("_LeftCorner3", _LeftCorner3);

            trackDictionary.Add("_Finish0", _Finish0);
            trackDictionary.Add("_Finish1", _Finish1);
            trackDictionary.Add("_Finish2", _Finish2);
            trackDictionary.Add("_Finish3", _Finish3);
        }
    }
}