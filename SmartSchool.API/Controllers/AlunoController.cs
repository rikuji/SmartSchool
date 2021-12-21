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
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Aluno aluno)
        {
            _repository.Add(aluno);
            if (_repository.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Aluno alunoUpdate)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _repository.Update(alunoUpdate);
            if (_repository.SaveChanges())
                return Ok(alunoUpdate);

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Aluno alunoUpdate)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _repository.Update(alunoUpdate);
            if (_repository.SaveChanges())
                return Ok(alunoUpdate);

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
                return Ok("Aluno deletado");

            return BadRequest("Aluno não deletado");
        }
    }
}
