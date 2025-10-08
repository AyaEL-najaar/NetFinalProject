using Microsoft.EntityFrameworkCore;
using NetFinalProject.Models;
using NetFinalProject.Repository;
using NetFinalProject.Filters;
using NetFinalProject.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ======================
// 1️⃣ Add Services
// ======================

// DbContext
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Filters
builder.Services.AddScoped<AuthorizeStudentFilter>();

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // مدة الجلسة
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Controllers with Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ======================
// 2️⃣ Seed initial data (Departments)
// ======================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UniversityContext>();

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

// ======================
// 3️⃣ Configure Middleware / Pipeline
// ======================
app.UseSession(); // لازم قبل أي Authentication / Authorization
app.UseMiddleware<RequestLoggingMiddleware>();

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
