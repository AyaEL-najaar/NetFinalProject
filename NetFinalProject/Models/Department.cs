using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Department
    {
        [Key] // primary key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ManagerName { get; set; }

        // Navigation properties : 1 Department has many Students, Courses, Instructors
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
