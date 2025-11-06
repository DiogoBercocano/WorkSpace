using System;

namespace BarbeariaApp.Models
{
    public class Agendamento
    {
        public int Id { get; set; } // Chave primária
        public string NomeCliente { get; set; } = string.Empty;   // Solução CS8618
        public string Servico { get; set; } = string.Empty;       // Solução CS8618
        public DateTime DataHora { get; set; } = DateTime.Now;    // Solução CS8618
    }
}
