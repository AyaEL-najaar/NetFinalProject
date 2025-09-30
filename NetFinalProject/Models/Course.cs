using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Degree { get; set; }
        public int MinimumDegree { get; set; }
        [Precision(10, 2)]
        public decimal Hours { get; set; }

        // Foreign key to Department
        public int DeptId { get; set; }

        // Navigation property : each course belongs to one department 
        public Department? Department { get; set; }

        // Navigation property : each course can have many instructors
        public ICollection<Instructor>? Instructors { get; set; }
    }
}
