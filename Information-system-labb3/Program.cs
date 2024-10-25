using Information_system_labb3.Data;
using Information_system_labb3.Models;
using Information_system_labb3.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Information_system_labb3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDriverRepo, DriverRepository>();
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepository>();
            builder.Services.AddScoped<INoteRepo, NoteRepository>();

            var app = builder.Build();

            // Initialize roles
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleInitializer.InitializeAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Middleware to redirect authenticated users to the home page
            app.Use(async (context, next) =>
            {
                if (context.User.Identity.IsAuthenticated && context.Request.Path == "/")
                {
                    context.Response.Redirect("/Home");
                }
                else
                {
                    await next();
                }
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
