using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public void OnRaceIsFinished(object sender, RaceFinishedEventArgs e)
        {
            //award points
            Console.Clear();
            Console.WriteLine("Race finnished");
            Console.WriteLine($"{e.FinishedParticipants.Dequeue().Name} heeft gewonnen");
            Thread.Sleep(1000);
        }

        public Track NextTrack()
        {
            return Tracks.Any() ? Tracks.Dequeue() : null;
        }
    }
}