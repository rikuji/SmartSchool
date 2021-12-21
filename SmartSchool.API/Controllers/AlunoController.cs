using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System.Linq;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno alunoUpdate)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _context.Update(alunoUpdate);
            _context.SaveChanges();
            return Ok(alunoUpdate);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Aluno alunoUpdate)
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _context.Update(alunoUpdate);
            _context.SaveChanges();
            return Ok(alunoUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(x => x.Id == id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _context.Remove(aluno);
            _context.SaveChanges();

            return Ok();
        }
    }
}
