using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class ExistingMachineModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public ExistingMachineModel(BrewMasterContext context)
        {
            _context = context;
        }

        public List<Machine> Machines { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle maskiner fra databasen
            Machines = await _context.Machines.ToListAsync();
            return Page();
        }
    }
}