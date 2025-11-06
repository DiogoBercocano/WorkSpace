using BarbeariaApp.Models;
using Microsoft.EntityFrameworkCore;
namespace BarbeariaApp.Data
{
    public class BarbeariaContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=BarbeariaDB;Trusted_Connection=True;");
        }
    }
}
