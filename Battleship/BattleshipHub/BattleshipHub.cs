using Battleship.Dto;
using Battleship.Model;
using Battleship.Services;
using Microsoft.AspNetCore.SignalR;

namespace Battleship.BattleshipHub
{
    public class BattleshipHub (QueueService queueService, GameService gameService) : Hub
    {
        public Task JoinQueue(StartModel start)
        {
            if(start == null)
            {
                return Task.CompletedTask;
            }

            queueService.SetQueue(start, Context.ConnectionId);
            return Task.CompletedTask;
        }

        public void Shoot(ShootDto shoot)
        {
            gameService.HandleShoot(shoot);
        }
    }
}
