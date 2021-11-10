using System.Security.Principal;

namespace Model
{
    public interface IParticipant
    {
        enum TeamColors
        {
            Red,
            Blue,
            Yellow,
            Grey,
            Green,
            purple,
        }

        string Name { get; set; }
        string teamColourText { get; set; }
        int Points { get; set; }
        IEquipment Equipment { get; set; }
        TeamColors TeamColor { get; set; }
    }
    
}