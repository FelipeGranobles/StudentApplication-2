namespace StudentApplication.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public string? SubjectName { get; set; }
        public string? Subjectcode { get; set; }
        public Teacher? Teacher { get; set; }
        public List<Class>? Class { get; set; }
        public List<Attendance>? Attendances { get; set; }
    }
}
