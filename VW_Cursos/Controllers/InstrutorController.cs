using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VW_Cursos.Applications.Services;
using VW_Cursos.DTOs.InstrutorDto;
using VW_Cursos.Exceptions;

namespace VW_Cursos.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrutorController : ControllerBase
    {
        private readonly InstrutorService _service;

        public InstrutorController(InstrutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerInstrutorDto>> Listar()
        {
            List<LerInstrutorDto> instrutores = _service.Listar();

            return Ok(instrutores);
        }

        [HttpGet("{id}")]
        public ActionResult<LerInstrutorDto> ObterPorId(int id)
        {
            LerInstrutorDto instrutor = _service.ObterPorId(id);

            if (instrutor == null)
            {
                return NotFound();
            }

            return Ok(instrutor);
        }

        [HttpPost]
        public ActionResult<LerInstrutorDto> Adicionar(CriarInstrutorDto instrutorDto)
        {
            try
            {
                LerInstrutorDto instrutorCriado = _service.Adicionar(instrutorDto);

                return StatusCode(201, instrutorCriado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LerInstrutorDto> Atualizar(int id, CriarInstrutorDto instrutorDto)
        {
            try
            {
                LerInstrutorDto instrutorAtualizado = _service.Atualizar(id, instrutorDto);

                return StatusCode(201, instrutorAtualizado);
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
