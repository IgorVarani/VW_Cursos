using VW_Cursos.Applications.Conversoes;
using VW_Cursos.Domains;
using VW_Cursos.DTOs.MatriculaDto;
using VW_Cursos.Exceptions;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Applications.Services
{
    public class MatriculaService
    {
        private readonly IMatriculaRepository _repository;

        public MatriculaService(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        private static LerMatriculaDto LerDto(Matricula matricula)
        {
            LerMatriculaDto lerMatricula = new LerMatriculaDto
            {
                MatriculaId = matricula.MatriculaId,
                StatusMatricula = matricula.StatusMatricula ?? true
            };

            return lerMatricula;
        }

        public List<LerMatriculaDto> Listar()
        {
            List<Matricula> matriculas = _repository.Listar();

            List<LerMatriculaDto> matriculasDto = matriculas
                .Select(matriculaBanco => LerDto(matriculaBanco))
                .ToList();

            return matriculasDto;
        }

        public LerMatriculaDto ObterPorId(int id)
        {
            Matricula? matricula = _repository.ObterPorId(id);

            if (matricula == null)
            {
                throw new DomainException("Matricula não existe.");
            }

            return LerDto(matricula);
        }

        // Não terminado...
    }
}
