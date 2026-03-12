using VW_Cursos.Domains;

namespace VW_Cursos.Interfaces
{
    public interface IAlunoRepository
    {
        List<Aluno> Listar();

        Aluno? ObterPorId(int id);

        Aluno? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar(Aluno aluno);

        void Atualizar(Aluno aluno);

        void Remover(int id);
    }
}
