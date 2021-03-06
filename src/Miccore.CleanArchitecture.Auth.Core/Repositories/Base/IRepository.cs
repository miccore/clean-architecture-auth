using Miccore.CleanArchitecture.Auth.Core.Entities;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Core.Repositories.Base
{
    /// <summary>
    /// core repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity    
    {
        Task<PaginationModel<T>> GetAllAsync(PaginationQuery query);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}