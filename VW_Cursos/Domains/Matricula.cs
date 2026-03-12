using System;
using System.Collections.Generic;

namespace VW_Cursos.Domains;

public partial class Matricula
{
    public int MatriculaId { get; set; }

    public int? AlunoId { get; set; }

    public int? CursoId { get; set; }

    public bool? StatusMatricula { get; set; }

    public virtual Aluno? Aluno { get; set; }

    public virtual Curso? Curso { get; set; }
}
