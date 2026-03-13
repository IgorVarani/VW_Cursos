using VW_Cursos.Applications.Conversoes;
using VW_Cursos.Domains;
using VW_Cursos.DTOs.CursoDto;
using VW_Cursos.Exceptions;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Applications.Services
{
    public class CursoService
    {
        private readonly ICursoRepository _repository;

        public CursoService(ICursoRepository repository)
        {
            _repository = repository;
        }

        public List<LerCursoDto> Listar()
        {
            List<Curso> cursos = _repository.Listar();

            List<LerCursoDto> cursosDto = cursos.Select(CursoParaDto.ConverterParaDto).ToList();

            return cursosDto;
        }

        public LerCursoDto ObterPorId(int id)
        {
            Curso curso = _repository.ObterPorId(id);

            if (curso == null)
            {
                throw new DomainException("Curso não encontrado.");
            }

            return CursoParaDto.ConverterParaDto(curso);
        }

        public LerCursoDto Adicionar(CriarCursoDto cursoDto)
        {
            if (_repository.NomeExiste(cursoDto.Nome))
            {
                throw new DomainException("Curso já existente.");
            }

            Curso curso = new Curso
            {
                Nome = cursoDto.Nome,
                Preco = cursoDto.Preco,
                Descricao = cursoDto.Descricao,
                CargaHoraria = cursoDto.CargaHoraria,
                StatusCurso = true,
            };

            _repository.Adicionar(curso, cursoDto.IntrutorIds);

            return CursoParaDto.ConverterParaDto(curso);
        }

        public LerCursoDto Atualizar(int id, AtualizarCursoDto cursoDto)
        {
            Curso cursoBanco = _repository.ObterPorId(id);

            if (cursoBanco == null)
            {
                throw new DomainException("Curso não encontrado.");
            }

            if (_repository.NomeExiste(cursoDto.Nome, cursoIdAtual: id))
            {
                throw new DomainException("Já existe outro curso com esse nome.");
            }

            if (cursoDto.InstrutorIds == null || cursoDto.InstrutorIds.Count == 0)
            {
                throw new DomainException("Curso deve ter ao menos um Instrutor.");
            }

            if (cursoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            cursoBanco.Nome = cursoDto.Nome;
            cursoBanco.Preco = cursoDto.Preco;
            cursoBanco.Descricao = cursoDto.Descricao;
            cursoBanco.CargaHoraria = cursoDto.CargaHoraria;

            if (cursoDto.StatusCurso.HasValue)
            {
                cursoBanco.StatusCurso = cursoDto.StatusCurso.Value;
            }

            _repository.Atualizar(cursoBanco, cursoDto.InstrutorIds);

            return CursoParaDto.ConverterParaDto(cursoBanco);
        }

        public void Remover(int id)
        {
            Curso curso = _repository.ObterPorId(id);

            if (curso == null)
            {
                throw new DomainException("Curso não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
