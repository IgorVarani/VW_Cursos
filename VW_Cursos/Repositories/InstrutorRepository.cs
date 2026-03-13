using VW_Cursos.Contexts;
using VW_Cursos.Domains;
using VW_Cursos.Interfaces;

namespace VW_Cursos.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private readonly VW_CursosContext _context;

        public InstrutorRepository(VW_CursosContext context)
        {
            _context = context;
        }

        public List<Instrutor> Listar()
        {
            return _context.Instrutor.ToList();
        }

        public Instrutor? ObterPorId(int id)
        {
            return _context.Instrutor.Find(id);
        }

        public void Adicionar(Instrutor instrutor)
        {
            _context.Instrutor.Add(instrutor);
            _context.SaveChanges();
        }

        public void Atualizar(Instrutor instrutor)
        {
            Instrutor? instrutorBanco = _context.Instrutor.FirstOrDefault(instrutorAux => instrutorAux.InstrutorId == instrutor.InstrutorId);

            if (instrutorBanco == null)
            {
                return;
            }

            instrutorBanco.Nome = instrutor.Nome;
            instrutorBanco.Especializacao = instrutor.Especializacao;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Instrutor? instrutor = _context.Instrutor.FirstOrDefault(instrutorAux => instrutorAux.InstrutorId == id);

            if (instrutor == null)
            {
                return;
            }

            _context.Instrutor.Remove(instrutor);
            _context.SaveChanges();
        }
    }
}
