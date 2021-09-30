namespace Model
{
    public interface IEquipment
    {
        public int Quality { get; set; }
        public int Preformance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
    }
}