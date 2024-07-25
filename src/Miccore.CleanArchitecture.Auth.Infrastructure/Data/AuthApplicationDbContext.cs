using Microsoft.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Auth.Infrastructure.Data
{
    /// <summary>
    /// Database context registration
    /// </summary>
    public class AuthApplicationDbContext : DbContext
    {
        public AuthApplicationDbContext(DbContextOptions<AuthApplicationDbContext> options) : base(options) { }

        #region dbset

        public DbSet<Core.Entities.User> Users
        {
            get;
            set;
        }

        public DbSet<Core.Entities.Role> Roles
        {
            get;
            set;
        }

        #endregion
    }
}