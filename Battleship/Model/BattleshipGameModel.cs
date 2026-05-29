namespace Battleship.Model
{
    public class BattleshipGameModel
    {
        public string Game_Id { get; set; } = string.Empty;
        public int ActivePlayerIndex { get; set; }
        public List<PlayerModel> Players { get; set; } = new();
    }
}
