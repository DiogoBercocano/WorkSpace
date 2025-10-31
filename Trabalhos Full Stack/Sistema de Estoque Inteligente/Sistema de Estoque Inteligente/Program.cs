using Microsoft.Extensions.DependencyInjection;
using Repositorio.Data;
using Repositorio.Interfaces;
using Repositorio.Classes;
using Dominio.Models;

namespace SistemaEstoqueInteligente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<EstoqueContext>();
            services.AddScoped<IRepositorioProduto, ProdutoRepositorio>();

            var serviceProvider = services.BuildServiceProvider();

            var repositorio = serviceProvider.GetRequiredService<IRepositorioProduto>();

            Console.WriteLine("=== SISTEMA DE ESTOQUE ===");
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Preço inválido!");
                return;
            }

            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida!");
                return;
            }

            Console.Write("ID da categoria: ");
            if (!int.TryParse(Console.ReadLine(), out int categoriaId))
            {
                Console.WriteLine("ID de categoria inválido!");
                return;
            }

            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int fornecedorId))
            {
                Console.WriteLine("ID de fornecedor inválido!");
                return;
            }

            var produto = new Produto()
            {
                Nome = nome,
                Preco = preco,
                QuantidadeEstoque = quantidade,
                CategoriaId = categoriaId,
                FornecedorId = fornecedorId
            };

            repositorio.Inserir(produto);

            Console.WriteLine("✅ Produto inserido com sucesso!");
        }
    }
}
