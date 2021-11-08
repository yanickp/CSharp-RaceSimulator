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
            //Set the currentrace if there is a new race from the queue
            if (race != null)
            {
                CurrentRace = new Race(race, Competition.Participants);
            }
            else
            {
                CurrentRace = null;
            }
        }

        public static void AddParticipants()
        {
            Car ferrari = new Car(10, 8, 7, false);
            Car honda = new Car(9, 9, 6, false);
            Car mercedes = new Car(10, 8, 8, false);
            IParticipant driver1 = new Driver("Kees", 0, IParticipant.TeamColors.Blue, ferrari);
            IParticipant driver2 = new Driver("Jan", 0, IParticipant.TeamColors.Green, honda);
            IParticipant driver3 = new Driver("Willem", 0, IParticipant.TeamColors.Yellow, mercedes);
            IParticipant driver4 = new Driver("Popie", 0, IParticipant.TeamColors.Grey, honda);
            IParticipant driver5 = new Driver("Holla", 0, IParticipant.TeamColors.Red, ferrari);
            Competition.Participants.Add(driver1);
            Competition.Participants.Add(driver2);
            Competition.Participants.Add(driver3);
            Competition.Participants.Add(driver4);
            Competition.Participants.Add(driver5);
        }

        public static void AddTracks()
        {
            #region TrackZandfoort

            Section.SectionTypes[] track1Sections =
            {
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,

                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,

                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,

                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,

                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Finish,
            };

            #endregion
            //todo deze nog anders maken
            #region TrackBarcelona
            Section.SectionTypes[] track2Sections =
            {
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.StartGrid,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Finish,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.LeftCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.RightCorner,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
                Section.SectionTypes.Straight,
            };
            #endregion

            Track track1 = new Track("zandvoort", track1Sections);
            Track track2 = new Track("barcelona", track2Sections);
            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);
        }
    }
}