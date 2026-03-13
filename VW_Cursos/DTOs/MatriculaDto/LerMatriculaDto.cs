namespace VW_Cursos.DTOs.MatriculaDto
{
    public class LerMatriculaDto
    {
        public int MatriculaId { get; set; }

        public int AlunoId { get; set; }

        public int CursoId { get; set; }

        public bool? StatusMatricula { get; set; }

        // Aluno

        public List<int> AlunoIds { get; set; } = new();
        public List<string> Alunos { get; set; } = new();

        // Curso

        public List<int> CursoIds { get; set; } = new();
        public List<string> Cursos { get; set; } = new();
    }
}
