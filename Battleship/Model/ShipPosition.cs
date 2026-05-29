namespace Battleship.Model
{
    public class ShipPosition
    {
        public PositionModel Position { get; set; } = new();
        public bool Hit { get; set; }
    }
}
