using Microsoft.EntityFrameworkCore;
using VW_Cursos.Contexts;
using VW_Cursos.Domains;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly VW_CursosContext _context;

        public CursoRepository(VW_CursosContext context)
        {
            _context = context;
        }

        public List<Curso> Listar()
        {
            List<Curso> cursos = _context.Curso
                .Include(curso => curso.Instrutor)
                .ToList();

            return cursos;
        }

        public Curso ObterPorId(int id)
        {
            Curso? curso = _context.Curso
                .Include(cursoDb => cursoDb.Instrutor)

                .FirstOrDefault(cursoDb => cursoDb.CursoId == id);

            return curso!;
        }

        public bool NomeExiste(string nome, int? cursoIdAtual = null)
        {
            var cursoConsultado = _context.Curso.AsQueryable();

            if (cursoIdAtual.HasValue)
            {
                cursoConsultado = cursoConsultado.Where(curso => curso.CursoId != cursoIdAtual.Value);
            }

            return cursoConsultado.Any(curso => curso.Nome == nome);
        }

        public void Adicionar(Curso curso, List<int> instrutorIds)
        {
            List<Instrutor> instrutores = _context.Instrutor
                .Where(instrutor => instrutorIds.Contains(instrutor.InstrutorId))
                .ToList();

            _context.Curso.Add(curso);
            _context.SaveChanges();
        }

        public void Atualizar(Curso curso, List<int> instrutorIds)
        {
            Curso? cursoBanco = _context.Curso
                .Include(curso => curso.Instrutor)
                .FirstOrDefault(cursoAux => cursoAux.CursoId == curso.CursoId);

            if (cursoBanco == null)
            {
                return;
            }

            cursoBanco.Nome = curso.Nome;
            cursoBanco.Preco = curso.Preco;
            cursoBanco.Descricao = curso.Descricao;

            if (curso.StatusCurso.HasValue)
            {
                cursoBanco.StatusCurso = curso.StatusCurso;
            }

            var instrutores = _context.Instrutor
                .Where(instrutor => instrutorIds.Contains(instrutor.InstrutorId))
                .ToList();

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Curso? curso = _context.Curso.FirstOrDefault(curso => curso.CursoId == id);

            if(curso == null)
            {
                return;
            }

            _context.Curso.Remove(curso);
            _context.SaveChanges();
        }
    }
}
