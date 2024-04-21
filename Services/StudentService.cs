using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{
    public interface IStudentService
    {
        Task<List<Student>> getStudents();
        Task<Student> getStudent(int studentId);
        Task<Student> createStudent(string studentName, string studentLastName, string email, string birthday);
        Task<Student> updateStudent(int studentId, string? studentName = null, string? studentLastName = null, string? email = null, string? birthday = null);
        Task<Student> deleteStudent(int studentId, bool active = false);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> createStudent(string studentName, string studentLastName, string email, string birthday)
        {
            return await _studentRepository.createStudent(studentName, studentLastName, email, birthday);
        }

        public async Task<Student> getStudent(int studentId)
        {
            return await _studentRepository.getStudent(studentId);
        }

        public async Task<List<Student>> getStudents()
        {
            return await _studentRepository.getStudents();
        }

        public async Task<Student> updateStudent(int studentId, string? studentName = null, string? studentLastName = null, string? email = null, string? birthday = null)
        {
            if (studentId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(studentId), "El ID del estudiante debe ser un número positivo.");
            }

            if (student == null)
            {
                return null;
            }

            if (studentName != null)
            {
                student.StudentName = studentName;
            }

            {
                student.StudentLastName = studentLastName;
            }

            {
                student.Email = email;
            }

            {
                if (DateTime.TryParse(birthday, out DateTime birthdate))
            }

            return await _studentRepository.updateStudent(student);
        }

        public async Task<Student> deleteStudent(int studentId, bool active = false)
        {
            Student student = await _studentRepository.getStudent(studentId);
            if (student == null)
            {
                return null; 
            }
            if (active)
            {
                student.Active = active;
            }
            await _studentRepository.deleteStudent(studentId, active);
            return student;
        }
    }
}