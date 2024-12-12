using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;

namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class CreateMachineModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public CreateMachineModel(BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Machine Machine { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Hvis den indtastet maskine ikke er gyldig, vises der en fejl. 
            if (!ModelState.IsValid)
            {
                return Page();
                
            }

            // Her tjekkes der for, om der allerede eksisterer en maskine med samme navn.
            var existingMachine = _context.Machines
                .FirstOrDefault(m => m.Location == Machine.Location);

            if (existingMachine != null)
            {
                ModelState.AddModelError("Machine.Location", "En maskine med denne placering findes allerede.");
                return Page();
            }

            try
            {
                // Her bliver maskinen skrevet ind til databasen.
                _context.Machines.Add(Machine);
                await _context.SaveChangesAsync();

                // Omdirigerer tilbage til frontsiden af medarbejdere.
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }
            catch (Exception ex)
            {
                // Console.WriteLine skriver her hvis der opstår en fejl. Det er ellers ikke lovligt med CW, men ellers skal det laves i en txt fil. 
                Console.WriteLine($"Der opstod en fejl under oprettelsen af maskinen: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Der opstod en uventet fejl. Prøv igen senere.");
                return Page();
            }
        }
    }
}