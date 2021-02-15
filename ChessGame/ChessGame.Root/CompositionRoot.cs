using ChessGame.Business.Contracts.Services;
using ChessGame.Business.Services;
using ChessGame.Data;
using ChessGame.Data.Contracts;
using ChessGame.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChessGame.Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            // ChessGame.Data dependencies
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();

            // ChessGame.Business dependencies
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IGameService, GameService>();

            // DBContext
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("WatersChessGameConnection"),
                    sqlServerOptionsAction => sqlServerOptionsAction.MigrationsAssembly("ChessGame.Data")));

            // force database to start up
            services.AddHostedService<DatabaseStartup>();

            // Automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(PlayerService)));


        }
    }
}
