using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class CategoriasWindow : Window
    {
        private ObservableCollection<Categoria> categorias;

        public CategoriasWindow()
        {
            InitializeComponent();
            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            using (var context = new EstoqueContext())
            {
                categorias = new ObservableCollection<Categoria>(
                    context.Categorias.ToList()
                );
            }
            dataGridCategorias.ItemsSource = categorias;
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e) => Close();

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var form = new CategoriaFormWindow();
            form.Owner = this;
            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    context.Categorias.Add(form.Categoria);
                    context.SaveChanges();
                }
                CarregarCategorias();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridCategorias.SelectedItem as Categoria;
            if (selecionado == null) return;

            var form = new CategoriaFormWindow(selecionado);
            form.Owner = this;
            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    context.Categorias.Update(form.Categoria);
                    context.SaveChanges();
                }
                CarregarCategorias();
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridCategorias.SelectedItem as Categoria;
            if (selecionado == null) return;

            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new EstoqueContext())
                {
                    var categoria = context.Categorias.FirstOrDefault(c => c.CategoriaId == selecionado.CategoriaId);
                    if (categoria != null)
                    {
                        context.Categorias.Remove(categoria);
                        context.SaveChanges();
                    }
                }
                CarregarCategorias();
            }
        }
    }
}
