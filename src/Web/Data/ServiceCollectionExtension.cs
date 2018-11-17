using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories;
using Web.Repositories.Implementations;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddQuizGameData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDefaultIdentity<User>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
