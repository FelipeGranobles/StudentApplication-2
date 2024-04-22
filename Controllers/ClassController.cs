using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Class>>> GetClasses()
        {
            return Ok(await _classService.GetClasses());
        }
        [HttpGet("{ClassId}")]
        public async Task<ActionResult<Class>> GetClass(int ClassId)
        {
            var clase = await _classService.GetClass(ClassId);
            if (clase == null)
            {
                return NotFound ("Clase no encontrada");
            }
            return Ok(clase);
        }
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass(string ClassName)
        {
            try
            {
                var createdClass = await _classService.createClass(ClassName);
                if (createdClass == null)
                {
                    return BadRequest ("No se pudo crear la clase");
                }
                return CreatedAtAction(nameof(GetClass),new { ClassId = createdClass.ClassId},createdClass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asistencia: {ex.Message}");
            }
        }

        [HttpPut("{ClassId}")]
        public async Task<ActionResult<Class>> UpdateClass(int ClassId, [FromBody] Class clase)
        {
            if(ClassId != clase.ClassId)
            {
                return BadRequest("Error llamando el id");
            }
            var updatedClase = await _classService.updateClass(ClassId, clase.ClassName);
            if (updatedClase == null)
            {
                return NotFound("Clase no encontrada");
            }
            return Ok(updatedClase);
        }

        [HttpPut]
        public async Task<ActionResult<Class>> DeleteClass(int ClassId)
        {
            var deactivatedClass = await _classService.deleteClass(ClassId, false);
            if(deactivatedClass == null)
            {
                return NotFound("Clase no encontrada");
            }
            return Ok(deactivatedClass);
        }
    }
}