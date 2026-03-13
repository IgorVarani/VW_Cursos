using System.Security.Cryptography;
using System.Text;
using VW_Cursos.Domains;
using VW_Cursos.DTOs.InstrutorDto;
using VW_Cursos.Exceptions;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Applications.Services
{
    public class InstrutorService
    {
        private readonly IInstrutorRepository _repository;

        public InstrutorService(IInstrutorRepository repository)
        {
            _repository = repository;
        }

        private static LerInstrutorDto LerDto(Instrutor instrutor)
        {
            LerInstrutorDto lerInstrutor = new LerInstrutorDto
            {
                InstrutorId = instrutor.InstrutorId,
                Nome = instrutor.Nome,
                Especializacao = instrutor.Especializacao,
                StatusInstrutor = instrutor.StatusInstrutor ?? true
            };

            return lerInstrutor;
        }

        public List<LerInstrutorDto> Listar()
        {
            List<Instrutor> instrutores = _repository.Listar();

            List<LerInstrutorDto> instrutoresDto = instrutores.Select(instrutorBanco => LerDto(instrutorBanco)).ToList();

            return instrutoresDto;
        }

        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor? instrutor = _repository.ObterPorId(id);

            if (instrutor == null)
            {
                throw new DomainException("Instrutor não existe.");
            }

            return LerDto(instrutor);
        }

        public LerInstrutorDto Adicionar(CriarInstrutorDto instrutorDto)
        {
            Instrutor instrutor = new Instrutor()
            {
                Nome = instrutorDto.Nome,
                Especializacao = instrutorDto.Especializacao,
                StatusInstrutor = true
            };

            _repository.Adicionar(instrutor);

            return LerDto(instrutor);
        }

        public LerInstrutorDto Atualizar(int id, CriarInstrutorDto instrutorDto)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id)!;

            if (instrutorBanco == null)
            {
                throw new DomainException("Instrutor não encontrado.");
            }

            instrutorBanco.Nome = instrutorDto.Nome;
            instrutorBanco.Especializacao = instrutorDto.Especializacao;

            _repository.Atualizar(instrutorBanco);

            return LerDto(instrutorBanco);
        }

        public void Remover(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id)!;

            if (instrutor == null)
            {
                throw new DomainException("Instrutor não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
