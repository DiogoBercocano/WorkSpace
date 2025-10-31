using System.Collections.Generic;

namespace Dominio.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
