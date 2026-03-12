using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VW_Cursos.Domains;

namespace VW_Cursos.Contexts;

public partial class VW_CursosContext : DbContext
{
    public VW_CursosContext()
    {
    }

    public VW_CursosContext(DbContextOptions<VW_CursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Aluno { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    public virtual DbSet<Matricula> Matricula { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D20S30-1362981\\SQLEXPRESS;Database=VW_Cursos;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.AlunoId).HasName("PK__Aluno__C1967D8FA78EA692");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoAluno"));

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D105340BF574D9").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(99)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(99)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusAluno).HasDefaultValue(true);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Curso__7E0235D7E538716E");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoCurso"));

            entity.HasIndex(e => e.Nome, "UQ__Curso__7D8FE3B204E68777").IsUnique();

            entity.Property(e => e.CargaHoraria).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.Nome)
                .HasMaxLength(99)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusCurso).HasDefaultValue(true);

            entity.HasOne(d => d.Instrutor).WithMany(p => p.Curso)
                .HasForeignKey(d => d.InstrutorId)
                .HasConstraintName("FK__Curso__Instrutor__4F7CD00D");
        });

        modelBuilder.Entity<Instrutor>(entity =>
        {
            entity.HasKey(e => e.InstrutorId).HasName("PK__Instruto__096B8414E8E9AE4C");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoInstrutor"));

            entity.Property(e => e.InstrutorId).ValueGeneratedNever();
            entity.Property(e => e.Especializacao)
                .HasMaxLength(99)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(99)
                .IsUnicode(false);
            entity.Property(e => e.StatusInstrutor).HasDefaultValue(true);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.MatriculaId).HasName("PK__Matricul__908CEE02EC382AB5");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoMatricula"));

            entity.Property(e => e.StatusMatricula).HasDefaultValue(true);

            entity.HasOne(d => d.Aluno).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.AlunoId)
                .HasConstraintName("FK__Matricula__Aluno__5812160E");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("FK__Matricula__Curso__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
