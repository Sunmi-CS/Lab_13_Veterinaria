using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaAPI.Datos.Models;

public partial class VeterinariaContext : DbContext
{
    public VeterinariaContext(DbContextOptions<VeterinariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    public virtual DbSet<Propietario> Propietarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota).HasName("PK__Mascotas__5C9C26F0D38294DF");

            entity.Property(e => e.Especie).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(80);
            entity.Property(e => e.Raza).HasMaxLength(80);

            entity.HasOne(d => d.IdPropietarioNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdPropietario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascotas_Propietarios");
        });

        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(e => e.IdPropietario).HasName("PK__Propieta__4D581B50DAEC98AD");

            entity.HasIndex(e => e.Correo, "UQ_Propietarios_Correo").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(120);
            entity.Property(e => e.Nombres).HasMaxLength(80);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
