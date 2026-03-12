using VW_Cursos.Contexts;
using VW_Cursos.Domains;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly VW_CursosContext _context;

        public AlunoRepository(VW_CursosContext context)
        {
            _context = context;
        }

        public List<Aluno> Listar()
        {
            return _context.Aluno.ToList();
        }

        public Aluno? ObterPorId(int id)
        {
            return _context.Aluno.Find(id);
        }

        public Aluno? ObterPorEmail(string email)
        {
            return _context.Aluno.FirstOrDefault(aluno => aluno.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Aluno.Any(aluno => aluno.Email == email);
        }

        public void Adicionar(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            _context.SaveChanges();
        }

        public void Atualizar(Aluno aluno)
        {
            Aluno? alunoBanco =
                _context.Aluno.FirstOrDefault(alunoAux => alunoAux.AlunoId == aluno.AlunoId);

            if (alunoBanco == null)
            {
                return;
            }

            alunoBanco.Nome = aluno.Nome;
            alunoBanco.Email = aluno.Email;
            alunoBanco.Senha = aluno.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Aluno? aluno =
                _context.Aluno.FirstOrDefault(alunoAux => alunoAux.AlunoId == id);

            if (aluno == null)
            {
                return;
            }

            _context.Aluno.Remove(aluno);
            _context.SaveChanges();
        }
    }
}
