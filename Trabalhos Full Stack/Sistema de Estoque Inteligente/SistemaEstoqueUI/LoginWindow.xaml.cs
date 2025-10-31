using Repositorio.Data;
using Dominio.Models;
using System.Linq;
using System.Windows;


namespace SistemaEstoqueUI
{
    public partial class LoginWindow : Window
    {
        EstoqueContext _contexto = new EstoqueContext();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Password;

            var usuarioEncontrado = _contexto.Usuarios
                .FirstOrDefault(u => u.Nome == usuario && u.Senha == senha);

            if (usuarioEncontrado != null)
            {
                MessageBox.Show("Login efetuado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
