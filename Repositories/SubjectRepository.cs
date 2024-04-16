using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;

namespace StudentApplication.Repositories
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int SubjectId);
        Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode ,Teacher teacher);
        Task<Subject> UpdateSubject(Subject subject);
        Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject);
    }

    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentDbContext _db;
        public SubjectRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode ,Teacher Teacher)
        {
            Subject newSubject = new Subject
            {
                ClassId = ClassId,
                SubjectName = SubjectName,
                Subjectcode = Subjectcode,
                Teacher = Teacher,
                ActiveSubject = true
            };
            await _db.Subjects.AddAsync( newSubject );  
            _db.SaveChanges();
            return newSubject;

            // throw new NotImplementedException();
        }   

        public async Task<Subject> DeleteSubject(int SubjectId,bool ActiveSubject)
        {
            var subject = await _db.Subjects.FindAsync(SubjectId);
            if (subject == null)
            {
                return null;
            }  
            subject.ActiveSubject = ActiveSubject;
            await _db.SaveChangesAsync();
            return subject;
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _db.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubject(int SubjectId)
        {
            return await _db.Subjects.FirstOrDefaultAsync(u => u.SubjectId == SubjectId);
        }

        public async Task<Subject> UpdateSubject(Subject subject)
        {
            _db.Subjects.Update(subject);
            await _db.SaveChangesAsync();
            return subject;
        }
    }

}