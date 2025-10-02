using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(100)]
        [Display(Name = "Student Name")]
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? Address { get; set; }
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        public int Grade { get; set; }

        // Foreign key: Department id
        [Display(Name = "Department")]
        [Required]
        public int DeptId { get; set; }
        // navigation property:1 student in 1 department
        public Department? Department { get; set; }
        // navigation: student maybe in many courses
       
        public ICollection<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
    }
}
