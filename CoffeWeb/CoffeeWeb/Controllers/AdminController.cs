using System.Linq;
using System.Web.Mvc;
using CoffeeWeb.Models;
namespace CoffeeWeb.Controllers
{
    public class AdminController : Controller
    {
        private CoffeeContext db = new CoffeeContext();
        // GET: /Admin/Login
        public ActionResult Login()
        {
            // Se já estiver logado, manda pro Dashboard direto
            if (Session["UsuarioLogado"] != null)
                return RedirectToAction("Dashboard");
            return View();
        }
        // POST: /Admin/Login (Recebe os dados do formulário)
        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            // Busca no banco um usuário com o mesmo e-mail e senha
            var user = db.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
            if (user != null)
            {
                // Deu certo! Cria a sessão com o nome do usuário.
                Session["UsuarioLogado"] = user.Nome;
                return RedirectToAction("Dashboard");
            }
            // Deu errado! Manda mensagem de erro para a tela.
            ViewBag.Erro = "E-mail ou senha inválidos!";
            return View();
        }
        // GET: /Admin/Dashboard
        public ActionResult Dashboard()
        {
            // PROTEÇÃO DA ROTA: Se a sessão for nula, expulsa para o Login
            if (Session["UsuarioLogado"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        // GET: /Admin/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Limpa todas as sessões
            return RedirectToAction("Login");
        }
    }
}