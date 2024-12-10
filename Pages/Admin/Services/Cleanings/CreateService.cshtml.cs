using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models.Pages.Admin.Services.Cleanings
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

        public IList<Employee> Employee { get; set; } // Listen over medarbejdere

        // GET: Hent medarbejdere til dropdown-menuen
        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle medarbejdere fra databasen
            Employee = await _context.Employees.ToListAsync();
            return Page();
        }

        // POST: Opret en ny service
        public async Task<IActionResult> OnPostAsync()
        {
            // Hvis ModelState ikke er valid, vis siden med fejl
            if (!ModelState.IsValid)
            {
                Employee = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontrollér, at UserID refererer til en gyldig medarbejder
            var employeeExists = await _context.Employees.AnyAsync(e => e.UserId == Service.UserId);
            if (!employeeExists)
            {
                ModelState.AddModelError("Service.UserID", "Den valgte medarbejder findes ikke.");
                Employee = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontrollér, at der ikke allerede findes en service med den samme dato
            var serviceExists = await _context.Services.AnyAsync(s => s.Date == Service.Date);
            if (serviceExists)
            {
                ModelState.AddModelError("Service.Date", "Der eksisterer allerede en service for denne dato.");
                Employee = await _context.Employees.ToListAsync();
                return Page();
            }

            try
            {
                // Tilføj og gem Service i databasen
                _context.Services.Add(Service);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "Der opstod en fejl under oprettelsen af servicen.");
                Console.WriteLine(ex.Message); // Log fejlen i konsollen
                Employee = await _context.Employees.ToListAsync();
                return Page();
            }

            // Omdiriger til dashboard efter succesfuld oprettelse
            return RedirectToPage("/Admin/Services/Cleanings/ServiceDashboard");
        }
    }
}
