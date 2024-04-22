using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Subject>>> GetSubjects()
        {
            return Ok(await _subjectService.GetSubjects());
        }
        [HttpGet("{SubjectId}")]
        public async Task<ActionResult<Subject>> GetSubject(int SubjectId)
        {
            var subject = await _subjectService.GetSubject(SubjectId);
            if (subject == null)
            {
                return NotFound("Materia no encontrada");
            }
            return Ok(subject);
        }
        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject(int ClassId, string SubjectName, string Subjectcode, Teacher teacher)
        {
            try
            {
                var createdSubject = await _subjectService.CreateSubject(ClassId, SubjectName, Subjectcode, teacher);
                if (createdSubject == null)
                {
                    return BadRequest("No se pudo crear la materia");
                }
                return CreatedAtAction(nameof(GetSubject), new { SubjectId = createdSubject.SubjectId, }, createdSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la Materia: {ex.Message}");
            }
        }

        [HttpPut("{SubjectId}")]
        public async Task<ActionResult<Subject>> UpdateSubject(int SubjectId, [FromBody] Subject subject)
        {
            if (SubjectId != subject.SubjectId)
            {
                return BadRequest("El ID de la materia en la ruta no coincide con el ID de la materia proporcionado.");
            }
            if (subject == null)
            {
                return BadRequest("Error");
            }
            var updatedSubject = await _subjectService.UpdateSubject(SubjectId, subject.ClassId, subject.SubjectName, subject.Subjectcode);
            if (updatedSubject == null)
            {
                return NotFound("Materia no encontrada");
            }
            return Ok(updatedSubject);
        }
        [HttpPut]
        public async Task<ActionResult<Subject>> DeleteSubject(int SubjectId)
        {
            var deactivatedSubject = await _subjectService.DeleteSubject(SubjectId, false);
            if (deactivatedSubject == null)
            {
                return NotFound("Materia no encontrada");
            }
            return Ok(deactivatedSubject);
        }
    }
}