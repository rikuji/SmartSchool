using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            _repository.Add(professor);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professorUpdate)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _repository.Update(professorUpdate);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor professorUpdate)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");
            _repository.Update(professorUpdate);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _repository.Delete(professor);
            if (_repository.SaveChanges())
                return Ok("Professor deletado");

            return BadRequest("Professor não deletado");
        }
    }
}
