using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Model;

namespace Controller
{
    public class Race
    {
        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private readonly Dictionary<IParticipant, int> _rounds;
        private readonly int DistancePerSection;
        private readonly int RoundsPerRace;


        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            Participants = participants;
            this.StartTime = DateTime.Now;
            this._random = new Random(DateTime.Now.Millisecond);
            this._positions = new Dictionary<Section, SectionData>();
            _rounds = new Dictionary<IParticipant, int>();
            DistancePerSection = 100;
            RoundsPerRace = 2;
            
        }

        public Race(Track track)
        {
            this.track = track;
            Participants = new List<IParticipant>();
            this.StartTime = DateTime.Now;
            this._random = new Random(DateTime.Now.Millisecond);
            this._positions = new Dictionary<Section, SectionData>();
        }

        public void RandomizeEquipmnet()
        {
            Random rnd = new Random();
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Preformance = rnd.Next(10);
                participant.Equipment.Quality = rnd.Next(10);
            }
        }
        
        public SectionData GetSectionData(Section s)
        {
            
            if (!_positions.ContainsKey(s))
            {
                _positions.Add(s, new SectionData());
            }

            return _positions[s];
            
            // if (_positions.TryGetValue(s, out SectionData value))
            // {
            //     return value;
            // }
            // // als er geen data is op die plek nieuwe aanmaken
            // SectionData temp = new SectionData();
            // _positions.Add(s, temp);
            // return temp;
        }

        public void PlaceParticipants()
        {
            var section = this.track.GetStartSection();
            var SectionData = this.GetSectionData(section);
            foreach (var participant in Participants)
            {
                SectionData.AddParticipant(participant);
            }

        }
    }
}