namespace VW_Cursos.DTOs.CursoDto
{
    public class AtualizarCursoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public decimal CargaHoraria { get; set; }

        public List<int> InstrutorIds { get; set; } = new();

        public bool? StatusCurso { get; set; }
    }
}
