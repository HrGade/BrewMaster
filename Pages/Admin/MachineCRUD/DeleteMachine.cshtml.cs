using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;


namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class DeleteMachineModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public DeleteMachineModel(BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; }

        // Hent maskinen baseret på ID (GET-request)
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find maskinen med det givne ID
            Machine = await _context.Machines.FindAsync(id);

            // Hvis maskinen ikke findes, omdiriger til NoMachineFound-siden
            if (Machine == null)
            {
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }

            return Page();
        }

        // Håndter sletning af maskinen (POST-request)
        public async Task<IActionResult> OnPostAsync()
        {
            if (Machine != null)
            {
                // Find maskinen i databasen, hvis den stadig eksisterer
                var machineToDelete = await _context.Machines.FindAsync(Machine.MachineId);

                if (machineToDelete != null)
                {
                    // Fjern maskinen fra databasen
                    _context.Machines.Remove(machineToDelete);
                    await _context.SaveChangesAsync();
                }
            }

            // Omdiriger til Admin-siden
            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}