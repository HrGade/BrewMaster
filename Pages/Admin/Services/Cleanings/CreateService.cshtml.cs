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

        
        public IList<Employee> Employees { get; set; }

        // GET: Hent medarbejdere til dropdown-menu
        public async Task<IActionResult> OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync();
            return Page();
        }

       //Opret en ny opgave
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                // Hvis ikke valid, henter vi medarbejderne igen og viser formularen
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontrollerer om den valgte medarbejder (UserID) findes i Employee
            var employeeExists = await _context.Employees.AnyAsync(e => e.UserId == Service.UserId);
            if (!employeeExists)
            {
                // Hvis medarbejderen ikke findes, tilf�jes en fejlmeddelelse
                ModelState.AddModelError("Service.UserID", "Den valgte medarbejder findes ikke.");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Kontroll�r om datoen er sat korrekt
            if (Service.Date == DateTime.MinValue)
            {
                ModelState.AddModelError("Service.Date", "V�lg en gyldig dato.");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Try-catch for at h�ndtere databasefejlen med datoen
            try
            {
                // Tilf�j service til databasen
                _context.Services.Add(Service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hvis en fejl opst�r under gemningen, vis fejl hvis datoen er forkert
                ModelState.AddModelError(string.Empty, $"Der opstod en fejl under oprettelsen af servicen: {ex.Message}");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }

            // Omdiriger til ServiceDashboard efter success
            return RedirectToPage("/Admin/Service/Cleanings/ServiceDashboard");
        }
    }
}