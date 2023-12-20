using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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
        /// update auth entity
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public new async Task<Miccore.CleanArchitecture.Auth.Core.Entities.Role> UpdateAsync(Miccore.CleanArchitecture.Auth.Core.Entities.Role entity)
        {
            var role = await _context.Set<Core.Entities.Role>().FirstOrDefaultAsync(x => x.Id == entity.Id && (x.DeletedAt == 0 || x.DeletedAt == null)) ?? throw new NotFoundException(ExceptionEnum.ROLE_NOT_FOUND.ToString());

            role = SetValueForUpdateAsync(entity, role);
            role.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return role;
        }

    }
}