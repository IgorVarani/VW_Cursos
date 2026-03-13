namespace VW_Cursos.DTOs.CursoDto
{
    public class CriarCursoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public decimal CargaHoraria { get; set; }

        public List<int> IntrutorIds { get; set; } = new();
    }
}
