using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using System.Threading.Tasks;

namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class EditMachineModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public EditMachineModel(BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Hent maskinen fra databasen
            var machine = await _context.Machines.FindAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            // Opdater maskinen
            machine.Location = "Ny placering";

                        // Brug Update() i stedet for Attach()
            _context.Machines.Update(machine);

            // Gem ændringerne i databasen
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }

    }
}
