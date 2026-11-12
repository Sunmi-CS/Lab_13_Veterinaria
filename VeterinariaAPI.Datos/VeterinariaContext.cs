using Microsoft.EntityFrameworkCore;
using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos;

public partial class VeterinariaContext : DbContext
{
    public VeterinariaContext(DbContextOptions<VeterinariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Propietario> Propietarios { get; set; }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(e => e.IdPropietario);

            entity.ToTable("Propietarios");

            entity.HasIndex(e => e.Correo)
                .IsUnique()
                .HasDatabaseName("UQ_Propietarios_Correo");

            entity.Property(e => e.Nombres)
                .HasMaxLength(80)
                .IsRequired();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsRequired();
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota);

            entity.ToTable("Mascotas");

            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsRequired();

            entity.Property(e => e.Especie)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Raza)
                .HasMaxLength(80)
                .IsRequired();

            entity.HasOne(d => d.Propietario)
                .WithMany(p => p.Mascotas)
                .HasForeignKey(d => d.IdPropietario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Mascotas_Propietarios");
        });
    }
}
