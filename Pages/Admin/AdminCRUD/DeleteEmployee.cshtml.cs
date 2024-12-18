using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Repositories;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD.Repository //Skrevet af Adam
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Employee Employee { get; set; }

        // Constructor der injicerer ICrudRepository for Employee
        public DeleteEmployeeModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Hent medarbejderen via ID og vis den på siden
        public async Task<IActionResult> OnGetAsync(int id)

        {
            // Hent medarbejder ved hjælp af repository
            Employee = await _employeeRepository.GetByIdAsync(id);


            // Hvis medarbejderen ikke findes, returner 404 NotFound
            if (Employee == null)
            {
                TempData["ErrorMessage"] = "Medarbejderen blev ikke fundet.";
                return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
            }

            return Page();
        }

        // Håndter sletning af medarbejderen
        public async Task<IActionResult> OnPostAsync(int id)
        {

            // Slet medarbejderen ved hjælp af repository
            await _employeeRepository.DeleteAsync(id);


            
            TempData["SuccessMessage"] = "Medarbejderen blev slettet succesfuldt.";

            // Redirect til listen over medarbejdere efter sletning
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}