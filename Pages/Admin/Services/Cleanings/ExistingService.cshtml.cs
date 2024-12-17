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
            _serviceRepository = serviceRepository; //Tildeler serviceRepository 
            Services = new List<Service>(); //Initialiser en ny liste med Services
        }

        
        public IList<Service> Services { get; set; } //Interface til at opbevare en liste af Service Objekter. 

        public async Task OnGetAsync()
        {//Hent service-objekter <Service> og sæt dem sammen med en service
            Services = (List<Service>)await _serviceRepository.GetAllAsync();
        }
    }
}




