using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class CadastroWindow : Window
    {
        public CadastroWindow()
        {
            InitializeComponent();
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new EstoqueContext())
            {
                string nome = txtNome.Text.Trim();
                string senha = txtSenha.Password.Trim();

                if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
                {
                    MessageBox.Show("Preencha todos os campos!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Evita duplicação
                if (context.Usuarios.Any(u => u.Nome == nome))
                {
                    MessageBox.Show("Usuário já existe!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var novoUsuario = new Usuario
                {
                    Nome = nome,
                    Senha = senha
                };

                context.Usuarios.Add(novoUsuario);
                context.SaveChanges();

                MessageBox.Show("Usuário cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);

                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
