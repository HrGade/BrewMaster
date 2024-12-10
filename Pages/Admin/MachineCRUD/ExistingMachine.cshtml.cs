using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class ExistingMachineModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public ExistingMachineModel(BrewMasterContext context)
        {
            _context = context;
        }

        // Liste over maskiner
        public IList<Machine> Machines { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle maskiner fra databasen
            Machines = await _context.Machines.ToListAsync();
            return Page();
        }
    }
}