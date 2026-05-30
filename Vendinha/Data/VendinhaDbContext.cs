using Microsoft.EntityFrameworkCore;
using Vendinha.Models;

namespace Vendinha.Data
{
    public class VendinhaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Divida> Dividas { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\DIEGO\source\repos\Vendinha\Vendinha\vendinha.db");
        }
    }
}