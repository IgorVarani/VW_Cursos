using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VW_Cursos.Applications.Services;
using VW_Cursos.DTOs.MatriculaDto;
using VW_Cursos.Exceptions;

namespace VW_Cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly MatriculaService _service;

        public MatriculaController(MatriculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerMatriculaDto>> Listar()
        {
            List<LerMatriculaDto> matriculas = _service.Listar();

            return Ok(matriculas);
        }

        [HttpGet("{id}")]
        public ActionResult<LerMatriculaDto> ObterPorId(int id)
        {
            LerMatriculaDto matricula = _service.ObterPorId(id);

            if (matricula == null)
            {
                return NotFound();
            }

            return Ok(matricula);
        }

        [HttpPost]
        public ActionResult Adicionar([FromForm] CriarMatriculaDto matriculaDto, int alunoId, int cursoId)
        {
            try
            {
                _service.Adicionar(matriculaDto, alunoId, cursoId);

                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, [FromBody] CriarMatriculaDto matriculaDto, int alunoId, int cursoId)
        {
            try
            {
                _service.Atualizar(id, matriculaDto, alunoId, cursoId);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
