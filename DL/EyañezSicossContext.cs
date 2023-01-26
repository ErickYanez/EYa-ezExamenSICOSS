using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class EyañezSicossContext : DbContext
{
    public EyañezSicossContext()
    {
    }

    public EyañezSicossContext(DbContextOptions<EyañezSicossContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calculadora> Calculadoras { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=EYañezSICOSS; Trusted_Connection=True; User ID=sa; Password=pass@word1;TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculadora>(entity =>
        {
            entity.HasKey(e => e.IdCalculadora).HasName("PK__Calculad__F502BD7FA9EDE8B7");

            entity.ToTable("Calculadora");

            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.Numero).IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Calculadoras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Calculado__IdUsu__25869641");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971537494D");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Password, "UQ__Usuario__87909B15DAEAAEC8").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F284569F20A9A7").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
