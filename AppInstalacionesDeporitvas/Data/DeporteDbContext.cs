using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AppInstalacionesDeporitvas.Models;
namespace AppInstalacionesDeporitvas.Data

{
    public class DeporteDbContext : DbContext
    {
        //DbContext representa una sesión de trabajo con la base de datos.
        public DeporteDbContext(DbContextOptions<DeporteDbContext> options): base(options)
        {
        }


        //Dbset gestiona y consulta un conjunto de entidades
        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<Reserva> Reservas => Set<Reserva>();

        public DbSet<InstalacionDeportiva> InstalacionesDeportivas => Set<InstalacionDeportiva>();


        //Mediante OnModelCreating() configuramos los nombres de las tablas, las restricciones y las relaciones.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Instalación Deportiva
            modelBuilder.Entity<InstalacionDeportiva>(entity =>
            {
                
                entity.ToTable("instalaciones-deportivas");

                //para crear la clave primaria usaré HasKey()
                entity.HasKey(i => i.Id);
                /*
                    para los demás campos se usa Property() donde HasMaxLength() será el máximo de carácteres
                    y IsRequired() hará que sea un campor requerido
                */
                entity.Property(i => i.Nombre).HasMaxLength(100).IsRequired();
                /*
                    HasConversion() hara que el tipo de dato Enum sea pasado a string
                */
                entity.Property(i => i.TipoDeporte).HasConversion<string>().HasMaxLength(30).IsRequired();
                entity.Property(i => i.PrecioPorHora).HasPrecision(10, 2);
            });
            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.NombreCompleto).HasMaxLength(150).IsRequired();
                entity.Property(c => c.CorreoElectronico).HasMaxLength(100).IsRequired();
                entity.HasIndex(c => c.CorreoElectronico).IsUnique(); // Requisito explícito
                entity.Property(c => c.Telefono).HasMaxLength(20).IsRequired();
            });


            // Configuración de Reserva 
            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("reservas");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.EstadoReserva).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.Property(r => r.ImporteTotal).HasPrecision(10, 2);

                // Relación Cliente - Reserva (1:N) con Restricción de Borrado
                entity.HasOne(r => r.Cliente)
                      .WithMany(c => c.Reservas)
                      .HasForeignKey(r => r.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                /* 
                 - Relación Instalación - Reserva (1:N) con Restricción de Borrado
                 - HasOne y WithMay hará que la relación sea de uno a varios
                 - HasForeignKey hará que la clave foranea sea InstalacionId
                 - OnDelete(DeleteBehavior.Restrict); hace que se eliminen duplicados
                 */ 
                entity.HasOne(r => r.Instalacion)
                      .WithMany(i => i.Reservas)
                      .HasForeignKey(r => r.InstalacionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
