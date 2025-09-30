using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;

namespace NetFinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<UniversityContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UniversityContext>();

                // ?? ???? Departments ?????
                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department { Name = "Computer Science", ManagerName = "Dr. Ahmed" },
                        new Department { Name = "Information Technology", ManagerName = "Dr. Mona" },
                        new Department { Name = "Information System", ManagerName = "Dr. Karim" },
                        new Department { Name = "Software Engineering", ManagerName = "Dr. Sara" },
                        new Department { Name = "Cyber Security", ManagerName = "Dr. Youssef" }

                    );
                    context.SaveChanges();
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
