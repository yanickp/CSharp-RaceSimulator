using System;
using System.IO.MemoryMappedFiles;

namespace Model
{
    public class SectionData : IParticipant
    {
     
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public IParticipant.TeamColors TeamColor { get; set; }
        private IParticipant Left { get; set; }
        private int DistanceLeft { get; set; }
        private IParticipant Right { get; set; }
        private int DistanceRight { get; set; }

        public SectionData(IParticipant iLeft, IParticipant iRight, int disleft, int disright)
        {
            this.Left = iLeft;
            this.Right = iRight;
            this.DistanceLeft = disleft;
            this.DistanceRight = disright;
        }

    }
}