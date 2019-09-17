using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.RoteiroFilmess.WebApi.Domains
{
    public partial class RoteiroFilmesContext : DbContext
    {
        public RoteiroFilmesContext()
        {
        }

        public RoteiroFilmesContext(DbContextOptions<RoteiroFilmesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Filmes> Filmes { get; set; }
        public virtual DbSet<Generos> Generos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress;Initial Catalog=T_RoteiroFilmes;User Id=sa;Pwd=132;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filmes>(entity =>
            {
                entity.HasKey(e => e.IdFilme);

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Filmes__7B406B563251EB6F")
                    .IsUnique();

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Filmes)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK__Filmes__IdGenero__4D94879B");
            });

            modelBuilder.Entity<Generos>(entity =>
            {
                entity.HasKey(e => e.IdGenero);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Generos__7D8FE3B245BC21C6")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
