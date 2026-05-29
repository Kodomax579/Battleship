using Battleship.Model;

namespace Battleship.Dto
{
    public class ShootDto
    {
        public string Game_Id { get; set; } = string.Empty;
        public PositionModel Position { get; set; } = new();
    }
}
