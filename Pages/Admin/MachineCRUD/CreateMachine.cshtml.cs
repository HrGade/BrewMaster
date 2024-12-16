using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BrewMaster.Pages.Admin.MachineCRUD
{
    public class CreateMachineModel : PageModel
    {
        private readonly ICRUDRepository<Machine> _machineRepository;

        [BindProperty]
        public Machine Machine { get; set; }

        public CreateMachineModel(ICRUDRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _machineRepository.AddAsync(Machine);

            TempData["SuccessMessage"] = "Machine created successfully.";
            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine"); // Omdirigér til dashboardet eller maskineoversigten
        }
    }
}


