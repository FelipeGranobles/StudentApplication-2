using Microsoft.EntityFrameworkCore;
using StudentApplication.Models;
using StudentApplication.Context;

namespace StudentApplication.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetClasses();
        Task<Class> GetClass(int ClassId);
        Task<Class> CreateClass(string ClassName);
        Task<Class> UpdateClass(Class Clases);
        Task<Class> DeleteClass(Class Clases);
    }

    public class ClassRepository : IClassRepository
    {
        private readonly StudentDbContext _db;

        public ClassRepository(StudentDbContext db)
        {
            _db = db;
        }
        public async Task<Class> CreateClass(string ClassName)
        {
            Class newClass = new Class
            {
                ClassName = ClassName
            };
            
            await _db.Classs.AddAsync(newClass);
            _db.SaveChanges();
            return newClass;
        }

        public Task<Class> DeleteClass(Class Clases)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Class>> GetClasses()
        {
            return await _db.Classs.ToListAsync();
        }

        public async Task<Class> GetClass(int ClassId)
        {
            return await _db.Classs.FirstOrDefaultAsync(u => u.ClassId == ClassId);
        }

        public async Task<Class> UpdateClass(Class Clases)
        {
            _db.Classs.Update(Clases);
            await _db.SaveChangesAsync();
            return Clases;  
        }
    }
}