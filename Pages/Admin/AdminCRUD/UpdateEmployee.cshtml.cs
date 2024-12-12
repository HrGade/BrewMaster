using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;


namespace BrewMaster.Models.Pages.Admin.AdminCRUD;

public class UpdateEmployeeModel : PageModel
{
    private readonly BrewMasterContext _context;

   
    [BindProperty]
    public Employee Employee { get; set; }

    public UpdateEmployeeModel(BrewMasterContext context)
    {
        _context = context;
    }

 
    public async Task<IActionResult> OnGetAsync(int id)
    {
        // Hent Medarbejderen via id
        Employee = await _context.Employees.FindAsync(id);

        // Hvis medarbejderen ikke kan findes, så returner "ikke fundet"
        if (Employee == null)
        {
            return NotFound();
        }

        
        return Page();
    }

    // OnPostAsync håndterer her opdateringen af medarbejderen
    public async Task<IActionResult> OnPostAsync()
    {
       
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Hent Medarbejderen via id
        var employeeToUpdate = await _context.Employees.FindAsync(Employee.UserId);

        // Hvis medarbejderen ikke kan findes, så returner "ikke fundet"
        if (employeeToUpdate == null)
        {
            return NotFound(); 
        }

        // Opdater medarbejderen med ændret egenskaber
        employeeToUpdate.Name = Employee.Name;
        employeeToUpdate.Password = Employee.Password;
        employeeToUpdate.UserType = Employee.UserType;

        // Gem til databasen
        await _context.SaveChangesAsync();

        // retuner til ExistingEmployee hvis det lykkedes
        return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
    }
}
