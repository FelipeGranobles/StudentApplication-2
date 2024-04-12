using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{

    public interface IStudentService
    {
        Task<List<Student>> getStudents();
        Task<Student> getStudent(int StudentId);
        Task<Student> createStudent(string StudentName, string StudentLastName, string Email, string Birthday);
        Task<Student> updateStudent(int StudentId, string? StudentName = null, string? StudentLastName = null, string? Email = null, string? Birthday = null);
        Task<Student> deleteStudent(int StudentId, bool Active);
    }

    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> createStudent(string StudentName, string StudentLastName, string Email, string Birthday)
        {
            return await _studentRepository.createStudent(StudentName, StudentLastName, Email, Birthday);
        }

        public async Task<Student> getStudent(int StudentId)
        {
            return await _studentRepository.getStudent(StudentId);
        }

        public async Task<List<Student>> getStudents()
        {
            return await _studentRepository.getStudents();
        }

        public async Task<Student> updateStudent(int StudentId, string? StudentName = null, string? StudentLastName = null, string? Email = null, string? Birthday = null)
        {
            Student student = await _studentRepository.getStudent(StudentId);

            if (student != null)
            {
                return null;
            }
            else
                if (StudentName != null)
            {
                student.StudentName = StudentName;
            }
            if (StudentLastName != null)
            {
                student.StudentLastName = StudentLastName;
            }
            if (Email != null)
            {
                student.Email = Email;
            }
            if (Birthday != null)
            {
                student.Birthdate = Birthday;
            }

            return await _studentRepository.updateStudent(student);
        }

        public async Task<Student> deleteStudent(int StudentId, bool Active)
        {
            return await _studentRepository.deleteStudent(StudentId,Active);
        }

    }
}   