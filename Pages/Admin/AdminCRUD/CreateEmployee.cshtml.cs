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

        //En Constructor der injicerer ICRUDRepository til Employee som <T>
        public CreateEmployeeModel(ICRUDRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        public void OnGet()
        {
            // OnGet viser kun formularen
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            // Validering af modellen
            if (!ModelState.IsValid)
            {
                return Page(); // Man bliver få siden, hvis der sker en fejl (f.eks. hvis ModelState er invalid). 
            }

            // Bruger repository til at tilføje medarbejderen
            await _employeeRepository.AddAsync(Employee);

            // TempData gør her, at når en Medarbejder bliver oprettet, bliver brugeren (Admin) underret om at det er success
            TempData["SuccessMessage"] = "Medarbejderen blev oprettet succesfuldt.";

            // Fører brugeren  hen til ExistingEmployee for at vise listen med den oprettet medarbejder
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}
