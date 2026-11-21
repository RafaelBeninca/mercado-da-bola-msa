using Microsoft.EntityFrameworkCore;
using TeamService.Servicos;

namespace TeamService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Clube> Clubes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clube>().HasKey(c => c.Id);
            modelBuilder.Entity<Clube>().Property(c => c.Nome).IsRequired();
            modelBuilder.Entity<Clube>().Property(c => c.Nacionalidade).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
