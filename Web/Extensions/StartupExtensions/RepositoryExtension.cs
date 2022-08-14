using Data.MongoDb.Repositories;
using Data.MongoDb.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions.StartupExtensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFinantialResultRepository, FinantialResultRepository>();
        }
    }
}