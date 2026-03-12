using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VW_Cursos.Applications.Services;
using VW_Cursos.DTOs.AlunoDto;
using VW_Cursos.Exceptions;

namespace VW_Cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;

        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerAlunoDto>> Listar()
        {
            List<LerAlunoDto> alunos = _service.Listar();

            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerAlunoDto> ObterPorId(int id)
        {
            LerAlunoDto aluno = _service.ObterPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpGet("email/{email}")]
        public ActionResult<LerAlunoDto> ObterPorEmail(string email)
        {
            LerAlunoDto aluno = _service.ObterPorEmail(email);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public ActionResult<LerAlunoDto> Adicionar(int id, CriarAlunoDto alunoDto)
        {
            try
            {
                LerAlunoDto alunoAtualizado = _service.Atualizar(id, alunoDto);

                return StatusCode(200, alunoAtualizado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LerAlunoDto> Atualizar(int id, CriarAlunoDto alunoDto)
        {
            try
            {
                LerAlunoDto alunoAtualizado = _service.Atualizar(id, alunoDto);

                return StatusCode(200, alunoAtualizado);
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
