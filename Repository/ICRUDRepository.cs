using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewMaster.Repositories
{
    public interface ICRUDRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}

