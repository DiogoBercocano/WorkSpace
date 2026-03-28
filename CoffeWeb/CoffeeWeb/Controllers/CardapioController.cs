// Controllers/CardapioController.cs
using System.Linq;
using System.Web.Mvc;
using CoffeeWeb.Models;
namespace CoffeeWeb.Controllers
{
    public class CardapioController : Controller
    {
        private CoffeeContext db = new CoffeeContext();
        public ActionResult Index()
        {
            var cafes = db.Produtos.ToList();
            return View(cafes);
        }
        public ActionResult Detalhes(int id)
        {
            var cafe = db.Produtos.Find(id);
            if (cafe == null)
            {
                return HttpNotFound(); // Retorna erro 404 se não achar o café
            }
            return View(cafe);
        }
    }
}