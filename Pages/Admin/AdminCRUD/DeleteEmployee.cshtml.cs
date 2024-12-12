using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BrewMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models.Pages.Admin.AdminCRUD
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly BrewMasterContext _context;

        
        [BindProperty]
        public Employee Employee { get; set; }

        
        public DeleteEmployeeModel(BrewMasterContext context)
        {
            _context = context;
        }

   
  

       //Håndter fjernelse af Employee
        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            var employeeToDelete = await _context.Employees.FindAsync(id); // Finder medarbejderen som skal slettes

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
