using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;


namespace BrewMaster.Models.Pages.Admin.MachineCRUD.Repository
{
    public class EditMachineModel : PageModel
    {
        private readonly ICRUDRepository<Machine> _machineRepository;

        [BindProperty]
        public Machine Machine { get; set; }

        // Konstruktør, der injicerer ICrudRepository for Machine
        public EditMachineModel(ICRUDRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;
        }

        // Hent maskinens data, når siden indlæses (GET)
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Hent maskinen fra databasen via id
            Machine = await _machineRepository.GetByIdAsync(id);

            // Hvis maskinen ikke findes, returner 404
            if (Machine == null)
            {
                TempData["ErrorMessage"] = "Maskinen blev ikke fundet.";
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }

            return Page();
        }

        // Håndter POST-anmodningen for at opdatere maskinen
        public async Task<IActionResult> OnPostAsync()
        {
            // Valider dataen fra formularen
            if (!ModelState.IsValid)
            {
                return Page(); // Hvis modellen er ugyldig, vis siden igen med fejlmeddelelser
            }

            // Opdater maskinen i databasen
            await _machineRepository.UpdateAsync(Machine);

            // Brug TempData til at vise en succesbesked
            TempData["SuccessMessage"] = "Maskinen blev opdateret succesfuldt.";

            // Omdiriger til listen over maskiner
            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}