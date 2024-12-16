using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;


namespace BrewMaster.Models.Pages.Admin.MachineCRUD
{
    public class ExistingMachinesModel : PageModel
    {
        private readonly ICRUDRepository<Machine> _machineRepository;

        // Liste over maskiner, der vises p� siden
        public IEnumerable<Machine> Machines { get; set; }

        // Konstrukt�r, der injicerer ICrudRepository for Machine
        public ExistingMachinesModel(ICRUDRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;
        }

        // Hent maskiner, n�r siden indl�ses (GET)
        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle maskiner fra databasen
            Machines = await _machineRepository.GetAllAsync();

            // Hvis der ikke er nogen maskiner, vis en fejlbesked
            if (Machines == null || !Machines.Any())
            {
                TempData["ErrorMessage"] = "Der blev ikke fundet nogen maskiner.";
            }

            return Page();
        }
    }
}