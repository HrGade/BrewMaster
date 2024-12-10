using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly BrewMasterContext _context;

        // Property to hold the employee data
        [BindProperty]
        public Employee Employee { get; set; }

        // Constructor to initialize the database context
        public DeleteEmployeeModel(BrewMasterContext context)
        {
            _context = context;
        }

        // On GET: Fetch the employee by ID
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Attempt to find the employee by ID
            Employee = await _context.Employees.FindAsync(id);

            // If employee is not found, show a custom message instead of 404
            if (Employee == null)
            {
                TempData["ErrorMessage"] = "Medarbejderen blev ikke fundet.";  // Store the error message in TempData
                return RedirectToPage("/Admin/AdminCRUD/NoEmployeeFound");  // Redirect to the Admin page
            }

            return Page();  // Return the page with the employee data
        }

        // On POST: Handle employee deletion
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Find the employee to delete
            var employeeToDelete = await _context.Employees.FindAsync(id);

            // If the employee is not found, show a custom message instead of 404
            if (employeeToDelete == null)
            {
                TempData["ErrorMessage"] = "Medarbejderen blev ikke fundet.";  // Store the error message in TempData
                return RedirectToPage("/Admin/AdminCRUD/NoEmployeeFound");  // Redirect to the Admin page
            }

            // Remove the employee from the database
            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();

            // Redirect to the Admin page after deletion
            return RedirectToPage("/Admin/AdminCRUD/ExistingEmployee");
        }
    }
}
