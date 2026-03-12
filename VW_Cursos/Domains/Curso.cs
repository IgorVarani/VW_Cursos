using System;
using System.Collections.Generic;

namespace VW_Cursos.Domains;

public partial class Curso
{
    public int CursoId { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public decimal CargaHoraria { get; set; }

    public bool? StatusCurso { get; set; }

    public int? InstrutorId { get; set; }

    public virtual Instrutor? Instrutor { get; set; }

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
