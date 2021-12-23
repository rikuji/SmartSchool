﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;
using System.Collections.Generic;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);
            if (_repository.SaveChanges())
                return Created($"api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor model)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor professorUpdate)
        {
            var professor = _repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não foi encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

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
