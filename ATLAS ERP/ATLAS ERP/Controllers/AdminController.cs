using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ATLAS_ERP.Data;
using ATLAS_ERP.Filters;

namespace ATLAS_ERP.Controllers
{
    public class AdminController : Controller
    {
        private readonly AtlasContext db = new AtlasContext();

        // SOMENTE ADMIN E GERENTE
        [RoleFilter("Admin", "Gerente")]
        public ActionResult Dashboard()
        {
            var hoje = DateTime.Today;

            //  Vendas hoje
            var vendasHoje = db.Vendas
                .Where(v => v.DataVenda >= hoje)
                .Sum(v => (decimal?)v.Total) ?? 0;

            //  Total produtos
            var totalProdutos = db.Produtos.Count();

            //  Total clientes
            var totalClientes = db.Clientes.Count();

            //  Total vendas do dia
            var totalVendasDia = db.Vendas
                .Where(v => v.DataVenda >= hoje)
                .Count();

            //  Últimas vendas
            var ultimasVendas = db.Vendas
                .Include(v => v.Cliente)
                .OrderByDescending(v => v.DataVenda)
                .Take(10)
                .ToList();

            // ViewBag (mantendo simples por enquanto)
            ViewBag.VendasHoje = vendasHoje;
            ViewBag.TotalProdutos = totalProdutos;
            ViewBag.TotalClientes = totalClientes;
            ViewBag.TotalVendasDia = totalVendasDia;
            ViewBag.UltimasVendas = ultimasVendas;

            return View();
        }
    }
}