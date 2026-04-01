using System.Linq;
using System.Web.Mvc;
using ATLAS_ERP.Data;

namespace ATLAS_ERP.Controllers
{
    public class AuthController : Controller
    {
        private AtlasContext db = new AtlasContext();

        // GET: /Auth/Login
        public ActionResult Login()
        {
            if (Session["UsuarioLogado"] != null)
            {
                var role = Session["Role"]?.ToString();

                if (role == "Admin" || role == "Gerente")
                    return RedirectToAction("Dashboard", "Admin");

                if (role == "Vendedor")
                    return RedirectToAction("Index", "Produto");
            }

            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            var user = db.Usuarios.FirstOrDefault(u =>
                u.Email == email &&
                u.SenhaHash == senha &&
                u.Ativo == true
            );

            if (user != null)
            {
                Session["UsuarioLogado"] = user.Name;
                Session["UsuarioId"] = user.UsuarioId;
                Session["EmpresaId"] = user.EmpresaId;
                Session["Role"] = user.Role;

                var role = user.Role;

                if (role == "Admin" || role == "Gerente")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                if (role == "Vendedor")
                {
                    return RedirectToAction("Index", "Produto");
                }

                // fallback
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Erro = "E-mail ou senha inválidos!";
            return View();
        }

        // LOGOUT
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}