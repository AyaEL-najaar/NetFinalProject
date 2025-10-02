using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        public int Degree { get; set; }
        public int MinimumDegree { get; set; }

        [Range(1, 10, ErrorMessage = "Credit hours must be between 1 and 10")]
        [Display(Name = "Credit Hours")]
        public decimal Hours { get; set; }

        // Foreign key to Department
        [Required(ErrorMessage = "Department is required.")]
        public int DeptId { get; set; }

        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        // Navigation property : each course belongs to one department 
        public Department? Department { get; set; }

        // Navigation property : each course can have many instructors
        public ICollection<Instructor>? Instructors { get; set; }
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();

    }
}
