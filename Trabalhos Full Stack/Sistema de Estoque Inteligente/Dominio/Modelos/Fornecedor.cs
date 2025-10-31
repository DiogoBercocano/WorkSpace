using System.Collections.Generic;

namespace Dominio.Models
{
    public class Fornecedor
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
