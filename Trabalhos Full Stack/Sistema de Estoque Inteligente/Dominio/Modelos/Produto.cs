using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models

{
    public class Produto
    {

        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade não pode ser negativa.")]
        public int QuantidadeEstoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; } = null!;
        public void Validar()
        {
            var contexto = new ValidationContext(this);
            Validator.ValidateObject(this, contexto, true);
        }
    }
}
