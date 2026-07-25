using Microsoft.EntityFrameworkCore;
using PracticaProgramada3Grupo7.DAL.Entidades;

namespace PracticaProgramada3Grupo7.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Votante> Votantes { get; set; }

        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }

        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Votante>()
                .HasIndex(votante => votante.Cedula)
                .IsUnique();

            modelBuilder.Entity<PartidoPolitico>()
                .HasIndex(partido => partido.Siglas)
                .IsUnique();

            modelBuilder.Entity<Voto>()
                .HasIndex(voto => voto.VotanteId)
                .IsUnique();

            modelBuilder.Entity<Voto>()
                .HasOne(voto => voto.Votante)
                .WithOne(votante => votante.Voto)
                .HasForeignKey<Voto>(voto => voto.VotanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voto>()
                .HasOne(voto => voto.PartidoPolitico)
                .WithMany(partido => partido.Votos)
                .HasForeignKey(voto => voto.PartidoPoliticoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}