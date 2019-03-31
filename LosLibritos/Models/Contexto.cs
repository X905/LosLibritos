namespace LosLibritos.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }

        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Libro> Libro { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>()
                .Property(e => e.NombreGenero)
                .IsUnicode(false);

            modelBuilder.Entity<Genero>()
                .HasMany(e => e.Libro)
                .WithRequired(e => e.Genero)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.CodigoLibro)
                .IsUnicode(false);

            modelBuilder.Entity<Libro>()
                .Property(e => e.Nombre)
                .IsUnicode(false);
        }
    }
}
