using System.Linq;
using System.Web.Mvc;
using ATLAS_ERP.Data;
using ATLAS_ERP.Filters;

namespace ATLAS_ERP.Controllers
{
    public class ProdutoController : Controller
    {
        private AtlasContext db = new AtlasContext();

        // QUALQUER USUÁRIO LOGADO
        public ActionResult Index()
        {
            // 🔐 PROTEÇÃO DE LOGIN
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Login", "Auth");

            var produtos = db.Produtos.ToList();
            return View(produtos);
        }

        // ADMIN E GERENTE
        [RoleFilter("Admin", "Gerente")]
        public ActionResult Create()
        {
            return View();
        }

        // SOMENTE ADMIN
        [RoleFilter("Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}