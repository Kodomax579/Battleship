using Battleship.Dto;
using System.Collections.Concurrent;

namespace Battleship.Services
{
    public class QueueService
    {
        ConcurrentQueue<(string ConnectionId, StartModel Data)>  _queue = new();

        public event Action<(string ConnectionId, StartModel Data), (string ConnectionId, StartModel Data)> MatchFound;

        public void SetQueue(StartModel start, string connectionId)
        {
            // if there is someone waiting, the if will execute
            if (_queue.TryDequeue(out var opponent))
            {
                var currentPlayer = (connectionId, start);

                MatchFound?.Invoke(currentPlayer, opponent);
            }
            else
            {
                // no one is waiting, so I wait
                _queue.Enqueue((connectionId, start));
            }
        }
    }
}
