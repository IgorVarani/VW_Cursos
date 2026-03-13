using Microsoft.EntityFrameworkCore;
using VW_Cursos.Contexts;
using VW_Cursos.Domains;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly VW_CursosContext _context;

        public MatriculaRepository(VW_CursosContext context)
        {
            _context = context;
        }

        public List<Matricula> Listar()
        {
            List<Matricula> matriculas = _context.Matricula
                .Include(matricula => matricula.Aluno)
                .Include(matricula => matricula.Curso)
                .ToList();

            return matriculas;
        }

        public Matricula ObterPorId(int id)
        {
            Matricula? matricula = _context.Matricula
                .Include(matricula => matricula.Aluno)
                .Include(matricula => matricula.Curso)

                .FirstOrDefault(matriculaDb => matriculaDb.MatriculaId == id);

            return matricula!;
        }

        public void Adicionar(Matricula matricula, List<int> alunoIds, List<int> cursoIds)
        {
            List<Aluno> alunos = _context.Aluno
                .Where(aluno => alunoIds.Contains(aluno.AlunoId))
                .ToList();

            List<Curso> cursos = _context.Curso
                .Where(curso => cursoIds.Contains(curso.CursoId))
                .ToList();

            _context.Matricula.Add(matricula);
            _context.SaveChanges();
        }

        public void Atualizar(Matricula matricula, List<int> alunoIds, List<int> cursoIds)
        {
            Matricula? matriculaBanco = _context.Matricula
                .Include(matricula => matricula.Aluno)
                .Include(matricula => matricula.Curso)
                .FirstOrDefault(matriculaAux => matriculaAux.MatriculaId == matricula.MatriculaId);

            if (matriculaBanco == null)
            {
                return;
            }

            if (matricula.StatusMatricula.HasValue)
            {
                matriculaBanco.StatusMatricula = matricula.StatusMatricula;
            }

            var alunos = _context.Aluno
                .Where(aluno => alunoIds.Contains(aluno.AlunoId))
                .ToList();

            var cusros = _context.Curso
                .Where(curso => cursoIds.Contains(curso.CursoId))
                .ToList();

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Matricula? matricula = _context.Matricula.FirstOrDefault(matricula => matricula.MatriculaId == id);

            if (matricula == null)
            {
                return;
            }

            _context.Matricula.Remove(matricula);
            _context.SaveChanges();
        }
    }
}
