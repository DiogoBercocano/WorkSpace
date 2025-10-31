using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models
{
    public class MovimentacaoEstoque
    {
        public int MovimentacaoEstoqueId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; } = null!;

        [Required]
        public DateTime DataMovimentacao { get; set; }

        [Required]
        public int Quantidade { get; set; } // positivo = entrada; negativo = saída

        [Required]
        [StringLength(50)]
        public string TipoOperacao { get; set; } = string.Empty; // "Entrada" ou "Saída"
    }
}
