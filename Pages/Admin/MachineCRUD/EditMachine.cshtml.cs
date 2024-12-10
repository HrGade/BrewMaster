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

        // GET: Hent maskindata baseret på ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Machine = await _context.Machines.FindAsync(id);
            if (Machine == null)
            {
                return NotFound();
            }
            return Page();
        }

        // POST: Opdater maskinen
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find den eksisterende maskine i databasen
            var machineToUpdate = await _context.Machines.FindAsync(Machine.MachineId);
            if (machineToUpdate == null)
            {
                return NotFound();
            }

            // Opdater kun de relevante felter
            machineToUpdate.Location = Machine.Location;
            machineToUpdate.LatestCleaning = Machine.LatestCleaning;
            machineToUpdate.LatestService = Machine.LatestService;
            machineToUpdate.LatestFillUp = Machine.LatestFillUp;

            // Gem ændringerne i databasen
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}