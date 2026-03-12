using System.Security.Cryptography;
using System.Text;
using VW_Cursos.Domains;
using VW_Cursos.DTOs.AlunoDto;
using VW_Cursos.Exceptions;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Applications.Services
{
    public class AlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
        }

        private static LerAlunoDto LerDto(Aluno aluno)
        {
            LerAlunoDto lerAluno = new LerAlunoDto
            {
                AlunoId = aluno.AlunoId,
                Nome = aluno.Nome,
                Email = aluno.Email,
                StatusAluno = aluno.StatusAluno ?? true
            };

            return lerAluno;
        }

        public List<LerAlunoDto> Listar()
        {
            List<Aluno> alunos = _repository.Listar();

            List<LerAlunoDto> alunosDto = alunos.Select(alunoBanco => LerDto(alunoBanco)).ToList();

            return alunosDto;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerAlunoDto ObterPorId(int id)
        {
            Aluno? aluno = _repository.ObterPorId(id);

            if (aluno == null)
            {
                throw new DomainException("Aluno não existe.");
            }

            return LerDto(aluno);
        }

        public LerAlunoDto ObterPorEmail(string email)
        {
            Aluno? aluno = _repository.ObterPorEmail(email);

            if (aluno == null)
            {
                throw new DomainException("Aluno não existe.");
            }

            return LerDto(aluno);
        }

        public LerAlunoDto Adicionar(CriarAlunoDto alunoDto)
        {
            ValidarEmail(alunoDto.Email);

            if (_repository.EmailExiste(alunoDto.Email))
            {
                throw new DomainException("Já existe um aluno com este e-mail.");
            }

            Aluno aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                Senha = HashSenha(alunoDto.Senha),
                StatusAluno = true
            };

            _repository.Adicionar(aluno);

            return LerDto(aluno);
        }

        public LerAlunoDto Atualizar(int id, CriarAlunoDto alunoDto)
        {
            Aluno alunoBanco = _repository.ObterPorId(id)!;

            if (alunoBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            ValidarEmail(alunoDto.Email);

            Aluno alunoComMesmoEmail = _repository.ObterPorEmail(alunoDto.Email)!;

            if (alunoComMesmoEmail != null && alunoComMesmoEmail.AlunoId != id)
            {
                throw new DomainException("Já existe um aluno com este e-mail.");
            }

            alunoBanco.Nome = alunoDto.Nome;
            alunoBanco.Email = alunoDto.Email;
            alunoBanco.Senha = HashSenha(alunoDto.Senha);

            _repository.Atualizar(alunoBanco);

            return LerDto(alunoBanco);
        }

        public void Remover(int id)
        {
            Aluno aluno = _repository.ObterPorId(id)!;

            if (aluno == null)
            {
                throw new DomainException("Aluno não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
