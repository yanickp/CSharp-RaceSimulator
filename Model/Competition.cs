using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public string name { get; set; }

        public Competition(string name)
        {
            this.name = name;
            this.Participants = new List<IParticipant>();
            this.Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            if (Tracks.Count != 0)
            {
                return Tracks.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}