using ContractService.Servicos;
using Microsoft.EntityFrameworkCore;

namespace ContractService
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Contrato> Contratos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contrato>().HasKey(p => p.Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}
