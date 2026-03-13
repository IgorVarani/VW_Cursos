using VW_Cursos.Domains;

namespace VW_Cursos.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        bool NomeExiste(string nome, int? cursoIdAtual = null);
        void Adicionar(Curso curso, List<int> instrutorIds);
        void Atualizar(Curso curso, List<int> instrutorIds);
        void Remover(int id);
    }
}
