using Battleship.Model;

namespace Battleship.Dto
{
    public class StartModel
    {
        public string PlayerName { get; set; } = string.Empty;
        public List<ShipModelDto> ShipModels { get; set; } = new();
    }
}
