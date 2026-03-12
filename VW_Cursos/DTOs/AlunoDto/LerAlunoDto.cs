namespace VW_Cursos.DTOs.AlunoDto
{
    public class LerAlunoDto
    {
        public int AlunoId { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool StatusAluno { get; set; }
    }
}
