using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Address { get; set; }
        public int Grade { get; set; }

        // Foreign key: Department id
        public int DeptId { get; set; }
        // navigation property:1 student in 1 department
        public Department? Department { get; set; }
        // navigation: student maybe in many courses
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
    }
}
