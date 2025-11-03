using Dominio.Models;
using System.Windows;

namespace SistemaEstoqueUI
{
    public partial class CategoriaFormWindow : Window
    {
        public Categoria Categoria { get; private set; }

        public CategoriaFormWindow()
        {
            InitializeComponent();
            Categoria = new Dominio.Models.Categoria();
        }

        public CategoriaFormWindow(Dominio.Models.Categoria categoria) : this()
        {
            Categoria = categoria;
            txtNome.Text = categoria.Nome;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe um nome válido para a categoria.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNome.Focus();
                return;
            }
            Categoria.Nome = txtNome.Text.Trim();
            DialogResult = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
