using BrewMaster.Models;
using BrewMaster.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<BrewMasterContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registrer ServiceRepository som implementering af både ICRUDServiceRepository og ICRUDRepository<Service>
            builder.Services.AddScoped<ICRUDRepository<Machine>, MachineRepository>();

            // Registrer EmployeeRepository som implementering af ICRUDRepository<Employee>
            builder.Services.AddScoped<ICRUDRepository<Employee>, EmployeeRepository>();

            builder.Services.AddScoped<ICRUDRepository<Service>, ServiceRepository>();



            builder.Services.Configure<CookiePolicyOptions>(options => {
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

         

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

        
    }
    
}
