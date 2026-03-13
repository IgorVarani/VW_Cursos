using VW_Cursos.Domains;

namespace VW_Cursos.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();

        Instrutor? ObterPorId(int id);

        void Adicionar(Instrutor instrutor);

        void Atualizar(Instrutor instrutor);

        void Remover(int id);
    }
}
