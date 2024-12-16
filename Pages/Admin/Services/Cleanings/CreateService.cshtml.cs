using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BrewMaster.Pages.Admin.Services
{
    public class CreateServiceModel : PageModel
    {
        private readonly ICRUDRepository<Service> _serviceRepository;
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Service Service { get; set; }

        public CreateServiceModel(ICRUDRepository<Service> serviceRepository, ICRUDRepository<Employee> employeeRepository)
        {
            _serviceRepository = serviceRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            // Hent Employee baseret på UserId og sæt User-egenskaben
            var user = await _employeeRepository.GetByIdAsync(Service.UserId);

            if (user == null)
            {
                ModelState.AddModelError("Service.UserId", "Invalid UserId.");
                return Page();
            }

            Service.User = user;

            await _serviceRepository.AddAsync(Service);
            TempData["SuccessMessage"] = "Service created successfully.";
            return RedirectToPage("ServiceDashboard");
        }
    }
}









