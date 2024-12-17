using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BrewMaster.Pages.Admin.Services.Cleanings
{
    public class DeleteServiceModel : PageModel
    {
        private readonly ICRUDRepository<Service> _serviceRepository;
        private readonly ICRUDRepository<Employee> _employeeRepository;

        [BindProperty]
        public Service Service { get; set; }

        public Employee Employee { get; set; }

        public DeleteServiceModel(ICRUDRepository<Service> serviceRepository, ICRUDRepository<Employee> employeeRepository)
        {
            _serviceRepository = serviceRepository; //Tildeler serviceRepository 
            _employeeRepository = employeeRepository; //Tildeler employeeRepository
        }

        public async Task<IActionResult> OnGetAsync(int serviceId) //Get anmodning baseret på serviceId
        {
            Service = await _serviceRepository.GetByIdAsync(serviceId);

            if (Service == null)
            {
                return NotFound();
            }

            Employee = await _employeeRepository.GetByIdAsync(Service.UserId); //Henter medarbejder 

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId); //Hent fra serviceId

            if (service == null)
            {
                return NotFound();
            }
            //Slet fra databasen
            await _serviceRepository.DeleteAsync(serviceId);

            TempData["SuccessMessage"] = "Service deleted successfully.";
            return RedirectToPage("ServiceDashboard");
        }
    }
}

