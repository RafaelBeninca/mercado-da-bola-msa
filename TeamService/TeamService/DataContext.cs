using Microsoft.EntityFrameworkCore;
using TeamService.Servicos;

namespace TeamService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Time>().HasKey(t => t.Id);
            modelBuilder.Entity<Time>().Property(t => t.Nome).IsRequired();
            modelBuilder.Entity<Time>().Property(t => t.Cidade).IsRequired();
            modelBuilder.Entity<Time>().Property(t => t.Pais).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
