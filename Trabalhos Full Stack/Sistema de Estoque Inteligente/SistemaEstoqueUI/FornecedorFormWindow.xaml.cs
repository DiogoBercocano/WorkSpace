using System.Windows;

namespace SistemaEstoqueUI
{
    public partial class FornecedorFormWindow : Window
    {
        public Dominio.Models.Fornecedor Fornecedor { get; private set; }

        public FornecedorFormWindow()
        {
            InitializeComponent();
            Fornecedor = new Dominio.Models.Fornecedor();
        }

        public FornecedorFormWindow(Dominio.Models.Fornecedor fornecedor) : this()
        {
            Fornecedor = fornecedor;
            txtNome.Text = fornecedor.Nome;
            txtEmail.Text = fornecedor.Email;
            txtTelefone.Text = fornecedor.Telefone;
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do fornecedor.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNome.Focus();
                return;
            }
            Fornecedor.Nome = txtNome.Text.Trim();
            Fornecedor.Email = txtEmail.Text.Trim();
            Fornecedor.Telefone = txtTelefone.Text.Trim();

            DialogResult = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
