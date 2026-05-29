using Battleship.BattleshipHub;
using Battleship.Services;

namespace Battleship
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();

            builder.Services.AddSingleton<QueueService>();
            builder.Services.AddSingleton<GameService>();

            var app = builder.Build();
            
            app.MapHub<BattleshipHub.BattleshipHub>("/Battleship");

            app.Run();
        }
    }
}
