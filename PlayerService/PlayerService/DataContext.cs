using Microsoft.EntityFrameworkCore;
using PlayerService.Servicos;

namespace PlayerService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Jogador> Jogadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>().HasKey(p => p.Id);
            modelBuilder.Entity<Jogador>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Jogador>().Property(p => p.Sobrenome).IsRequired();
            modelBuilder.Entity<Jogador>().Property(p => p.Nacionalidade).IsRequired();
            modelBuilder.Entity<Jogador>().Property(p => p.Altura).HasColumnType("smallint");

            base.OnModelCreating(modelBuilder);
        }
    }
}
