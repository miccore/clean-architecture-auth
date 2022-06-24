using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.CleanArchitecture.Auth.Core.Repositories.Base;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Miccore.CleanArchitecture.Auth.Infrastructure.Persistances
{
    /// <summary>
    /// dependency injection of database injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// add db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // get connection string from environment file
            var connectionString = configuration.GetConnectionString("AuthDB");

            // database connexion
            services.AddDbContext<AuthApplicationDbContext>(option =>
            {
                option.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            }, ServiceLifetime.Scoped);

            // add repositories
            #region repositories

            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IRoleRepository, RoleRepository>();

            #endregion

            return services;
        }

    }
}