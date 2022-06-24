using System.Diagnostics.Contracts;
using Miccore.CleanArchitecture.Auth.Core.Entities;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using Miccore.CleanArchitecture.Auth.Infrastructure.Data;
using Miccore.CleanArchitecture.Auth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Auth.Infrastructure.Repositories
{
    public class UserRepository : Repository<Miccore.CleanArchitecture.Auth.Core.Entities.User>, IUserRepository
    {
        /// <summary>
        /// Auth repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UserRepository(AuthApplicationDbContext context) : base(context) { }
        
        /// <summary>
        /// soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task DeleteAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }
            entity.DeletedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get user by email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<User> GetSingleByEmailAsync(string Email)
        {
             var user = await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Email == Email);
            

            if (user is null){
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            return user;
        }

        /// <summary>
        /// get user by phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<User> GetSingleByPhoneAsync(string phone)
        {
            var user = await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Phone == phone);
            

            if (user is null){
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            return user;

        }

        /// <summary>
        /// get user by refresh token
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public async Task<User> GetSingleByRefreshTokenAsync(string refresh)
        {
            var user = await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.RefreshToken == refresh);
            

            if (user is null){
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            return user;
        }

        /// <summary>
        /// update auth entity
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public new async Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> UpdateAsync(Miccore.CleanArchitecture.Auth.Core.Entities.User entity)
        {
            Contract.Requires(entity is not null);

            var user = await _context.Users.FindAsync(entity.Id);
            if (user is null)
            {
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            user.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// update user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public async Task<User> UpdatePasswordAsync(int id, string newPassword)
        {
            
            var user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            user.Password = newPassword;
            user.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// update refresh token of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateRefreshTokenAsync(User user)
        {
            Contract.Requires(user is not null);

            var userGet = await _context.Users.FindAsync(user.Id);
            if (userGet is null)
            {
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND.ToString());
            }

            userGet.RefreshToken = user.RefreshToken;
            userGet.UpdatedAt = DateUtils.GetCurrentTimeStamp();

            await _context.SaveChangesAsync();

            return user;
        }
    }
}