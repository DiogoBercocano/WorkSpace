using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SistemaEstoqueUI
{
    public partial class ProdutosWindow : Window
    {
        private ObservableCollection<Produto> produtos;

        public ProdutosWindow()
        {
            InitializeComponent();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            using (var context = new EstoqueContext())
            {
                produtos = new ObservableCollection<Produto>(
                    context.Produtos.Include(p => p.Categoria).ToList()
                );
            }
            dataGridProdutos.ItemsSource = produtos;
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var form = new ProdutoFormWindow();
            form.Owner = this;

            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    context.Produtos.Add(form.Produto);
                    context.SaveChanges();

                    if (form.Produto.QuantidadeEstoque != 0)
                    {
                        var mov = new MovimentacaoEstoque
                        {
                            ProdutoId = form.Produto.ProdutoId,
                            DataMovimentacao = System.DateTime.Now,
                            Quantidade = form.Produto.QuantidadeEstoque,
                            TipoOperacao = form.Produto.QuantidadeEstoque > 0 ? "Entrada" : "Saída"
                        };
                        context.MovimentacoesEstoque.Add(mov);
                        context.SaveChanges();
                    }
                }
                CarregarProdutos();
                dataGridProdutos.SelectedItem = produtos.Last();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridProdutos.SelectedItem as Produto;
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um produto para editar.");
                return;
            }

            // Cria uma cópia do produto selecionado para editar, para evitar problemas de tracking
            Produto produtoEdicao;
            using (var context = new EstoqueContext())
            {
                produtoEdicao = context.Produtos
                    .AsNoTracking()
                    .FirstOrDefault(p => p.ProdutoId == selecionado.ProdutoId);
            }

            var form = new ProdutoFormWindow(produtoEdicao);
            form.Owner = this;

            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    // Busca o produto de novo para garantir está com tracking
                    var produtoOriginal = context.Produtos
                        .FirstOrDefault(p => p.ProdutoId == form.Produto.ProdutoId);

                    if (produtoOriginal != null)
                    {
                        // Atualize campo a campo!
                        produtoOriginal.Nome = form.Produto.Nome;
                        produtoOriginal.QuantidadeEstoque = form.Produto.QuantidadeEstoque;
                        produtoOriginal.Preco = form.Produto.Preco;
                        produtoOriginal.CategoriaId = form.Produto.CategoriaId;
                        produtoOriginal.FornecedorId = form.Produto.FornecedorId;

                        context.SaveChanges();
                    }
                }
                CarregarProdutos();
                dataGridProdutos.SelectedItem = produtos.FirstOrDefault(p => p.ProdutoId == selecionado.ProdutoId);
            }
        }


        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridProdutos.SelectedItem as Produto;
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um produto para excluir.");
                return;
            }

            if (MessageBox.Show("Confirma exclusão deste produto?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new EstoqueContext())
                {
                    var prod = context.Produtos.FirstOrDefault(p => p.ProdutoId == selecionado.ProdutoId);
                    if (prod != null)
                    {
                        context.Produtos.Remove(prod);
                        context.SaveChanges();
                    }
                }
                CarregarProdutos();
            }
        }
    }
}
