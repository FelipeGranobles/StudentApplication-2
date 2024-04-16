namespace StudentApplication.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherLastName { get; set; }
        public string? TeacherEmail { get; set; }
        public string? TeacherPhone {  get; set; }
        public bool ActiveTeacher { get; set; }
        public List<Subject>? Subject { get; set; }

    }
}
