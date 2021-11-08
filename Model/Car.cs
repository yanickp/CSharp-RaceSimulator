namespace Model
{
    public class Car : IEquipment
    {
        public int Quality { get; set; }
        public int Preformance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }

        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Preformance = performance;
            Speed = speed;
            IsBroken = isBroken;
        }
    }
}