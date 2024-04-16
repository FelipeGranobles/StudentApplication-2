using StudentApplication.Models;
using StudentApplication.Repositories;


namespace StudentApplication.Services
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetTeachers();
        Task<Teacher> GetTeacher(int TeacherId);
        Task<Teacher> CreateTeacher(string TeacherName, string TeacherLastName, string TeacherEmail, string TeacherPhone);
        Task<Teacher> UpdateTeacher(int TeacherId,string? TeacherName = null, string? TeacherLastName = null,string? TeacherEmail = null,string? TeacherPhone= null);
        Task<Teacher> DeleteTeacher(int TeacherId, bool ActiveTeacher);
    }

    public class TeacherService : ITeacherService
    {

        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Teacher> CreateTeacher(string TeacherName, string TeacherLastName, string TeacherEmail, string TeacherPhone)
        {
            return await _teacherRepository.CreateTeacher(TeacherName,TeacherLastName,TeacherEmail,TeacherPhone);
        }
        public async Task<Teacher> GetTeacher(int TeacherId)
        {
            return await _teacherRepository.GetTeacher(TeacherId);
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _teacherRepository.GetTeachers();
        }

        public async Task<Teacher> UpdateTeacher(int TeacherId,string? TeacherName = null, string? TeacherLastName = null,string? TeacherEmail = null,string? TeacherPhone= null)
        {
            Teacher teacher = await _teacherRepository.GetTeacher(TeacherId);

            if (teacher == null)
            {
                return null;
            }
            else
                if (TeacherName != null)
            {
                teacher.TeacherName = TeacherName;
            }
            if (TeacherLastName != null)
            {
                teacher.TeacherLastName = TeacherLastName;
            }
            if (TeacherEmail != null)
            {
                teacher.TeacherEmail = TeacherEmail;
            }
            if (TeacherPhone != null)
            {
                teacher.TeacherPhone = TeacherPhone;
            }

            return await _teacherRepository.UpdateTeacher(teacher);
        }

        public async Task<Teacher> DeleteTeacher(int TeacherId, bool ActiveTeacher)
        {
            Teacher teacher = await _teacherRepository.GetTeacher(TeacherId);
            if (teacher == null)
            {
                return null; 
            }
            if (ActiveTeacher)
            {
                teacher.ActiveTeacher = ActiveTeacher; 
            }
            await _teacherRepository.DeleteTeacher(TeacherId,ActiveTeacher);
            return teacher;
        }

    }
}