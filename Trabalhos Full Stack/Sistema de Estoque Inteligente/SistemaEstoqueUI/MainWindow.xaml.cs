using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SistemaEstoqueUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            AbrirModal(new ProdutosWindow());
        }

        private void BtnCategorias_Click(object sender, RoutedEventArgs e)
        {
            AbrirModal(new CategoriasWindow());
        }

        private void BtnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            AbrirModal(new FornecedoresWindow());
        }

        private void BtnMovimentacoes_Click(object sender, RoutedEventArgs e)
        {
            AbrirModal(new MovimentacoesWindow());
        }
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement card &&
                FindResource("CardHoverIn") is Storyboard hoverIn)
            {
                hoverIn.Begin(card);
            }
        }
        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement card &&
                FindResource("CardHoverOut") is Storyboard hoverOut)
            {
                hoverOut.Begin(card);
            }
        }
        private void AbrirModal(Window window)
        {
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}
