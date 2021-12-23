using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;
using System.Collections.Generic;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if (_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}",  _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AlunoRegistrarDto model)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] AlunoRegistrarDto model)
        {
            var aluno = _repository.GetAlunoById(id);
            if (aluno == null)
                return BadRequest("O aluno não foi encontrado");

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            if (_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

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
