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
            // Hvis modellen er ugyldig, vis valideringsfejl
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tjek for dubletter (for eksempel, om der allerede findes en maskine med samme placering)
            var existingMachine = _context.Machines
                .FirstOrDefault(m => m.Location == Machine.Location);

            if (existingMachine != null)
            {
                ModelState.AddModelError("Machine.Location", "En maskine med denne placering findes allerede.");
                return Page();
            }

            try
            {
                // Tilføj maskinen til databasen
                _context.Machines.Add(Machine);
                await _context.SaveChangesAsync();

                // Omdiriger til oversigten over maskiner
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }
            catch (Exception ex)
            {
                // Log fejlen (kan også logges i en fil, men her er det konsollen)
                Console.WriteLine($"Der opstod en fejl under oprettelsen af maskinen: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Der opstod en uventet fejl. Prøv igen senere.");
                return Page();
            }
        }
    }
}