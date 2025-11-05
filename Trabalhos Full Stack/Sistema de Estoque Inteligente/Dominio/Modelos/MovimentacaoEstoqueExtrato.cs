using System;

public class MovimentacaoEstoqueExtrato
{
    public int Id { get; set; }
    public string Produto { get; set; }
    public string Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public int SaldoAposMovimentacao { get; set; }
    public bool NecessitaReposicao { get; set; }
}
