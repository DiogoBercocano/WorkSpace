using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Repositorio.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SistemaEstoqueUI
{
    public partial class MovimentacoesWindow : Window
    {
        private ObservableCollection<MovimentacaoEstoque> movimentacoes;

        public MovimentacoesWindow()
        {
            InitializeComponent();
            CarregarMovimentacoes();
        }

        private void CarregarMovimentacoes()
        {
            using (var context = new EstoqueContext())
            {
                movimentacoes = new ObservableCollection<MovimentacaoEstoque>(
                    context.MovimentacoesEstoque
                        .Include(m => m.Produto)
                        .OrderByDescending(m => m.DataMovimentacao)
                        .ToList()
                );
            }
            dataGridMovimentacoes.ItemsSource = movimentacoes;
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
