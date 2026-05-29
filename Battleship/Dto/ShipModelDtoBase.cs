using Battleship.Model;

namespace Battleship.Dto
{
    public class ShipModelDto
    {
        public string Name { get; set; } = string.Empty;
        public List<PositionModel> Positions { get; set; } = new();
    }
}