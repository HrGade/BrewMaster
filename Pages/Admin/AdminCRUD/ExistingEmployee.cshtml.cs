using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD
{
    public class ExistingEmployeeModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public ExistingEmployeeModel(BrewMasterContext context)
        {
            _context = context;
        }

        // Liste over medarbejdere
        public IList<Employee> Employees { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle medarbejdere fra databasen
            Employees = await _context.Employees.ToListAsync();
            return Page();
        }
    }
}
