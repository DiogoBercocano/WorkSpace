using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;  // Ajuste conforme o namespace do seu contexto

namespace SistemaEstoqueUI
{
    public partial class ProdutoFormWindow : Window
    {
        public Produto Produto { get; private set; }

        public ProdutoFormWindow(Produto produto = null)
        {
            InitializeComponent();

            // Carregar categorias do banco e popular o ComboBox
            using (var context = new EstoqueContext())
            {
                var categorias = context.Categorias.ToList();
                cmbCategoria.ItemsSource = categorias;
            }

            using (var context = new EstoqueContext())
            {
                var fornecedores = context.Fornecedores.ToList();
                cmbFornecedor.ItemsSource = fornecedores;
            }


            if (produto != null)
            {
                Produto = produto;
                txtNome.Text = Produto.Nome;
                cmbCategoria.SelectedItem = Produto.Categoria;
                txtQuantidade.Text = Produto.QuantidadeEstoque.ToString();
                txtPreco.Text = Produto.Preco.ToString("F2");
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

            if (cmbCategoria.SelectedItem == null)
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

            if (cmbFornecedor.SelectedItem == null)
            {
                MessageBox.Show("Selecione um fornecedor.");
                return;
            }
            Produto.FornecedorId = ((Fornecedor)cmbFornecedor.SelectedItem).FornecedorId;

            Produto.Nome = txtNome.Text;
            Produto.QuantidadeEstoque = int.Parse(txtQuantidade.Text);
            Produto.Preco = decimal.Parse(txtPreco.Text);
            Produto.CategoriaId = ((Categoria)cmbCategoria.SelectedItem).CategoriaId;
            Produto.FornecedorId = ((Fornecedor)cmbFornecedor.SelectedItem).FornecedorId;

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
