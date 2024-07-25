using Miccore.CleanArchitecture.Auth.Core.Repositories.Base;

namespace Miccore.CleanArchitecture.Auth.Core.Repositories
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository : IRepository<Entities.User>
    {
        // custom operations here
        Task<Entities.User> GetSingleByPhoneAsync(string phone);
        Task<Entities.User> GetSingleByEmailAsync(string Email);
        Task<Entities.User> GetSingleByRefreshTokenAsync(string refresh);
        Task<Entities.User> UpdatePasswordAsync(Entities.User entity, string newPassword);
        Task<Entities.User> UpdateRefreshTokenAsync(Entities.User user);
    }   
}