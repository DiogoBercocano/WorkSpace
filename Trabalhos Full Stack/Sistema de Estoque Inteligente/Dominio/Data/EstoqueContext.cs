using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repositorio.Data
{
    public class EstoqueContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SISTEMA_ESTOQUE;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed: cria o usuário admin com senha 1234
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nome = "admin", Senha = "1234" }
            );
        }
    }
}
