using Microsoft.EntityFrameworkCore;
using TransferService.Servicos;

namespace ContractService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Transferencia> Transferencias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transferencia>().HasKey(p => p.Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}
