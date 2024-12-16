using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD
{
    public class CreateEmployeeModel : PageModel
    {
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Employee Employee { get; set; }

        // Konstrukt�r, der injicerer ICrudRepository for Employee
        public CreateEmployeeModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // N�r siden indl�ses (GET)
        public void OnGet()
        {
            // Vi beh�ver ikke g�re noget her, fordi vi kun viser formularen
        }

        // H�ndter POST-anmodning for at oprette en ny medarbejder
        public async Task<IActionResult> OnPostAsync()
        {
            // Validering af modellen
            if (!ModelState.IsValid)
            {
                return Page(); // Hvis modellen er ugyldig, bliv p� siden og vis fejl
            }

            // Tilf�j medarbejderen via repository
            await _employeeRepository.AddAsync(Employee);

            // Brug TempData til at vise en succesbesked
            TempData["SuccessMessage"] = "Medarbejderen blev oprettet succesfuldt.";

            // Omdiriger til listen over medarbejdere
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}
