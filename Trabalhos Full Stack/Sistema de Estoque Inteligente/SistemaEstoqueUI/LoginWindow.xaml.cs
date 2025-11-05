using System.Linq;
using System.Windows;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new EstoqueContext())
            {
                string usuario = txtUsuario.Text.Trim();
                string senha = txtSenha.Password.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                {
                    MessageBox.Show("Preencha todos os campos!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = context.Usuarios.FirstOrDefault(u => u.Nome == usuario && u.Senha == senha);

                if (user != null)
                {
                    MessageBox.Show($"Bem-vindo, {user.Nome}!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    var main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var cadastro = new CadastroWindow();
            cadastro.Show();
            this.Close();
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
