using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BrewMaster.Pages.Admin.Services
{
    public class ServiceDashboardModel : PageModel
    {
        private readonly ICRUDRepository<Service> _serviceRepository;


        public ServiceDashboardModel(ICRUDRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
            Services = new List<Service>();
        }

        public IList<Service> Services { get; set; }

        public async Task OnGetAsync()
        {
            Services = (List<Service>)await _serviceRepository.GetAllAsync();
        }
    }
}




