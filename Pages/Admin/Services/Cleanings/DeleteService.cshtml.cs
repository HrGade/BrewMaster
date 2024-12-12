using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Pages.Admin.Services.Cleanings
{
    public class DeleteServiceModel : PageModel
    {
        private readonly BrewMasterContext _context;

        public DeleteServiceModel(BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public Employee Employee { get; set; }

        // Hent servicen og tilhørende medarbejder baseret på ServiceId
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Find servicen baseret på ID
            Service = await _context.Services.FindAsync(id);

            if (Service == null)
            {
                return RedirectToPage("/Admin/Service/Cleanings/NoServiceFound");
            }

            // Hent navnet på medarbejderen, der er knyttet til servicen
            Employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == Service.UserId);
            return Page();
        }

        // Håndter sletning af servicen
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Find servicen baseret på id
            Service = await _context.Services.FindAsync(id);

            if (Service != null)
            {
                // Fjern servicen fra databasen
                _context.Services.Remove(Service);
                await _context.SaveChangesAsync();
            }

            // Omdiriger til listen over eksisterende services
            return RedirectToPage("/Admin/Services/Cleanings/ServiceDashboard");
        }
    }
}
