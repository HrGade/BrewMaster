using System.Collections.Generic;
using System.Threading.Tasks;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Repositories
{
    public class MachineRepository : ICRUDRepository<Machine>
    {
        private readonly BrewMasterContext _context;

        public MachineRepository(BrewMasterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            return await _context.Machines.ToListAsync();
        }

        public async Task<Machine> GetByIdAsync(int id)
        {
            return await _context.Machines.FindAsync(id);
        }

        public async Task AddAsync(Machine entity)
        {
            await _context.Machines.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Machine entity)
        {
            _context.Machines.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine != null)
            {
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync();
            }
        }
    }
}

