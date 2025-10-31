using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repositorio.Data
{
    public class EstoqueContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SistemaEstoqueInteligente;Trusted_Connection=True;");
        }
    }
}
