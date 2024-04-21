using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentApplication.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int StudentId { get; set; }
        public  string StudentName { get; set; }
        public  string StudentLastName { get; set; }
        public string Email { get; set; }
        public  string Birthdate { get; set; }
        public bool Active { get; set; } = true;
      //  public  List<Grade> Grades { get; set; }
       // public  List<Attendance> Attendances { get; set; }
    }
}
