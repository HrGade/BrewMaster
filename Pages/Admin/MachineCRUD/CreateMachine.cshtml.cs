using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using System.Threading.Tasks;

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

            // Tilføj maskinen til databasen
            _context.Machines.Add(Machine);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}