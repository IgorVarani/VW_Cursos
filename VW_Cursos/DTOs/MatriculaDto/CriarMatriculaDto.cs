namespace VW_Cursos.DTOs.MatriculaDto
{
    public class CriarMatriculaDto
    {
        public List<int> AlunoIds { get; set; } = new();

        public List<int> CursoIds { get; set; } = new();
    }
}
