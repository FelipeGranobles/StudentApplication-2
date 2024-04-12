namespace StudentApplication.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string? Description { get; set; }
        public float Score { get; set; }
        public Student? Student { get; set; } 
        public Subject? Subject { get; set; } 
    }
}




