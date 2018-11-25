using Web.Repositories;
using Web.Repositories.Implementations;
using Web.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddQuizGame(this IServiceCollection services)
        {
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IUserGameSessionRepository, UserGameSessionRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            return services;
        }
    }
}
