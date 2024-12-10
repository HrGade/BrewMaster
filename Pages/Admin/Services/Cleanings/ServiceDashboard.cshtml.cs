using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;


namespace BrewMaster.Models.Pages.Admin.Services.Cleanings
{
    public class ExistingServiceModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public ExistingServiceModel(BrewMasterContext context)
        {
            _context = context;
        }

        // Liste over services og medarbejdere
        public IList<Service> Services { get; set; }
        public IList<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle services og medarbejdere fra databasen
            Services = await _context.Services.ToListAsync();
            Employees = await _context.Employees.ToListAsync();
            return Page();
        }
    }
}
