using System.Linq;
using System.Web.Mvc;
using ATLAS_ERP.Data;

namespace ATLAS_ERP.Controllers
{
    public class ClienteController : Controller
    {
        private AtlasContext db = new AtlasContext();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Login", "Auth");

            var clientes = db.Clientes.ToList();
            return View(clientes);
        }
    }
}