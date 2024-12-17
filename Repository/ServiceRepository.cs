using System.Collections.Generic;
using System.Threading.Tasks;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Repositories
{
    public class ServiceRepository : ICRUDRepository<Service> // Implementerer ICRUDRepository<Service> interface
    {
        private readonly BrewMasterContext _context; 

        public ServiceRepository(BrewMasterContext context) // Constructor der initialisere ServiceRepository 
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()  // Asynkron metode som henter alle Service fra databasen
        {
            return await _context.Services.ToListAsync(); // Returnerer alle Service¨'i en liste
        }

        public async Task<Service> GetByIdAsync(int id) // Asynkron metode som henter en enkelt Service udfra ID
        {
            return await _context.Services.FindAsync(id); // Finder og returnerer Service objektet udfra specifikt ID
        }

        public async Task AddAsync(Service entity) // Asynkron metode som tilføjer Service til databasen
        {
            await _context.Services.AddAsync(entity); // Tilfører Service til databasekonteksten
            await _context.SaveChangesAsync(); // Gemmer databasen
        }

        public async Task UpdateAsync(Service entity) // Asynkron metode som opdaterer Service i databasen
        {
            _context.Services.Update(entity); // Opdaterer Service i  databasekonteksten
            await _context.SaveChangesAsync(); // Gemmer databasen
        }

        public async Task DeleteAsync(int id) // Asynkron metode som sletter Service fra databasen udfra ID
        {
            var service = await _context.Services.FindAsync(id); // Finder Service objektet udfra specifikke ID
            if (service != null) 
            {
                _context.Services.Remove(service); // Sletter Service fra databasekonteksten
                await _context.SaveChangesAsync(); // Gemmer databasen
            }
        }
    }
}













