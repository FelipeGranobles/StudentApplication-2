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
        Task<Grade> DeleteGrade (Grade grade);   
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
                Score = Score
            };
            await _db.Grades.AddAsync(newGrade);
            _db.SaveChanges();
            return newGrade;

            // throw new NotImplementedException();

        }

        public Task<Grade> DeleteGrade(Grade grade)
        {
            throw new NotImplementedException();
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
    }
}