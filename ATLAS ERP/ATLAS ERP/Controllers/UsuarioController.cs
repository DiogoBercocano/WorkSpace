using System.Linq;
using System.Web.Mvc;
using ATLAS_ERP.Data;
using ATLAS_ERP.Models;
using ATLAS_ERP.Filters;

namespace ATLAS_ERP.Controllers
{
    [RoleFilter("Admin")]
    public class UsuarioController : Controller
    {
        private AtlasContext db = new AtlasContext();

        // LISTAR
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.ToList();
            return View(usuarios);
        }

        // TELA DE CRIAR
        public ActionResult Create()
        {
            return View();
        }

        // SALVAR
        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            if (ModelState.IsValid)
            {
                user.Ativo = true;

                user.EmpresaId = (int)Session["EmpresaId"];

                db.Usuarios.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}