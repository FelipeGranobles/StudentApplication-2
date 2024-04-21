using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;


namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Attendance>>> GetAttendances()
        {
            return Ok(await _attendanceService.GetAttendances());
        }
        [HttpGet("{AttendanceId}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int AttendanceId)
        {
            var attendance = await _attendanceService.GetAttendance(AttendanceId);
            if (attendance == null)
            {
                return NotFound("Asistencia no encontrada");
            }
            return Ok(attendance);
        }
        [HttpPost]
        public async Task<ActionResult<Attendance>> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag)
        {
            try
            {
                var createdAttendance = await _attendanceService.createAttendance(StudentId, SubjectId, Date, AttendanceFlag);
                if (createdAttendance == null)
                {
                    return BadRequest("No se pudo crear la asistencia");
                }
                return CreatedAtAction(nameof(GetAttendance), new { AttendanceId = createdAttendance.AttendanceId }, createdAttendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asistencia: {ex.Message}");
            }
        }

        [HttpPut("{AttendanceId}")]
        public async Task<ActionResult<Attendance>> UpdateAttendance(int AttendanceId, [FromBody] Attendance attendance)
        {
            if (AttendanceId != attendance.AttendanceId)
            {
                return BadRequest("Error");
            }
            var updatedAttendance = await _attendanceService.updateAttendance(AttendanceId, attendance.StudentId, attendance.SubjectId, attendance.Date, attendance.AttendanceFlag);
            if (updatedAttendance == null)
            {
                return NotFound("Asistencia no encontrado");
            }
            return Ok(updatedAttendance);
        }

        [HttpPut]
        public async Task<ActionResult<Attendance>> DeactivateAttendance(int AttendanceId)
        {
            var deactivatedAttendance = await _attendanceService.deleteAttendance(AttendanceId, false);
            if (deactivatedAttendance == null)
            {
                return NotFound("Asistencia no encontrada");
            }
            return Ok(deactivatedAttendance);
        }
    }
}