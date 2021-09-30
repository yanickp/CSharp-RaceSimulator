namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public IParticipant.TeamColors TeamColor { get; set; }

        public Driver(string name, int points, IEquipment equipment, IParticipant.TeamColors teamColor)
        {
            this.Name = name;
            this.Equipment = equipment;
            this.Points = points;
            this.TeamColor = teamColor;
        }

        public Driver(string name, int points, IParticipant.TeamColors teamColor)
        {
            this.Name = name;
            this.Points = points;
            this.TeamColor = teamColor;
            this.Equipment = null;
        }
    }
}