using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BrewMaster.Pages.Admin.Services.Cleanings
{
    public class EditServiceModel : PageModel
    {
        private readonly ICRUDRepository<Service> _serviceRepository;
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Service Service { get; set; }

        public EditServiceModel(ICRUDRepository<Service> serviceRepository, ICRUDRepository<Employee> employeeRepository)
        {
            _serviceRepository = serviceRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGetAsync(int serviceId)
        {
            Service = await _serviceRepository.GetByIdAsync(serviceId);

            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hent Employee baseret på UserId og sæt User-egenskaben
            var user = await _employeeRepository.GetByIdAsync(Service.UserId);

            if (user == null)
            {
                ModelState.AddModelError("Service.UserId", "Invalid UserId.");
                return Page();
            }

            Service.User = user;

            await _serviceRepository.UpdateAsync(Service);
            TempData["SuccessMessage"] = "Service updated successfully.";
            return RedirectToPage("/Admin/Services/Cleanings/ExistingService");
        }
    }
}




