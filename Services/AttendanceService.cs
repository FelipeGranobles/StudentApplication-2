using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAttendances();
        Task<Attendance> GetAttendance(int AttendanceId);
        Task<Attendance> createAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag);
        Task<Attendance> updateAttendance(int AttendanceId, int StudentId, int SubjectId, DateOnly Date , bool AttendanceFlag);
        Task<Attendance> deleteAttendance(int AttendanceId, bool ActiveAttendance);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        public async Task<List<Attendance>> GetAttendances()
        {
            return await _attendanceRepository.GetAttendances();
        }
        public async Task<Attendance> GetAttendance(int AttendanceId)
        {
            return await _attendanceRepository.GetAttendance(AttendanceId);
        }
        public async Task<Attendance> createAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag)
        {
            return await _attendanceRepository.CreateAttendance(StudentId, SubjectId, Date, AttendanceFlag);
        }
        public async Task<Attendance> updateAttendance(int AttendanceId, int StudentId, int SubjectId, DateOnly Date , bool AttendanceFlag)
        {
            Attendance attendance = await _attendanceRepository.GetAttendance(AttendanceId);

            if(AttendanceId<=0)
            {
                throw new ArgumentException("Attendance ID debe ser numero positivo.");
            }
            if (attendance == null)
            {
                return null;
            }
            else if (StudentId != null)
            {
                attendance.StudentId = StudentId;
            }
            if (SubjectId != null)
            {
                attendance.SubjectId = SubjectId;
            }
            if (!AttendanceFlag)
            {
                attendance.AttendanceFlag = AttendanceFlag;
            }

            return await _attendanceRepository.UpdateAttendance(attendance);
        }

        public async Task<Attendance> deleteAttendance(int AttendanceId, bool ActiveAttendance)
        {
            Attendance attendance = await _attendanceRepository.GetAttendance(AttendanceId);
            if(attendance == null)
            {
                return null;
            }
            if(ActiveAttendance)
            {
                attendance.ActiveAttendance = ActiveAttendance;
            }
            await _attendanceRepository.DeleteAttendance(AttendanceId, ActiveAttendance);
            return attendance;
        }
    }
}