namespace Battleship.Model
{
    public class PlayerModel
    {
        public string PlayerName { get; set; } = string.Empty;
        public List<ShipModel> Ships { get; set; } = new();
        public List<ShootModel> Shoots { get; set; } = new();
    }
}
