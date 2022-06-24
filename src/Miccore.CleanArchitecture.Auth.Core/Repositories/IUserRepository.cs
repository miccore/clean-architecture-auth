using Miccore.CleanArchitecture.Auth.Core.Repositories.Base;

namespace Miccore.CleanArchitecture.Auth.Core.Repositories
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository : IRepository<Miccore.CleanArchitecture.Auth.Core.Entities.User>
    {
        // custom operations here
        Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> GetSingleByPhoneAsync(string phone);
        Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> GetSingleByEmailAsync(string Email);
        Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> GetSingleByRefreshTokenAsync(string refresh);
        Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> UpdatePasswordAsync(int id, string newPassword);
        Task<Miccore.CleanArchitecture.Auth.Core.Entities.User> UpdateRefreshTokenAsync(Miccore.CleanArchitecture.Auth.Core.Entities.User user);
    }   
}