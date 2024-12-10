using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;


namespace BrewMaster.ModelsPages.Admin.Service.Cleanings
{
    public class CreateServiceModel : PageModel
    {
        private readonly BrewMaster.Models.BrewMasterContext _context;

        public CreateServiceModel(BrewMaster.Models.BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BrewMaster.Models.Service Service { get; set; }

        // Liste over alle medarbejdere til dropdown-menu
        public IList<Employee> Employees { get; set; }

        // GET: Hent medarbejdere til dropdown-menu
        public async Task<IActionResult> OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync();
            return Page();
        }

        // POST: Opret en ny service
        public async Task<IActionResult> OnPostAsync()
        {
            // Valider formularen
            if (!ModelState.IsValid)
            {
                // Hvis ikke valid, henter vi medarbejderne igen og viser formularen
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontrollér om den valgte medarbejder (UserID) findes i Employee
            var employeeExists = await _context.Employees.AnyAsync(e => e.UserId == Service.UserId);
            if (!employeeExists)
            {
                // Hvis medarbejderen ikke findes, tilføj fejlmeddelelse
                ModelState.AddModelError("Service.UserID", "Den valgte medarbejder findes ikke.");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontrollér om datoen er sat korrekt
            if (Service.Date == DateTime.MinValue)
            {
                ModelState.AddModelError("Service.Date", "Vælg en gyldig dato.");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Try-catch for at håndtere databasefejl
            try
            {
                // Tilføj service til databasen
                _context.Services.Add(Service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hvis en fejl opstår under gemningen, vis fejl
                ModelState.AddModelError(string.Empty, $"Der opstod en fejl under oprettelsen af servicen: {ex.Message}");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Omdiriger til ServiceDashboard efter succes
            return RedirectToPage("/Admin/Service/Cleanings/ServiceDashboard");
        }
    }
}