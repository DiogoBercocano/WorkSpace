// MainWindow.xaml.cs
using EstoquePerfumes.Models;
using System.Windows;

namespace EstoquePerfumes
{
    public partial class MainWindow : Window
    {
        private Usuario? usuarioEncontrado;

        // Construtor que recebe o model Usuario (usado no login)
        public MainWindow(Usuario usuarioEncontrado)
        {
            InitializeComponent();
            this.usuarioEncontrado = usuarioEncontrado;
            txtSaudacao.Text = $"Olá, {usuarioEncontrado.Nome}!"; // Mostra o nome
        }
        // Construtor padrão
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnListarPerfumes_Click(object sender, RoutedEventArgs e)
        {
            // Abre a janela da lista de perfumes
            var janelaPerfumes = new ListarPerfumesWindow();
            janelaPerfumes.Show();
        }
        private void btnCadastrarPerfume_Click(object sender, RoutedEventArgs e)
        {
            // Abre a janela de cadastro de novo perfume
            var janelaCadastrar = new CadastrarPerfumeWindow();
            janelaCadastrar.Show();
        }
        private void btnListarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            var janelaListarUsuarios = new ListarUsuariosWindow();
            janelaListarUsuarios.Show();
        }

        private void btnLogoff_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
