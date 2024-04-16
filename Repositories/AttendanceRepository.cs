using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;

namespace StudentApplication.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendances();
        Task<Attendance> GetAttendance(int AttendanceId);
        Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag);
        Task<Attendance> UpdateAttendance(Attendance attendance);
        Task<Attendance> DeleteAttendance(int AttendanceId, bool ActiveAttendance);
    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly StudentDbContext _db;

        public AttendanceRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag)
        {
            Attendance newAttendance = new Attendance
            {
                StudentId = StudentId,
                SubjectId = SubjectId,
                Date = Date,
                AttendanceFlag = AttendanceFlag,
                ActiveAttendance = true
            };

            await _db.Attendances.AddAsync(newAttendance);
            _db.SaveChanges();
            return newAttendance;
            
        }
        public async Task<List<Attendance>> GetAttendances()
        {
            return await _db.Attendances.ToListAsync();
        }
        public async Task<Attendance> GetAttendance(int AttendanceId)
        {
            return await  _db.Attendances.Where(u => u.AttendanceId == AttendanceId).FirstOrDefaultAsync();
        }
        public async Task<Attendance> UpdateAttendance(Attendance attendance)
        {
            _db.Attendances.Update(attendance);
            await _db.SaveChangesAsync();  
            return attendance;
        }

        public async Task<Attendance> DeleteAttendance(int AttendanceId, bool ActiveAttendance)
        {
            var attendance = await _db.Attendances.FindAsync(AttendanceId);
            if (attendance == null)
            {
                return null;
            } 
            attendance.ActiveAttendance = ActiveAttendance;
            await _db.SaveChangesAsync();
            return attendance;
        }
    }
}