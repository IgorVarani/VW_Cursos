namespace VW_Cursos.DTOs.CursoDto
{
    public class LerCursoDto
    {
        public int CursoId { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public decimal CargaHoraria { get; set; }

        public bool? StatusCurso { get; set; }

        // Instrutor

        public List<int> InstrutorIds { get; set; } = new();
        public List<string> Instrutores { get; set; } = new();
    }
}
