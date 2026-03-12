using System;
using System.Collections.Generic;

namespace VW_Cursos.Domains;

public partial class Aluno
{
    public int AlunoId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public bool? StatusAluno { get; set; }

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
