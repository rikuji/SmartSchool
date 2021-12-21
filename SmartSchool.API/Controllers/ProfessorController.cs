using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor não foi encontrado");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professorUpdate)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _context.Update(professorUpdate);
            _context.SaveChanges();
            return Ok(professorUpdate);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor professorUpdate)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _context.Update(professorUpdate);
            _context.SaveChanges();
            return Ok(professorUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null) return BadRequest("Professor não foi encontrado");
            _context.Remove(professor);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
