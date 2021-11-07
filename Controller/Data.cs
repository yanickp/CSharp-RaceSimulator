using System;
using System.Collections.Generic;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition("eerste compitition");
            AddParticipants();
            AddTracks();
        }

        public static void NextRace()
        {
            Track race = Competition.NextTrack();
            //Set the currentrace if there is a new race from the que
            if (race != null)
            {
                CurrentRace = new Race(race, Competition.Participants);
            }
        }

        public static void AddParticipants()
        {
            IParticipant driver1 = new Driver("kees", 0, IParticipant.TeamColors.Blue);
            IParticipant driver2 = new Driver("jan", 0, IParticipant.TeamColors.Green);
            IParticipant driver3 = new Driver("willem", 0, IParticipant.TeamColors.Yellow);
            Competition.Participants.Add(driver1);
            Competition.Participants.Add(driver2);
            Competition.Participants.Add(driver3);
        }

        public static void AddTracks()
        {
            Section.SectionTypes[] track1Sections =
            {
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Finish
            };
            Track track1 = new Track("zandvoort", track1Sections);
            Track track2 = new Track("barcelona", track1Sections);
            Track track3 = new Track("london", track1Sections);
            Track track4 = new Track("zeeland", track1Sections);
            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
            Competition.Tracks.Enqueue(track3);
            Competition.Tracks.Enqueue(track4);
        }
    }
}