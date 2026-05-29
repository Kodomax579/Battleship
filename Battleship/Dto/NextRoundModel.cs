using Battleship.Model;

namespace Battleship.Dto
{
    public class NextRoundModel
    {
        public string GameId { get; set; } = string.Empty;
        public int ActivePlayerIndex { get; set; }
        public PositionModel ShootPosition { get; set; } = new();
        public bool shootHit { get; set; }
        public bool isShipDown { get; set; }
    }
}
