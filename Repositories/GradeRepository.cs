using Microsoft.EntityFrameworkCore;
using StudentApplication.Models;
using StudentApplication.Context;

namespace StudentApplication.Repositories
{
    public interface IGradeRepository
    {
        Task<List<Grade>> GetGrades();
        Task<Grade> GetGrade(int GradeId);
        Task<Grade> CreateGrade (int StudentId, int SubjectId, string Description, float Score);
        Task<Grade> UpdateGrade (Grade grade);
        Task<Grade> DeleteGrade (int GradeId,bool ActiveGrade);   
    }

    public class GradeRepository : IGradeRepository
    {
        private readonly StudentDbContext _db;

        public GradeRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Grade> CreateGrade(int StudentId, int SubjectId, string Description, float Score)
        {
            Grade newGrade = new Grade
            {
                StudentId = StudentId,
                SubjectId = SubjectId,
                Description = Description,
                Score = Score,
                ActiveGrade = true
            };  
            await _db.Grades.AddAsync(newGrade);
            _db.SaveChanges();
            return newGrade;

            // throw new NotImplementedException();
        }
        public  async Task<List<Grade>> GetGrades()
        {
            return await _db.Grades.ToListAsync();
        }
        public async Task<Grade> GetGrade(int GradeId)
        {
            return await _db.Grades.FirstOrDefaultAsync(u => u.GradeId == GradeId);
        }
        public async Task<Grade> UpdateGrade(Grade grade)
        {
           _db.Grades.Update(grade);
            await _db.SaveChangesAsync();
            return grade;
        }
        public async Task<Grade> DeleteGrade(int GradeId, bool ActiveGrade)
        {
            var grade = await _db.Grades.FindAsync(GradeId);
            if (grade == null)
            {
                return null;
            }
            grade.ActiveGrade = ActiveGrade;
            await _db.SaveChangesAsync();
            return grade;
        }
    }
}