using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;


namespace BrewMaster.Models.Pages.Admin.AdminCRUD;

public class UpdateEmployeeModel : PageModel
{
    private readonly BrewMasterContext _context;

    // Bind Employee property to get and update employee data
    [BindProperty]
    public Employee Employee { get; set; }

    public UpdateEmployeeModel(BrewMasterContext context)
    {
        _context = context;
    }

    // On GET: Fetch the employee by ID for editing
    public async Task<IActionResult> OnGetAsync(int id)
    {
        // Fetch employee by ID from the database
        Employee = await _context.Employees.FindAsync(id);

        // If employee is not found, return NotFound
        if (Employee == null)
        {
            return NotFound();
        }

        // Return the page with the employee data
        return Page();
    }

    // On POST: Handle employee update
    public async Task<IActionResult> OnPostAsync()
    {
        // If model state is invalid, return the page
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Find the employee by ID in the database
        var employeeToUpdate = await _context.Employees.FindAsync(Employee.UserId);

        // If employee doesn't exist, return NotFound
        if (employeeToUpdate == null)
        {
            return NotFound();
        }

        // Update the employee properties with the new data
        employeeToUpdate.Name = Employee.Name;
        employeeToUpdate.Password = Employee.Password;
        employeeToUpdate.UserType = Employee.UserType;

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Redirect to the Admin page after successful update
        return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
    }
}
