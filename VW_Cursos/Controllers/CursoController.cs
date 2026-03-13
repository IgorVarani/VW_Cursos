using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VW_Cursos.Applications.Services;
using VW_Cursos.DTOs.CursoDto;
using VW_Cursos.Exceptions;

namespace VW_Cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _service;

        public CursoController(CursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerCursoDto>> Listar()
        {
            List<LerCursoDto> produtos = _service.Listar();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerCursoDto> ObterPorId(int id)
        {
            LerCursoDto curso = _service.ObterPorId(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public ActionResult Adicionar([FromForm] CriarCursoDto cursoDto)
        {
            try
            {
                _service.Adicionar(cursoDto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public ActionResult Atualizar(int id, [FromForm] AtualizarCursoDto cursoDto)
        {
            try
            {
                _service.Atualizar(id, cursoDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
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
