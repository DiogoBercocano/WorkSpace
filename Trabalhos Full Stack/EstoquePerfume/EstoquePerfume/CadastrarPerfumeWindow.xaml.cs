using EstoquePerfumes.Data;
using EstoquePerfumes.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace EstoquePerfumes
{
    public partial class CadastrarPerfumeWindow : Window
    {
        private Perfume? perfumeSelecionado = null;

        public CadastrarPerfumeWindow()
        {
            InitializeComponent();
            CarregarLista();
        }

        private void CarregarLista()
        {
            using var dbContext = new AppDbContext();
            dgPerfumes.ItemsSource = dbContext.Perfumes.ToList();
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtGenero.Clear();
            txtMl.Clear();
            txtValor.Clear();
            perfumeSelecionado = null;
            dgPerfumes.UnselectAll();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text)
                || string.IsNullOrWhiteSpace(txtGenero.Text)
                || string.IsNullOrWhiteSpace(txtMl.Text)
                || string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }

            if (!int.TryParse(txtMl.Text, out int ml))
            {
                MessageBox.Show("Quantidade inválida");
                return;
            }

            if (!decimal.TryParse(txtValor.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal valor))
            {
                MessageBox.Show("Valor inválido");
                return;
            }

            using var dbContext = new AppDbContext();

            if (perfumeSelecionado == null)
            {
                var perfume = new Perfume
                {
                    Nome = txtNome.Text,
                    Genero = txtGenero.Text,
                    Ml = ml,
                    Valor = valor
                };
                dbContext.Perfumes.Add(perfume);
            }
            else
            {
                var perfumeDb = dbContext.Perfumes.Find(perfumeSelecionado.Id);
                if (perfumeDb != null)
                {
                    perfumeDb.Nome = txtNome.Text;
                    perfumeDb.Genero = txtGenero.Text;
                    perfumeDb.Ml = ml;
                    perfumeDb.Valor = valor;
                }
            }
            dbContext.SaveChanges();
            MessageBox.Show("Dados salvos com sucesso!");
            CarregarLista();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (perfumeSelecionado == null)
            {
                MessageBox.Show("Nenhum perfume selecionado para exclusão.");
                return;
            }

            var resultado = MessageBox.Show("Confirma exclusão do perfume selecionado?", "Excluir Perfume", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                using var dbContext = new AppDbContext();
                var perfumeDb = dbContext.Perfumes.Find(perfumeSelecionado.Id);
                if (perfumeDb != null)
                {
                    dbContext.Perfumes.Remove(perfumeDb);
                    dbContext.SaveChanges();
                    MessageBox.Show("Perfume excluído com sucesso.");
                }
                CarregarLista();
            }
        }

        private void dgPerfumes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            perfumeSelecionado = dgPerfumes.SelectedItem as Perfume;
            if (perfumeSelecionado != null)
            {
                txtNome.Text = perfumeSelecionado.Nome;
                txtGenero.Text = perfumeSelecionado.Genero;
                txtMl.Text = perfumeSelecionado.Ml.ToString();
                txtValor.Text = perfumeSelecionado.Valor.ToString("F2");
            }
            else
            {
                LimparCampos();
            }
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
