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
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            if (student == null)
            {
                return BadRequest("Error");
            }
            var createdStudent = await _studentService.createStudent(student.StudentName, student.StudentLastName, student.Email, student.Birthdate);
            if (createdStudent == null)
            {
                return BadRequest("Error");
            }
            return CreatedAtAction(nameof(getStudent), new { id = createdStudent.StudentId }, createdStudent);
        }

        // [HttpPut("{StudentId}")]
        // public async Task<ActionResult<Student>> updateStudent(int StudentId, Student student)
        // {
        //     if(StudentId != student.StudentId)
        //     {
        //         return BadRequest("Errot :c");
        //     }

        // }
    }
}
