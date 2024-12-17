using System.Collections.Generic;
using System.Threading.Tasks;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Repositories
{
    public class MachineRepository : ICRUDRepository<Machine>
    {
        private readonly BrewMasterContext _context;

        public MachineRepository(BrewMasterContext context)  // constructor som initialisere MachineRepository
        {
            _context = context;
        }

        public async Task<IEnumerable<Machine>> GetAllAsync() // Asynkron metode som henter Machine objekter fra databasen
        {
            return await _context.Machines.ToListAsync(); // Returnerer Machine objekter i en liste
        }

        public async Task<Machine> GetByIdAsync(int id)  // Asynkron metode som henter Machine udfra ID
        {
            return await _context.Machines.FindAsync(id); // Finder og returnerer Machine objektet med det specifikke ID
        }

        public async Task AddAsync(Machine entity) // Asynkron metode som tilføjer Machine til databasen
        {
            await _context.Machines.AddAsync(entity);  // Tilfører Machine objekt til databasekonteksten
            await _context.SaveChangesAsync();  // Gemmer databasen
        }

        public async Task UpdateAsync(Machine entity) // Asynkron metode som opdaterer eksisterende Machine objekt i databasen
        {
            _context.Machines.Update(entity);  // Opdaterer Machine objektet i database konteksten
            await _context.SaveChangesAsync(); // Gemmer ændringerne i databasen
        } 

        public async Task DeleteAsync(int id)  // Asynkron metode som sletter Machine fra databasen udfra ID
        {
            var machine = await _context.Machines.FindAsync(id); // Finder Machine objektet udfra  specifikt ID
            if (machine != null) 
            {
                _context.Machines.Remove(machine); // sletter Machine objektet fra databasekonteksten
                await _context.SaveChangesAsync(); // Gemmer databasen
            }
        }
    }
}

