using System;
using System.Collections.Generic;

namespace Model
{
    public class DriverChangeEventArgs : EventArgs
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }

        public DriverChangeEventArgs(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
        }

    }
}