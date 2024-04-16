using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{
    public interface IClassService
    {
        Task<List<Class>> GetClasses();
        Task<Class> GetClass(int Classid);
        Task<Class> createClass(string? ClassName = null);
        Task<Class> updateClass(int Classid, string? ClassName = null);
        Task<Class> deleteClass(int Classid, bool ActiveClass);
    }

    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<List<Class>> GetClasses()
        {
            return await _classRepository.GetClasses();
        }
        public async Task<Class> GetClass(int Classid)
        {
            return await _classRepository.GetClass(Classid);
        }
        public async Task<Class> createClass(string? ClassName = null)
        {
            return await _classRepository.CreateClass(ClassName);
        }
        public async Task<Class> updateClass(int Classid,string? ClassName = null)
        {
            Class clase = await _classRepository.GetClass(Classid);
            if(Classid <= 0 )
            {
                throw new ArgumentException("Class ID debe ser numero positivo.");
            }
            if (clase == null)
            {
                return null;
            }
            if(ClassName != ClassName)
            {
                clase.ClassName = ClassName;
            }
            return await _classRepository.UpdateClass(clase);
        }
        public async Task<Class> deleteClass(int ClassId,bool ActiveClass)
        {
            Class clase = await _classRepository.GetClass(ClassId);
            if(clase == null)
            {
                return null;
            }
            if (ActiveClass)
            {
                clase.ActiveClass = ActiveClass;
            }
            await _classRepository.DeleteClass(ClassId,ActiveClass);
            return clase;
        }
    }
}