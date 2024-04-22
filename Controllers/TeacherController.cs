using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetTeachers()
        {
            return Ok(await _teacherService.GetTeachers());
        }
        [HttpGet("{TeacherId}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int TeacherId)
        {
            var teacher = await _teacherService.GetTeacher(TeacherId);
            if(teacher == null)
            {
                return NotFound("Profesor no encontrado");
            }
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> CreateTeacher(string TeacherName, string TeacherLastName,string TeacherEmail, string TeacherPhone)
        {
            try
            {
                var createdTeacher = await _teacherService.CreateTeacher(TeacherName, TeacherLastName, TeacherEmail, TeacherPhone); 
                if(createdTeacher == null)
                {
                    return BadRequest ("No se pudo crear la asistencia");
                }
                return CreatedAtAction(nameof(GetTeacher), new {TeacherId = createdTeacher.TeacherId}, createdTeacher);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error al crear el profesor: {ex.Message}");
            }
        }
        [HttpPut("{TeacherId}")]
        public async Task<ActionResult<Teacher>> UpdateTeacher(int TeacherId, [FromBody] Teacher teacher)
        {
            if(TeacherId != teacher.TeacherId)
            {
                return BadRequest("Error");
            }
            var updatedTeacher = await _teacherService.UpdateTeacher(TeacherId, teacher.TeacherName,teacher.TeacherLastName,teacher.TeacherEmail,teacher.TeacherPhone);
            if(updatedTeacher == null)
            {
                return NotFound("Asistencia no encontrado");
            }
            return Ok(updatedTeacher);
        }

        [HttpPut]
        public async Task<ActionResult<Teacher>> DeactivateTeacher(int TeacherId)
        {
            var deactivatedTeacher = await _teacherService.DeleteTeacher(TeacherId,false);
            if(deactivatedTeacher == null)
            {
                return NotFound("Profesor no encontrado");
            }
            return Ok(deactivatedTeacher);
        }
    }
}