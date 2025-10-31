using Dominio.Models;
using Repositorio.Data;
using Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio.Classes
{
    public class ProdutoRepositorio : IRepositorioProduto
    {
        private readonly EstoqueContext _contexto;

        public ProdutoRepositorio(EstoqueContext contexto)
        {
            _contexto = contexto;
        }

        public void Inserir(Produto entidade)
        {
            entidade.Validar();
            _contexto.Produtos.Add(entidade);
            _contexto.SaveChanges();
        }

        public void Alterar(Produto entidade)
        {
            entidade.Validar();
            _contexto.Produtos.Update(entidade);
            _contexto.SaveChanges();
        }

        public void Excluir(Produto entidade)
        {
            _contexto.Produtos.Remove(entidade);
            _contexto.SaveChanges();
        }

        public List<Produto> Listar(Expression<Func<Produto, bool>> expressao)
        {
            return _contexto.Produtos.Where(expressao).ToList();
        }

        public List<Produto> ListarTodos()
        {
            return _contexto.Produtos.ToList();
        }

        public Produto Recuperar(Expression<Func<Produto, bool>> expressao)
        {
            return _contexto.Produtos.FirstOrDefault(expressao);
        }
    }
}
