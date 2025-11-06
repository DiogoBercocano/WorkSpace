using EstoquePerfumes.Data;
using System.Linq;
using System.Windows;

namespace EstoquePerfumes
{
    public partial class ListarPerfumesWindow : Window
    {
        public ListarPerfumesWindow()
        {
            InitializeComponent();
            CarregarPerfumes();
        }

        private void CarregarPerfumes()
        {
            using var dbContext = new AppDbContext();
            dgPerfumes.ItemsSource = dbContext.Perfumes.ToList();
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
