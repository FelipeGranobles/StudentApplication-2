using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;

namespace StudentApplication.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetTeachers();
        Task<Teacher> GetTeacher(int TeacherId);
        Task<Teacher> CreateTeacher(string TeacherName, string TeacherLastName, string TeacherEmail, string TeacherPhone);
        Task<Teacher> UpdateTeacher(Teacher teacher);
        Task<Teacher> DeleteTeacher(int TeacherId, bool ActiveTeacher);
    }

    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudentDbContext _db;

        public TeacherRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Teacher> CreateTeacher(string TeacherName, string TeacherLastName, string TeacherEmail, string TeacherPhone)
        {
            Teacher NewTeacher = new Teacher
            {
                TeacherName = TeacherName,
                TeacherLastName = TeacherLastName,
                TeacherEmail = TeacherEmail,
                TeacherPhone = TeacherPhone,
                ActiveTeacher = true
            };

            await _db.Teachers.AddAsync(NewTeacher);
            _db.SaveChanges();
            return NewTeacher;

            // throw new NotImplementedException();
        }

        public async Task<Teacher> GetTeacher(int TeacherId)
        {
            return await _db.Teachers.FirstOrDefaultAsync(u => u.TeacherId == TeacherId);
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _db.Teachers.ToListAsync();
        }

        public async Task<Teacher> UpdateTeacher(Teacher teacher)
        {
            _db.Teachers.Update(teacher);
            await _db.SaveChangesAsync();
            return teacher;
        }
        public async Task<Teacher> DeleteTeacher(int TeacherId, bool ActiveTeacher)
        {
            var teacher = await _db.Teachers.FindAsync(TeacherId);
            if (teacher == null)
            {
                return null;
            }
            teacher.ActiveTeacher = ActiveTeacher;
            await _db.SaveChangesAsync();
            return teacher;
        }
    }
}