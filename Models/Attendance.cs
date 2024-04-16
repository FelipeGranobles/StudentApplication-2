namespace StudentApplication.Models
{
    public class Attendance
    {
        public  int AttendanceId { get; set; }
        public  int StudentId { get; set; }
        public  int SubjectId { get; set; }
        public  DateOnly Date { get; set; }
        public  bool AttendanceFlag {  get; set; }
        public  bool ActiveAttendance { get; set; } = true;
        public  Student? Student { get; set; }
        public  Subject?  Subjects { get; set; }

    }
}

