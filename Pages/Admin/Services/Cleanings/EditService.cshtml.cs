using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models.Pages.Admin.Services.Cleanings
{
    public class EditModel : PageModel
    {
        private readonly BrewMaster.Models.BrewMasterContext _context;

        public EditModel(BrewMaster.Models.BrewMasterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BrewMaster.Models.Service Service { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime date)
        {
            Service = await _context.Services
                .FirstOrDefaultAsync(s => s.Date == date);

            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }


        private bool ServiceExists(DateTime date)
        {
            return _context.Services.Any(e => e.Date == date);
        }
    }
}
