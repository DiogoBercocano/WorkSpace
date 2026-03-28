// Models/CoffeeContext.cs
using System.Data.Entity;
namespace CoffeeWeb.Models
{
    public class CoffeeContext : DbContext
    {
        public CoffeeContext() : base("name=CoffeeConn") { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}