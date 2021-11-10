using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Model;
using System.Timers;
using Timer = System.Timers.Timer;

public delegate void OnDriversChanged(object sender, DriverChangeEventArgs e);

public delegate void OnRaceIsFinished(object sender, RaceFinishedEventArgs e);

public delegate void OnNextRace(object sender, EventArgs e);


namespace Controller
{
    public class Race
    {
        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }


        private readonly Queue<IParticipant> FinishedParticipants;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private readonly Dictionary<IParticipant, int> _rounds;
        private readonly int DistancePerSection;
        private readonly int RoundsPerRace;
        private static Timer _timer;

        public event OnDriversChanged DriversChanged;
        public event OnRaceIsFinished RaceIsFinnished;
        public event OnNextRace NextRace;


        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            this.Participants = participants;
            this.StartTime = DateTime.Now;
            this._random = new Random(Seed: DateTime.Now.Millisecond);
            this._positions = new Dictionary<Section, SectionData>();
            FinishedParticipants = new Queue<IParticipant>();
            _rounds = new Dictionary<IParticipant, int>();
            DistancePerSection = 100;
            RoundsPerRace = 2;
            _timer = new Timer(500);

            _timer.Elapsed += onTimedEvent;

            ResetParticipantsStats();
            PlaceParticipants();

            //sets the count of the finnished rounds for each perticipant
            foreach (IParticipant p in Participants)
            {
                _rounds.Add(p, 0);
            }

            Start();
        }

        private void ResetParticipantsStats()
        {
            foreach (var p in Participants)
            {
                p.Equipment.Preformance = p.Equipment.startPreformance;
                p.Equipment.Speed = p.Equipment.startSpeed;
                p.Equipment.Quality = p.Equipment.startQuality;
                p.Equipment.IsBroken = false;
            }
        }

        private void Start()
        {
            _timer.Start();
            StartTime = DateTime.Now;
        }

        private void EndRace()
        {
            _timer.Stop();
            _timer.Elapsed -= onTimedEvent;

            RaceIsFinnished?.Invoke(this, new RaceFinishedEventArgs(FinishedParticipants));
            NextRace?.Invoke(this, EventArgs.Empty);
        }

        private void ChangeToBreakEquipment(IParticipant p)
        {
            if (_random.Next(1500) <= (21 - p.Equipment.Quality))
            {
                p.Equipment.IsBroken = true;
            }

            if (p.Equipment.IsBroken)
            {
                if (_random.Next(300) >= 300 - p.Equipment.Quality)
                {
                    p.Equipment.IsBroken = false;
                    if (p.Equipment.Speed > 8)
                    {
                        p.Equipment.Speed--;
                    }
                    else
                    {
                        if (p.Equipment.Preformance >= 8)
                        {
                            p.Equipment.Preformance--;
                        }
                    }
                }
            }
        }

        private void onTimedEvent(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            bool arleadyFinished = false;
            foreach (Section section in track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                foreach (IParticipant participant in Participants)
                {
                    arleadyFinished = false;
                    if (section.SectionType != Section.SectionTypes.StartGrid &&
                        section.SectionType != Section.SectionTypes.Finish)
                        ChangeToBreakEquipment(participant);

                    int speed = participant.Equipment.Preformance * participant.Equipment.Speed;

                    if (sectionData.Left != null)
                    {
                        if (sectionData.Left.Equals(participant))
                        {
                            if (!participant.Equipment.IsBroken)
                            {
                                sectionData.DistanceLeft += speed;

                                if (sectionData.DistanceLeft >= DistancePerSection)
                                {
                                    var next = track.Sections.Find(section)?.Next;
                                    if (track.Sections.First != null)
                                    {
                                        Section nextSection = next != null ? next.Value : track.Sections.First.Value;
                                        SectionData nextSectionData = GetSectionData(nextSection);

                                        if (nextSectionData.AddParticipant(participant))
                                        {
                                            nextSectionData.DistanceLeft =
                                                sectionData.DistanceLeft - DistancePerSection;
                                            sectionData.Left = null;
                                            sectionData.DistanceLeft = 0;
                                            if (section.SectionType == Section.SectionTypes.Finish)
                                                _rounds[participant]++;
                                        }
                                        else
                                        {
                                            sectionData.DistanceLeft = DistancePerSection;
                                        }


                                        if (_rounds[participant] > RoundsPerRace)
                                        {
                                            sectionData.Left = null;
                                            sectionData.DistanceLeft = 0;

                                            nextSectionData.Left = null;
                                            nextSectionData.DistanceLeft = 0;

                                            if (!arleadyFinished)
                                            {
                                                FinishedParticipants.Enqueue(participant);
                                                arleadyFinished = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (sectionData.Right == null) continue;
                    {
                        if (!sectionData.Right.Equals(participant)) continue;
                        if (participant.Equipment.IsBroken) continue;
                        sectionData.DistanceRight += speed;

                        if (sectionData.DistanceRight < DistancePerSection) continue;
                        var next = track.Sections.Find(section)?.Next;
                        if (track.Sections.First == null) continue;

                        Section nextSection = next != null ? next.Value : track.Sections.First.Value;
                        SectionData nextSectionData = GetSectionData(nextSection);
                        //makes sure the next section has room for a driver, else he has to wait at the current section
                        if (nextSectionData.AddParticipant(participant))
                        {
                            nextSectionData.DistanceRight =
                                sectionData.DistanceRight - DistancePerSection;
                            sectionData.Right = null;
                            sectionData.DistanceRight = 0;
                            if (section.SectionType == Section.SectionTypes.Finish)
                                _rounds[participant]++;
                        }
                        else
                        {
                            sectionData.DistanceRight = DistancePerSection;
                        }


                        if (_rounds[participant] <= RoundsPerRace) continue;
                        sectionData.Right = null;
                        sectionData.DistanceRight = 0;

                        nextSectionData.Right = null;
                        nextSectionData.DistanceRight = 0;

                        FinishedParticipants.Enqueue(participant);
                    }
                    if (sectionData.Right != null)
                    {
                        if (sectionData.Right.Equals(participant))
                        {
                            if (!participant.Equipment.IsBroken)
                            {
                                sectionData.DistanceRight += speed;

                                if (sectionData.DistanceRight >= DistancePerSection)
                                {
                                    var next = track.Sections.Find(section)?.Next;
                                    if (track.Sections.First != null)
                                    {
                                        Section nextSection = next != null ? next.Value : track.Sections.First.Value;
                                        SectionData nextSectionData = GetSectionData(nextSection);
                                        //makes sure the next section has room for a driver, else he has to wait at the current section
                                        if (nextSectionData.AddParticipant(participant))
                                        {
                                            nextSectionData.DistanceRight =
                                                sectionData.DistanceRight - DistancePerSection;
                                            sectionData.Right = null;
                                            sectionData.DistanceRight = 0;
                                        }
                                        else
                                        {
                                            sectionData.DistanceRight = DistancePerSection;
                                        }

                                        if (section.SectionType == Section.SectionTypes.Finish)
                                            _rounds[participant]++;

                                        if (_rounds[participant] > RoundsPerRace)
                                        {
                                            sectionData.Right = null;
                                            sectionData.DistanceRight = 0;

                                            nextSectionData.Right = null;
                                            nextSectionData.DistanceRight = 0;
                                            if (!arleadyFinished)
                                            {
                                                FinishedParticipants.Enqueue(participant);
                                                arleadyFinished = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            DriversChanged?.Invoke(this, new DriverChangeEventArgs(track, Participants));


            if (FinishedParticipants.Count == Participants.Count)
            {
                EndRace();
            }

            _timer.Start();
        }


        //if a race with no participants is made
        public Race(Track track) : this(track, new List<IParticipant>())
        {
        }

        public SectionData GetSectionData(Section s)
        {
            if (!_positions.ContainsKey(s))
            {
                _positions.Add(s, new SectionData());
            }

            return _positions[s];
        }

        private void PlaceParticipants()
        {
            //able to add up to infinite drivers!
            Section startSection = track.GetStartSection();
            SectionData SectionData = GetSectionData(startSection);
            foreach (var participant in Participants)
            {
                if (SectionData.AddParticipant(participant))
                {
                    continue;
                }

                var previousSection = track.GetPReviousSection(startSection);
                var previousSectionData = GetSectionData(previousSection);
                while (!previousSectionData.AddParticipant(participant))
                {
                    previousSection = track.GetPReviousSection(previousSection);
                    previousSectionData = GetSectionData(previousSection);
                    continue;
                }
            }
        }
    }
}