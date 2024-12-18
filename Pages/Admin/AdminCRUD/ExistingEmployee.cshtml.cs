using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;


namespace BrewMaster.Models.Pages.Admin.AdminCRUD //Skrevet af Adam
{
    public class ExistingEmployeesModel : PageModel
    {
        private readonly ICRUDRepository<Employee> _employeeRepository;

        // Liste over medarbejdere, der vises p� siden
        public List<Employee> Employees { get; set; }

        // Konstrukt�r, der injicerer ICrudRepository for Employee
        public ExistingEmployeesModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Hent medarbejdere, n�r siden indl�ses
        public async Task<IActionResult> OnGetAsync()
        {
            // Hent alle medarbejdere fra databasen
            Employees = (List<Employee>)await _employeeRepository.GetAllAsync();

            // Hvis der ikke er nogen medarbejdere, vis en besked
            if (Employees == null || Employees.Count == 0)
            {
                TempData["ErrorMessage"] = "Der blev ikke fundet nogen medarbejdere.";
            }

            return Page();
        }
    }
}
