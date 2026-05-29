using Battleship.Dto;
using Battleship.Model;
using Microsoft.AspNetCore.SignalR;

namespace Battleship.Services
{
    public class GameService
    {
        private Dictionary<string, BattleshipGameModel> battleshipGameStack = new();
        private readonly IHubContext<BattleshipHub.BattleshipHub> _battleshipHub;

        public GameService(QueueService queueService, IHubContext<BattleshipHub.BattleshipHub> battleshipHub)
        {
            queueService.MatchFound += CreateGame;
            battleshipHub = _battleshipHub;
        }

        public void HandleShoot(ShootDto shoot)
        {
            var game = battleshipGameStack[shoot.Game_Id];
            if (game == null)
            {
                return;
            }

            var enemyId = 1 - game.ActivePlayerIndex;

            game.Players[game.ActivePlayerIndex].Shoots.Add(new()
            {
                Position = shoot.Position,
            });

            var targetShip = game.Players[enemyId].Ships
                .SelectMany(ship => ship.ShipPositions) 
                .FirstOrDefault(p => p.Position.x == shoot.Position.x && p.Position.y == shoot.Position.y);

            

            game.ActivePlayerIndex = enemyId;

        }

        private async void CreateGame((string ConnectionId, StartModel Data) player1, (string ConnectionId, StartModel Data) player2)
        {
            var player1Connection = player1.ConnectionId;
            var player2Connection = player2.ConnectionId;

            var player1Data = player1.Data;
            var player2Data = player2.Data;
            BattleshipGameModel battleshipModel = CreateBattleshipGameModel(player1Data, player2Data);

            battleshipGameStack.Add(battleshipModel.Game_Id, battleshipModel);

            var nextRoundModel = CreateFirstNextTurnModel(battleshipModel);

            await _battleshipHub.Groups.AddToGroupAsync(player1Connection, battleshipModel.Game_Id);
            await _battleshipHub.Groups.AddToGroupAsync(player2Connection, battleshipModel.Game_Id);

            await _battleshipHub.Clients.Group(battleshipModel.Game_Id).SendAsync("NextRoundModel", nextRoundModel);
        }

        private NextRoundModel CreateFirstNextTurnModel(BattleshipGameModel battleshipModel)
        {
            return new NextRoundModel
            {
                ActivePlayerIndex = battleshipModel.ActivePlayerIndex,
                GameId = battleshipModel.Game_Id,
                isShipDown = false,
                shootHit = false,
                ShootPosition = new()
            };
        }

        private NextRoundModel CreateNextTurnModel()
        {
            return new NextRoundModel
            {
                ActivePlayerIndex = battleshipModel.ActivePlayerIndex,
                GameId = battleshipModel.Game_Id,
                isShipDown = false,
                shootHit = false,
                ShootPosition = new()
            };
        }

        private static BattleshipGameModel CreateBattleshipGameModel(StartModel player1Data, StartModel player2Data)
        {
            return new BattleshipGameModel()
            {
                Game_Id = Guid.NewGuid().ToString(),
                ActivePlayerIndex = 1,
                Players = new List<PlayerModel>
                {
                    new PlayerModel
                    {
                        PlayerName = player1Data.PlayerName,
                        Ships = player1Data.ShipModels.Select(ship => new ShipModel
                        {
                            Name = ship.Name,
                            ShipPositions = ship.Positions.Select(position => new ShipPosition
                            {
                                Hit = false,
                                Position = new PositionModel
                                {
                                    x = position.x,
                                    y = position.y
                                }
                            }).ToList()
                        }).ToList(),
                        Shoots = new()
                    },
                    new PlayerModel
                    {
                        PlayerName = player2Data.PlayerName,
                        Ships = player2Data.ShipModels.Select(ship => new ShipModel
                        {
                            Name = ship.Name,
                            ShipPositions = ship.Positions.Select(position => new ShipPosition
                            {
                                Hit = false,
                                Position = new PositionModel
                                {
                                    x = position.x,
                                    y = position.y
                                }
                            }).ToList()
                        }).ToList(),
                        Shoots = new()
                    }
                }
            };
        }
    }
}
