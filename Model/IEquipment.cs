namespace Model
{
    public interface IEquipment
    {
        public int Quality { get; set; }
        public int Preformance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
        
        public int startPreformance { get; set; }
        public int startSpeed { get; set; }
        public int startQuality { get; set; }
        
    }
}