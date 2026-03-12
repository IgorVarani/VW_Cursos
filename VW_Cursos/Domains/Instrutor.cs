using System;
using System.Collections.Generic;

namespace VW_Cursos.Domains;

public partial class Instrutor
{
    public int InstrutorId { get; set; }

    public string Nome { get; set; } = null!;

    public string Especializacao { get; set; } = null!;

    public bool? StatusInstrutor { get; set; }

    public virtual ICollection<Curso> Curso { get; set; } = new List<Curso>();
}
