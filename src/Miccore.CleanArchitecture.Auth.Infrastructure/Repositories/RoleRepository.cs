using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories.Base;

namespace Miccore.CleanArchitecture.Auth.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Miccore.CleanArchitecture.Auth.Core.Entities.Role>, IRoleRepository
    {
        /// <summary>
        /// Auth repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public RoleRepository(AuthApplicationDbContext context) : base(context) { }
        
        /// <summary>
        /// soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task DeleteAsync(int id)
        {
            var entity = await _context.Roles.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundException(ExceptionEnum.ROLE_NOT_FOUND.ToString());
            }
            entity.DeletedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update auth entity
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public new async Task<Miccore.CleanArchitecture.Auth.Core.Entities.Role> UpdateAsync(Miccore.CleanArchitecture.Auth.Core.Entities.User entity)
        {
            var role = await _context.Roles.FindAsync(entity.Id);
            if (role is null)
            {
                throw new NotFoundException(ExceptionEnum.ROLE_NOT_FOUND.ToString());
            }

            role.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return role;
        }

    }
}