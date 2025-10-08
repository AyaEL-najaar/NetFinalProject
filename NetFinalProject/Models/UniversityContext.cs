using Microsoft.EntityFrameworkCore;

namespace NetFinalProject.Models

{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options){ }


        // Database Sets (tables)
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseStudents> CourseStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: Course <-> Students
            modelBuilder.Entity<CourseStudents>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            // Student -> Department
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DeptId)
                .OnDelete(DeleteBehavior.NoAction);

            // Course -> Department
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DeptId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DeptId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Instructor>()
     .HasMany(i => i.Courses)
     .WithOne(c => c.Instructor)
     .HasForeignKey(c => c.InstructorId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
    .Property(c => c.Hours)
    .HasPrecision(5, 2);

            modelBuilder.Entity<Instructor>()
                .Property(i => i.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Department>()
        .HasMany(d => d.Courses)
        .WithOne(c => c.Department)
        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
    .HasOne(s => s.Department)
    .WithMany(d => d.Students)
    .HasForeignKey(s => s.DeptId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
        .HasOne(c => c.Instructor)
        .WithMany(i => i.Courses)
        .HasForeignKey(c => c.InstructorId)
        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Instructor>().HasData(
        new Instructor { Id = 1, Name = "Dr. Ahmed" },
        new Instructor { Id = 2, Name = "Dr. Mona" }
    );
        }

    }
}
