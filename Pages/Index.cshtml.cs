using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using BrewMaster.Repositories;


namespace BrewMaster.Pages.Repository
{
    public class IndexModel : PageModel
    {
        private readonly ICRUDRepository<Machine> _machineRepository;

        // Liste over maskiner
        public List<Machine> Machines { get; set; }

        // Korrekt injektion af ICrudRepository<Machine> i konstruktøren
        public IndexModel(ICRUDRepository<Machine> machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public async Task OnGetAsync()
        {
            // Hent alle maskiner via repository
            Machines = (List<Machine>)await _machineRepository.GetAllAsync();
        }
    }
}
