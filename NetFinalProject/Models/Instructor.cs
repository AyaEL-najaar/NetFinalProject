using System.ComponentModel.DataAnnotations;

namespace NetFinalProject.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        // Department id foreign key:
        public int DeptId { get; set; }
        // navigation property:join with Department table:1 department has many instructors
        public Department? Department { get; set; }
        //  Course id foreign key:
        public int CrsId { get; set; }
        // navigation property: join with Course table: 1 instructor teaches 1 course
        public Course? Course { get; set; }
    }
}
