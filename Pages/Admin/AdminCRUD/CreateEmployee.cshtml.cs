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

        // Konstruktør, der injicerer ICrudRepository for Employee
        public CreateEmployeeModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Når siden indlæses (GET)
        public void OnGet()
        {
            // Vi behøver ikke gøre noget her, fordi vi kun viser formularen
        }

        // Håndter POST-anmodning for at oprette en ny medarbejder
        public async Task<IActionResult> OnPostAsync()
        {
            // Validering af modellen
            if (!ModelState.IsValid)
            {
                return Page(); // Hvis modellen er ugyldig, bliv på siden og vis fejl
            }

            // Tilføj medarbejderen via repository
            await _employeeRepository.AddAsync(Employee);

            // Brug TempData til at vise en succesbesked
            TempData["SuccessMessage"] = "Medarbejderen blev oprettet succesfuldt.";

            // Omdiriger til listen over medarbejdere
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}
