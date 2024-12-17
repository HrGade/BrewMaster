using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewMaster.Repositories
{
    
    public interface ICRUDRepository<T>
    {
        
        Task<IEnumerable<T>> GetAllAsync(); // Asynkron metode som henter objekter af entiteten T

        
        Task<T> GetByIdAsync(int id); // Asynkron metode som henter objekt af entiteten T udfra ID

       
        Task AddAsync(T entity);  // Asynkron metode som tilføjer nyt objekt af entiteten T

        
        Task UpdateAsync(T entity); // Asynkron metode som opdaterer eksisterende objekt af entiteten T

       
        Task DeleteAsync(int id);  // Asynkron metode som sletter objekt udfra entiteten T udfra ID
    }

}

