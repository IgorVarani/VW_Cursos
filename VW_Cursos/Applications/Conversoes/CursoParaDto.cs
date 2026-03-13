using VW_Cursos.Domains;
using VW_Cursos.DTOs.CursoDto;

namespace VW_Cursos.Applications.Conversoes
{
    public class CursoParaDto
    {
        public static LerCursoDto ConverterParaDto(Curso curso)
        {
            return new LerCursoDto
            {
                CursoId = curso.CursoId,
                Nome = curso.Nome,
                Preco = curso.Preco,
                Descricao = curso.Descricao,
                StatusCurso = curso.StatusCurso,
            };
        }
    }
}
