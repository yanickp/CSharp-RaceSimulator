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
            ShowScoreBoard(e);
            Thread.Sleep(5000);
        }

        private void ShowScoreBoard(RaceFinishedEventArgs e)
        {
            Console.Clear();
            int index = 1;
            while (e.FinishedParticipants.Count > 0)
            {
                IParticipant p = e.FinishedParticipants.Dequeue();
                Console.WriteLine($"{index}e: {p.Name} met snelheid: {p.Equipment.startSpeed}, performance van {p.Equipment.Preformance}, en een qualiteit van {p.Equipment.Quality} voor team {p.TeamColor}");
                index++;
            }
        }

        public Track NextTrack()
        {
            return Tracks.Any() ? Tracks.Dequeue() : null;
        }
    }
}