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

        // Maskinen hented her på et ID ( int id) fra databasen
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find maskinen med det givne ID
            Machine = await _context.Machines.FindAsync(id);

            // Hvis maskinen ikke findes, omdirigeres der til NoMachineFound-siden
            if (Machine == null)
            {
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }

            return Page();
        }

        // Her er OnPostAsync() den metode der håndterer sletningen af maskinen
        public async Task<IActionResult> OnPostAsync()
        {
            if (Machine != null)
            {
                // Tjekker om maskinen eksisterer og henter den 
                var machineToDelete = await _context.Machines.FindAsync(Machine.MachineId);

                if (machineToDelete != null)
                {
                    // Fjern maskinen fra databasen
                    _context.Machines.Remove(machineToDelete);
                    await _context.SaveChangesAsync();
                }
            }

            // Gå til Admin-siden
            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}