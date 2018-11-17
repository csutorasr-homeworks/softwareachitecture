using Web.Repositories;
using Web.Repositories.Implementations;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddQuizGame(this IServiceCollection services)
        {
            services.AddTransient<IGameRepository, GameRepository>();
            return services;
        }
    }
}
