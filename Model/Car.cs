namespace Model
{
    public class Car : IEquipment
    {
        public int Quality { get; set; }
        public int Preformance { get; set; }
        public int Speed { get; set; }
        public bool IsBroken { get; set; }
        
        public int startPreformance { get; set; }
        public int startSpeed { get; set; }
        public int startQuality { get; set; }

        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Preformance = performance;
            Speed = speed;
            IsBroken = isBroken;
            
            startPreformance = performance;
            startSpeed = speed;
            startQuality = quality;
        }
    }
}