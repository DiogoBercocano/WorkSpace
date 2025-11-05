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
                // Ordenado da movimentação mais antiga pra mais nova (para calcular saldo sequencial)
                var lista = context.MovimentacoesEstoque
                    .Include(m => m.Produto)
                    .OrderBy(m => m.DataMovimentacao)
                    .ToList();

                var extrato = new List<MovimentacaoEstoqueExtrato>();
                var saldos = new Dictionary<int, int>(); // ProdutoId -> saldo

                foreach (var mov in lista)
                {
                    if (!saldos.ContainsKey(mov.ProdutoId))
                    {
                        // saldo inicial = 0 ao inserir o produto.
                        saldos[mov.ProdutoId] = 0;
                    }
                    saldos[mov.ProdutoId] += mov.Quantidade;

                    extrato.Add(new MovimentacaoEstoqueExtrato
                    {
                        Id = mov.MovimentacaoEstoqueId,
                        Produto = mov.Produto.Nome,
                        Tipo = mov.TipoOperacao,
                        Quantidade = mov.Quantidade,
                        Data = mov.DataMovimentacao,
                        SaldoAposMovimentacao = saldos[mov.ProdutoId],
                        NecessitaReposicao = saldos[mov.ProdutoId] <= 1
                    });
                }

                // Exibir do mais novo para o mais antigo (opcional)
                dataGridMovimentacoes.ItemsSource = extrato.OrderByDescending(e => e.Data).ToList();
            }
        }


        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
