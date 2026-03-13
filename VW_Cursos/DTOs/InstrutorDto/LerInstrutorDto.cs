namespace VW_Cursos.DTOs.InstrutorDto
{
    public class LerInstrutorDto
    {
        public int InstrutorId { get; set; }

        public string Nome { get; set; } = null!;

        public string Especializacao { get; set; } = null!;

        public bool StatusInstrutor { get; set; }
    }
}
