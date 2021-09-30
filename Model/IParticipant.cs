using System.Security.Principal;

namespace Model
{
    public interface IParticipant
    {
        enum TeamColors
        {
            Red,
            Yellow,
            Grey,
            Green,
            Blue
        }

        string Name { get; set; }
        int Points { get; set; }
        IEquipment Equipment { get; set; }
        TeamColors TeamColor { get; set; }
    }
}