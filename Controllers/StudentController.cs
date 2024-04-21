using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(await _studentService.getStudents());
        }
        [HttpGet("{StudentId}")]
        public async Task<ActionResult<Student>> GetStudent(int StudentId)
        {
            var student = await _studentService.getStudent(StudentId);
            if (student == null)
            {
                return NotFound("Estudiante no encontrado");
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(string StudentName, string StudentLastName, string Email, string Birthday)
        {
            // Validar la entrada
            if (string.IsNullOrEmpty(StudentName) || string.IsNullOrEmpty(StudentLastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Birthday))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            try
            {
                var createdStudent = await _studentService.createStudent(StudentName, StudentLastName, Email, Birthday);
                if (createdStudent == null)
                {
                    return BadRequest("No se pudo crear el estudiante.");
                }

                return CreatedAtAction(nameof(GetStudent), new { StudentId = createdStudent.StudentId }, createdStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estudiante: {ex.Message}");
            }
        }
        [HttpPut("{StudentId}")]
        public async Task<ActionResult<Student>> UpdateStudent(int StudentId, [FromBody] Student student)
        {
            if (StudentId != student.StudentId)
            {
                return BadRequest("Error");
            }

            var updatedStudent = await _studentService.updateStudent(StudentId, student.StudentName, student.StudentLastName, student.Email, student.Birthdate);

            if (updatedStudent == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            return Ok(updatedStudent);
        }
        [HttpPut("{StudentId}/deactivate")]
        public async Task<ActionResult<Student>> DeactivateStudent(int StudentId)
        {
            var deactivatedStudent = await _studentService.deleteStudent(StudentId, false);
            if (deactivatedStudent == null)
            {
                return NotFound("Estudiante no encontrado");
            }
            return Ok(deactivatedStudent);
        }
    }
}
