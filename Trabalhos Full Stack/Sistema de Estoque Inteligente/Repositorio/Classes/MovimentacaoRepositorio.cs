using Dominio.Models;
using Repositorio.Data;
using Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositorio.Classes
{
    public class MovimentacaoRepositorio : IRepositorioMovimentacao
    {
        private readonly EstoqueContext _contexto;

        public MovimentacaoRepositorio(EstoqueContext contexto)
        {
            _contexto = contexto;
        }

        public void Inserir(MovimentacaoEstoque entidade)
        {
            _contexto.MovimentacoesEstoque.Add(entidade);
            _contexto.SaveChanges();
        }

        public void Alterar(MovimentacaoEstoque entidade)
        {
            _contexto.MovimentacoesEstoque.Update(entidade);
            _contexto.SaveChanges();
        }

        public void Excluir(MovimentacaoEstoque entidade)
        {
            _contexto.MovimentacoesEstoque.Remove(entidade);
            _contexto.SaveChanges();
        }

        public List<MovimentacaoEstoque> Listar(Expression<Func<MovimentacaoEstoque, bool>> expressao)
        {
            return _contexto.MovimentacoesEstoque.Where(expressao).ToList();
        }

        public List<MovimentacaoEstoque> ListarTodos()
        {
            return _contexto.MovimentacoesEstoque.ToList();
        }

        public MovimentacaoEstoque Recuperar(Expression<Func<MovimentacaoEstoque, bool>> expressao)
        {
            return _contexto.MovimentacoesEstoque.FirstOrDefault(expressao)!;
        }
    }
}
