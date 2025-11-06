using System;
using System.Linq;
using System.Windows;
using BarbeariaApp.Data;
using BarbeariaApp.Models;

namespace BarbeariaApp
{
    public partial class MainWindow : Window
    {
        private Agendamento _agendamentoSelecionado;

        public MainWindow()
        {
            InitializeComponent();
            CarregarAgendamentos();
        }

        private void CarregarAgendamentos()
        {
            using (var context = new BarbeariaContext())
            {
                dgAgendamentos.ItemsSource = context.Agendamentos.ToList();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Corrigido para campos do layout novo:
            string nome = txtNomeCliente.Text.Trim();

            // Pega texto do ComboBox selecionado
            var servicoItem = cbServico.SelectedItem as System.Windows.Controls.ComboBoxItem;
            string servico = servicoItem != null ? servicoItem.Content?.ToString() ?? "" : "";

            DateTime? data = dpData.SelectedDate;

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(servico) ||
                !data.HasValue)
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new BarbeariaContext())
            {
                if (_agendamentoSelecionado == null)
                {
                    var novoAgendamento = new Agendamento
                    {
                        NomeCliente = nome,
                        Servico = servico,
                        DataHora = data.Value
                    };

                    context.Agendamentos.Add(novoAgendamento);
                    MessageBox.Show("Agendamento salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var agendamentoParaAtualizar = context.Agendamentos.Find(_agendamentoSelecionado.Id);
                    if (agendamentoParaAtualizar != null)
                    {
                        agendamentoParaAtualizar.NomeCliente = nome;
                        agendamentoParaAtualizar.Servico = servico;
                        agendamentoParaAtualizar.DataHora = data.Value;
                    }

                    MessageBox.Show("Agendamento atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                context.SaveChanges();
            }

            CarregarAgendamentos();
            LimparFormulario();
        }

        private void dgAgendamentos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _agendamentoSelecionado = dgAgendamentos.SelectedItem as Agendamento;

            if (_agendamentoSelecionado != null)
            {
                txtNomeCliente.Text = _agendamentoSelecionado.NomeCliente;

                // Seleciona o serviço no ComboBox conforme valor salvo
                foreach (var item in cbServico.Items)
                {
                    if (item is System.Windows.Controls.ComboBoxItem cbo
                        && cbo.Content?.ToString() == _agendamentoSelecionado.Servico)
                    {
                        cbServico.SelectedItem = item;
                        break;
                    }
                }

                dpData.SelectedDate = _agendamentoSelecionado.DataHora;
                btnExcluir.IsEnabled = true;
            }
        }

        private void LimparFormulario()
        {
            txtNomeCliente.Clear();
            cbServico.SelectedIndex = -1;
            dpData.SelectedDate = null;
            _agendamentoSelecionado = null;
            dgAgendamentos.SelectedItem = null;
            btnExcluir.IsEnabled = false;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (_agendamentoSelecionado == null)
            {
                MessageBox.Show("Selecione um agendamento para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show(
                $"Tem certeza que deseja excluir o agendamento de '{_agendamentoSelecionado.NomeCliente}'?",
                "Confirmação de Exclusão",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                using (var context = new BarbeariaContext())
                {
                    var agendamentoParaExcluir = context.Agendamentos.Find(_agendamentoSelecionado.Id);
                    if (agendamentoParaExcluir != null)
                    {
                        context.Agendamentos.Remove(agendamentoParaExcluir);
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Agendamento excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                CarregarAgendamentos();
                LimparFormulario();
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparFormulario();
        }
    }
}
