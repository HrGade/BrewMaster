using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD.Repository
{
    public class DeleteMachineModel : PageModel
    {
        private readonly ICRUDRepository<Machine> _machineRepository;

        [BindProperty]
        public Machine Machine { get; set; }

        // Constructor der injicerer ICrudRepository for Machine
        public DeleteMachineModel(ICRUDRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;
        }

        // Hent medarbejderen via ID og vis den p� siden
        public async Task<IActionResult> OnGetAsync(int id)

        {
            // Hent medarbejder ved hj�lp af repository
            Machine = await _machineRepository.GetByIdAsync(id);


            // Hvis kaffemaskinen ikke findes, returner 404 NotFound
            if (Machine == null)
            {
                TempData["ErrorMessage"] = "Ingen kaffemaskiner blev fundet.";
                return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
            }

            return Page();
        }

        // H�ndter sletning af kaffemaskinen
        public async Task<IActionResult> OnPostAsync(int id)
        {

            // Slet kaffemaskinen ved hj�lp af repository
            await _machineRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = "Kaffemaskinen blev slettet succesfuldt.";

            // Redirect til listen over kaffemaskiner efter sletning
            return RedirectToPage("/Admin/MachineCRUD/ExistingMachine");
        }
    }
}