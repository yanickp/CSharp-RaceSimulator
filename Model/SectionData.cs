using System;
using System.IO.MemoryMappedFiles;

namespace Model
{
    public class SectionData
    {
        
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }

        public SectionData(IParticipant iLeft, IParticipant iRight, int disleft, int disright)
        {
            this.Left = iLeft;
            this.Right = iRight;
            this.DistanceLeft = disleft;
            this.DistanceRight = disright;
        }

        public SectionData()
        {
            this.Left = null;
            this.Right = null;
            this.DistanceLeft = 0;
            this.DistanceRight = 0;
        }
        public SectionData(IParticipant left, int DistanceLeft, IParticipant right, int DistanceRight)
        {
            this.Left = left;
            this.DistanceLeft = DistanceLeft;
            this.Right = right;
            this.DistanceRight = DistanceRight;
        }

        public bool AddParticipant(IParticipant p)
        {
            if (Left == null)
            {
                Left = p;
                DistanceLeft = 0;
                return true;
            }
            if(Right == null)
            {
                Right = p;
                DistanceRight = 0;
                return true;
            }

            return false;
        }

    }
}