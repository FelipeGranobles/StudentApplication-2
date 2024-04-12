using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;

namespace StudentApplication.Repositories
{
    public interface IStudentRepository
    {

        Task<List<Student>> getStudents();
        Task<Student> getStudent(int StudentId);
        Task<Student> createStudent(string StudentName, string StudentLastName, string Email, string Birthday); //duda para llenar el campo de Active 
        Task<Student> updateStudent(Student student);
        Task<Student> deleteStudent(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _db; 

        public StudentRepository(StudentDbContext db)
        {
            _db = db;
        }
        public async Task<Student> createStudent(string StudentName, string StudentLastName, string Email, string Birthday)
        {

            Student newStudent = new Student
            {
                StudentName = StudentName,
                StudentLastName = StudentLastName,
                Email = Email,
                Birthdate = Birthday
            };

            await _db.Students.AddAsync(newStudent);
            _db.SaveChanges();
            return newStudent;

        }

        public async Task<List<Student>> getStudents()
        {
            return await _db.Students.ToListAsync();
        }

        public async Task<Student> getStudent(int StudentId) => await _db.Students.Where(u => u.StudentId == StudentId).FirstOrDefaultAsync();

        public async Task<Student> updateStudent(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public Task<Student> deleteStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}