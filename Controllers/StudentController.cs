using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;
using StudentApplication.Context;


namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> getStudents()
        {
            return Ok(await _studentService.getStudents());
        }
        [HttpGet("{StudentId}")]
        public async Task<ActionResult<Student>> getStudent(int StudentId)
        {
            var student = await _studentService.getStudent(StudentId);
            if (student == null)
            {
                return BadRequest("Student not found");
            }
            return Ok(student);
        }
        [HttpPost]
        // public async Task<ActionResult<Student>> CreateStudent(string StudentName, string StudentLastName, string Email, string Birthday)
        // {
        //     var createdStudent = await _studentService.createStudent(StudentName,  StudentLastName,  Email,  Birthday);
        //     if (createdStudent == null)
        //     {
        //         return BadRequest("Error");
        //     }
        //     return CreatedAtAction(nameof(getStudent), new { id = createdStudent.StudentId }, createdStudent);
        // }

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

                // Reemplazar "getStudent" con el nombre correcto de la acción que devuelve un estudiante por ID
                return CreatedAtAction(nameof(getStudent), new { Studentid = createdStudent.StudentId }, createdStudent);
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver una respuesta apropiada
                return StatusCode(500, $"Error al crear el estudiante: {ex.Message}");
            }
        }


        [HttpPut("{StudentId}")]
        public async Task<ActionResult<Student>> updateStudent(int StudentId, Student student)
        {
            if (StudentId != student.StudentId)
            {
                return BadRequest("Errot :c");
            }
            var update = await _studentService.updateStudent(student.StudentId);
            if (update == null)
            {
                return NotFound("Error");
            }
            return Ok(update);
        }

        [HttpPut("{StudentId}/delete")]
        public async Task<ActionResult<Student>> deleteStudent(int StudentId)
        {
            var deactivatedStd = await _studentService.deleteStudent(StudentId, false);
            if(deactivatedStd == null)
            {
                return NotFound("Student not found");
            }
            return Ok(deactivatedStd);
        }

    }
}
