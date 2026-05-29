namespace Battleship.Model
{
    public class ShipModel
    {
        public string Name { get; set; } = string.Empty;
        public List<ShipPosition> ShipPositions { get; set; } = new();
    }
}
