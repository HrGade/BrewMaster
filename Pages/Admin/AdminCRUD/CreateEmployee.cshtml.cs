using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;



namespace BrewMaster.Models.Pages.Admin.AdminCRUD;
public class CreateEmployeeModel : PageModel
{
    private readonly BrewMasterContext _context;

    public CreateEmployeeModel(BrewMasterContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Tjek for duplikeret Password 

        Employee existingEmployeeName = await _context.Employees.FindAsync(Employee.UserId);
        if (existingEmployeeName != null)
        {
            ModelState.AddModelError("Employee.Name", "BrugerNavn findes allerede.");
            return Page();
        }

        Employee existingEmployeePassword = await _context.Employees.FindAsync(Employee.UserId);
        if (existingEmployeePassword != null)
        {
            ModelState.AddModelError("Employee.Password", "Password findes allerede.");
            return Page();
        }

        _context.Employees.Add(Employee);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
    }
}
