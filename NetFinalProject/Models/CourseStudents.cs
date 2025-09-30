namespace NetFinalProject.Models
{
    public class CourseStudents
    {
       
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        // Navigation properties : 1 to many
        public Course? Course { get; set; }= null;
        public Student? Student { get; set; }
    }
}
