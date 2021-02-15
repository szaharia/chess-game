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
            // DAL SERVICES
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.Decorate<IPlayerRepository, PlayerRepositoryWithLogging>();

            services.AddScoped<IGameRepository, GameRepository>();
            services.Decorate<IGameRepository, GameRepositoryWithLogging>();

            // DBContext
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(
                    configuration.GetConnectionString("WatersChessGameConnection"),
                    sqlServerOptionsAction => sqlServerOptionsAction.MigrationsAssembly("ChessGame.Data")));

            // force database to be created
            services.AddHostedService<DatabaseStartup>();

            // BLL SERVICES
            services.AddScoped<IPlayerService, PlayerService>();
            services.Decorate<IPlayerService, PlayerServiceWithLogging>();

            services.AddScoped<IGameService, GameService>();
            services.Decorate<IGameService, GameServiceWithLogging>();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(PlayerService)));
        }
    }
}
