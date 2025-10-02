using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Instructor Name")]
        public string Name { get; set; } = string.Empty;
        [Range(7500,50000,ErrorMessage ="the min salary is 7500 EL and the max 50000 EL")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        // Department id foreign key:
        [Required]
        [Display(Name = "Department")]

        public int DeptId { get; set; }
        // navigation property:join with Department table:1 department has many instructors
        public Department? Department { get; set; }
        //  Course id foreign key:
        public int CrsId { get; set; }
        // navigation property: join with Course table: 1 instructor teaches many course
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
