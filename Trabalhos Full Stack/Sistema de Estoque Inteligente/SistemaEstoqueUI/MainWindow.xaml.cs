using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            var produtosWindow = new ProdutosWindow();
            produtosWindow.Owner = this;
            produtosWindow.ShowDialog();
        }

        private void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            var categoriasWindow = new CategoriasWindow();
            categoriasWindow.Owner = this;
            categoriasWindow.ShowDialog();
        }

        private void BtnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            var fornecedoresWindow = new FornecedoresWindow();
            fornecedoresWindow.Owner = this;
            fornecedoresWindow.ShowDialog();
        }


        private void BtnMovimentacoes_Click(object sender, RoutedEventArgs e)
        {
            var window = new MovimentacoesWindow();
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
