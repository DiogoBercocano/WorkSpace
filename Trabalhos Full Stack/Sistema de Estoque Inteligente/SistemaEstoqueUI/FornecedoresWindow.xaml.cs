using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Dominio.Models;
using Repositorio.Data;

namespace SistemaEstoqueUI
{
    public partial class FornecedoresWindow : Window
    {
        private ObservableCollection<Fornecedor> fornecedores;

        public FornecedoresWindow()
        {
            InitializeComponent();
            CarregarFornecedores();
        }

        private void CarregarFornecedores()
        {
            using (var context = new EstoqueContext())
            {
                fornecedores = new ObservableCollection<Fornecedor>(
                    context.Fornecedores.ToList()
                );
            }
            dataGridFornecedores.ItemsSource = fornecedores;
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e) => Close();

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var form = new FornecedorFormWindow();
            form.Owner = this;
            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    context.Fornecedores.Add(form.Fornecedor);
                    context.SaveChanges();
                }
                CarregarFornecedores();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridFornecedores.SelectedItem as Fornecedor;
            if (selecionado == null) return;

            var form = new FornecedorFormWindow(selecionado);
            form.Owner = this;
            if (form.ShowDialog() == true)
            {
                using (var context = new EstoqueContext())
                {
                    context.Fornecedores.Update(form.Fornecedor);
                    context.SaveChanges();
                }
                CarregarFornecedores();
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = dataGridFornecedores.SelectedItem as Fornecedor;
            if (selecionado == null) return;

            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new EstoqueContext())
                {
                    var fornecedor = context.Fornecedores.FirstOrDefault(f => f.FornecedorId == selecionado.FornecedorId);
                    if (fornecedor != null)
                    {
                        context.Fornecedores.Remove(fornecedor);
                        context.SaveChanges();
                    }
                }
                CarregarFornecedores();
            }
        }
    }
}
