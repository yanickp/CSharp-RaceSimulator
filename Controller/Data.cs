using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        private static Random _random;


        public static void Initialize()
        {
            _random = new Random();
            Competition = new Competition("eerste compitition");
            AddParticipants(3);
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

        public static void AddParticipants(int amount)
        {
            if (amount > 6)
            {
                throw new Exception();
            }

            string[] names = new[] {"Kees", "Jan", "Willem", "Peter", "henk", "Steve"};
            IParticipant.TeamColors[] teams =
                Enum.GetValues(typeof(IParticipant.TeamColors)) as IParticipant.TeamColors[];

            for (int i = 0; i < amount; i++)
            {
                Car car = new Car(_random.Next(1, 20), _random.Next(8, 12), _random.Next(8, 10), false);
                string name = names[i];
                IParticipant.TeamColors team = teams[i];
                IParticipant driver = new Driver(name, 0, team, car);
                Competition.Participants.Add(driver);
            }
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

            if (ValidateTrack(track1Sections))
            {
                Track track1 = new Track("zandvoort", track1Sections);
                Competition.Tracks.Enqueue(track1);
            }

            if (ValidateTrack(track2Sections))
            {
                Track track2 = new Track("barcelona", track2Sections);
                Competition.Tracks.Enqueue(track2);
            }
        }

        private static bool ValidateTrack(Section.SectionTypes[] sections)
        {
            Queue<bool> RightCorners = new Queue<bool>();
            RightCorners.Enqueue(true);
            RightCorners.Enqueue(true);
            RightCorners.Enqueue(true);
            RightCorners.Enqueue(true);
            if (!sections.Contains(Section.SectionTypes.Finish))
            {
                return false;
            }

            if (!sections.Contains(Section.SectionTypes.StartGrid))
            {
                return false;
            }

            foreach (var s in sections)
            {
                if (s.Equals(Section.SectionTypes.RightCorner))
                {
                    try
                    {
                        RightCorners.Dequeue();

                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

                if (s.Equals(Section.SectionTypes.LeftCorner))
                {
                    RightCorners.Enqueue(true);
                }
            }

            if (RightCorners.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}