using System.Collections.Generic;
using System.Threading.Tasks;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Repositories
{
    public class EmployeeRepository : ICRUDRepository<Employee>
    {
        private readonly BrewMasterContext _context; // lokal variabel som sættes sammen med databasekonteksten

        public EmployeeRepository(BrewMasterContext context) // Konstruktør som initialiserer EmployeeRepository 
        {
            _context = context;
        }


        public async Task<IEnumerable<Employee>> GetAllAsync() // Asynkron metode som henter Employee objekt fra databasen
        {
            return await _context.Employees.ToListAsync(); // Returnerer Employee objekte i form af en liste
        }

        public async Task<Employee> GetByIdAsync(int id) // Asynkron metode som henter Employee udfra ID
        {
            return await _context.Employees.FindAsync(id);  // Returnerer Employee objekt sammen med det specifikke ID
        }

        public async Task AddAsync(Employee entity) // Asynkron metode som tilføjer Employee til databasen
        {
            await _context.Employees.AddAsync(entity); // Tilføjer Employee objekt i databasekonteksten
            await _context.SaveChangesAsync(); // Gemmer databasen
        }

        public async Task UpdateAsync(Employee entity) // Asynkron metode som opdaterer eksisterende Employee i databasen
        {
            _context.Employees.Update(entity); // Opdaterer Employee objekt til databasekonteksten
            await _context.SaveChangesAsync(); // Gemmer databasen
        }

        public async Task DeleteAsync(int id) // Asynkron metode som sletter Employee fra databasen udfra ID
        {
            var employee = await _context.Employees.FindAsync(id); // Finder Employee udfra det specifikke ID
            if (employee != null) 
            {
                _context.Employees.Remove(employee); // Sletter Employee fra databasekonteksten
                await _context.SaveChangesAsync(); // Gemmer databasen
            }
        }
    }
}

