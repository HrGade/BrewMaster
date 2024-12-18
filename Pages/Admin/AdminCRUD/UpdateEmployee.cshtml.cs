using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD //Skrevet af Adam
{
    public class UpdateEmployeeModel : PageModel
    {
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Employee Employee { get; set; }

        // Constructor der injicerer ICrudRepository for Employee
        public UpdateEmployeeModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Hent medarbejderens oplysninger via ID og vis dem i formularen
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Hent medarbejder ved hjælp af repository
            Employee = await _employeeRepository.GetByIdAsync(id);

            // Hvis medarbejderen ikke findes, omdiriger til listen og vis en fejlmeddelelse
            if (Employee == null)
            {
                TempData["ErrorMessage"] = "Medarbejderen blev ikke fundet.";
                return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
            }

            return Page();
        }

        // Håndter opdatering af medarbejderen
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page(); // Hvis ModelState valideres ukorrekt bliver man på siden.
            }

            // Opdater medarbejder i databasen
            await _employeeRepository.UpdateAsync(Employee);

            // Tilføj succesmeddelelse til Brugeren
            TempData["SuccessMessage"] = "Medarbejderen blev opdateret succesfuldt.";

            // Omdiriger til listen over medarbejdere efter opdatering
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}