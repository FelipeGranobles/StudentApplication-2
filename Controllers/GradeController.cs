using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Repositories;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GradeController : ControllerBase{
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService) 
        {
            _gradeService = gradeService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Grade>>> GetGrades()
        {
            return Ok(await _gradeService.GetGrades());
        }
        [HttpGet("{GradeId}")]
        public async Task<ActionResult<Grade>> GetGrade(int GradeId)
        {
            var grade = await _gradeService.GetGrade(GradeId);
            if (grade == null) 
            {
                return NotFound("Grado no encontrada");
            }
            return Ok(grade);
        }
        [HttpPost]
        public async Task<ActionResult<Grade>> CreateGrade(int StudentId, int SubjectId, float Score, string Description)
        {
            try
            {
                var createdGrade = await _gradeService.CreateGrade(StudentId,SubjectId,Description,Score);
                if (createdGrade == null)
                {
                    return BadRequest("No se pudo crear");
                }
                return CreatedAtAction (nameof(GetGrade), new { GradeId = createdGrade.GradeId}, createdGrade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asistencia: {ex.Message}");
            }
        }

        [HttpPut("{GradeId}")]
        public async Task<ActionResult<Grade>> UpdateGrade(int GradeId, [FromBody] Grade grade)
        {
            if(GradeId != grade.GradeId)
            {
                return BadRequest("Error");
            }
            var updatedGrade = await _gradeService.UpdateGrade(GradeId, grade.StudentId,grade.SubjectId,grade.Score,grade.Description);
            if(updatedGrade == null)
            {
                return NotFound("Registro no encontrado");
            }
            return Ok(updatedGrade);
        }

        [HttpPut]
        public async Task<ActionResult<Grade>> DeactivateGrade(int GradeId)
        {
            var deactivatedGrade = await _gradeService.DeleteGrade(GradeId, false);
            if(deactivatedGrade == null)
            {
                return NotFound("Registro no encontrado");
            }
            return Ok(deactivatedGrade);
        } 
    }
}