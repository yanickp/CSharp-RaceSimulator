using System;
using System.Collections.Generic;

namespace Model
{
    public class RaceFinishedEventArgs : EventArgs
    {
        public Queue<IParticipant> FinishedParticipants { get; set; }

        public RaceFinishedEventArgs(Queue<IParticipant> finishedParticipants)
        {
            FinishedParticipants = finishedParticipants;
        }
    }
}