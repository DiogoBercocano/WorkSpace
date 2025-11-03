using System.Collections.Generic;

namespace Dominio.Models
{
    public class Fornecedor
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Acrescente estes campos para integração com seu formulário:
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        // Se preferir um campo de contato único substitua por isso:
        // public string Contato { get; set; } = string.Empty;

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
