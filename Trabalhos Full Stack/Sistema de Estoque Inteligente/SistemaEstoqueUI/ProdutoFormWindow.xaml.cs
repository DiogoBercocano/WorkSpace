using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class ProdutoFormWindow : Window
    {
        public Produto Produto { get; private set; }

        public ProdutoFormWindow(Produto produto = null)
        {
            InitializeComponent();

            // Carrega categorias e fornecedores
            using (var context = new EstoqueContext())
            {
                var categorias = context.Categorias.ToList();
                cmbCategoria.ItemsSource = categorias;

                var fornecedores = context.Fornecedores.ToList();
                cmbFornecedor.ItemsSource = fornecedores;
            }

            if (produto != null)
            {
                Produto = produto;
                txtNome.Text = Produto.Nome;
                txtQuantidade.Text = Produto.QuantidadeEstoque.ToString();
                txtPreco.Text = Produto.Preco.ToString("F2");
                // Crucial: seleciona pelo ID
                cmbCategoria.SelectedValue = Produto.CategoriaId;
                cmbFornecedor.SelectedValue = Produto.FornecedorId;
            }
            else
            {
                Produto = new Produto();
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha o nome do produto.");
                return;
            }

            if (cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma categoria.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade < 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            if (!decimal.TryParse(txtPreco.Text.Replace(",", "."), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal preco) || preco <= 0)
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            if (cmbFornecedor.SelectedValue == null)
            {
                MessageBox.Show("Selecione um fornecedor.");
                return;
            }

            Produto.Nome = txtNome.Text.Trim();
            Produto.QuantidadeEstoque = quantidade;
            Produto.Preco = preco;

            // Atualiza pelo ID da SelectedValue -- garante que a troca funciona
            Produto.CategoriaId = (int)cmbCategoria.SelectedValue;
            Produto.FornecedorId = (int)cmbFornecedor.SelectedValue;

            DialogResult = true;
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
