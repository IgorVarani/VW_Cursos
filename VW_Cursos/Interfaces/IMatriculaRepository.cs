using VW_Cursos.Domains;

namespace VW_Cursos.Interfaces
{
    public interface IMatriculaRepository
    {
        List<Matricula> Listar();
        Matricula ObterPorId(int id);
        void Adicionar(Matricula matricula, List<int> alunoIds, List<int> cursoIds);
        void Atualizar(Matricula matricula, List<int> alunoIds, List<int> cursoIds);
        void Remover(int id);
    }
}
