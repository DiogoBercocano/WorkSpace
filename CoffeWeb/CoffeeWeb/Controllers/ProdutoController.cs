using System.Linq;
using System.Web.Mvc;
using CoffeeWeb.Models;
namespace CoffeeWeb.Controllers
{
    public class ProdutoController : Controller
    {
        private CoffeeContext db = new CoffeeContext();
        public ActionResult Index(string busca)
        {
            if (Session["UsuarioLogado"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            var produtos = db.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(busca))
            {
                produtos = produtos.Where(p => p.Nome.Contains(busca) || p.Descricao.Contains(busca));
                ViewBag.BuscaAtual = busca;
            }
            return View(produtos.OrderBy(p => p.Nome).ToList());
        }
        public ActionResult Create()
        {
            // Proteção: Só entra se estiver logado
            if (Session["UsuarioLogado"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        // ==========================================
        // POST: /Produto/Create (Recebe os dados do formulário)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        // NOVO PARÂMETRO: recebe o arquivo que o usuário escolheu
        public ActionResult Create(Produto produto, System.Web.HttpPostedFileBase fotoUpload)
        {
            if (Session["UsuarioLogado"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (ModelState.IsValid)
            {
                // ==========================================
                // LÓGICA DE UPLOAD DA IMAGEM
                // ==========================================
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    // 1. Pega o nome original do arquivo (ex: "meucafe.jpg")
                    string nomeArquivo = System.IO.Path.GetFileName(fotoUpload.FileName);
                    // 2. Monta o caminho completo onde será salvo no servidor (na pasta Content/Imagens)
                    string caminhoServidor = System.IO.Path.Combine(Server.MapPath("~/Content/images"), nomeArquivo);
                    // 3. Salva o arquivo fisicamente na pasta
                    fotoUpload.SaveAs(caminhoServidor);
                    // 4. Guarda o caminho relativo no banco de dados para a View poder exibir depois
                    produto.ImagemUrl = "~/Content/images/" + nomeArquivo;
                }
                else
                {
                    // Se o usuário não mandou foto, colocamos uma imagem padrão
                    produto.ImagemUrl = "~/Content/images/default.jpeg";
                }
                // Salva no banco de dados
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }
            // ==========================================
            // GET: /Produto/Edit/5 (Abre a tela preenchida)
            // ==========================================
        public ActionResult Edit(int? id)
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Login", "Admin");

            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Produto produto = db.Produtos.Find(id);

            if (produto == null)
                return HttpNotFound();

            // Reaproveita a View Create
            return View("Create", produto);
        }


        // ==========================================
        // POST: /Produto/Edit/5 (Recebe os dados atualizados)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto, System.Web.HttpPostedFileBase fotoUpload)
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Login", "Admin");

            ModelState.Remove("ImagemUrl");

            if (ModelState.IsValid)
            {
                var produtoOriginal = db.Produtos.Find(produto.Id);

                if (produtoOriginal == null)
                    return HttpNotFound();

                // Atualiza os dados
                produtoOriginal.Nome = produto.Nome;
                produtoOriginal.Preco = produto.Preco;
                produtoOriginal.Descricao = produto.Descricao;

                // Se enviou nova imagem
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    string extensao = System.IO.Path.GetExtension(fotoUpload.FileName);
                    string nomeArquivo = System.Guid.NewGuid() + extensao;

                    string pasta = Server.MapPath("~/Content/images");

                    if (!System.IO.Directory.Exists(pasta))
                    {
                        System.IO.Directory.CreateDirectory(pasta);
                    }

                    string caminhoServidor = System.IO.Path.Combine(pasta, nomeArquivo);

                    fotoUpload.SaveAs(caminhoServidor);

                    produtoOriginal.ImagemUrl = "~/Content/images/" + nomeArquivo;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Create", produto);
        }

        // ==========================================
        // GET: /Produto/Delete/5 (Tela de Confirmação)
        // ==========================================
        public ActionResult Delete(int? id)
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Login", "Admin");
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            // Busca o café para mostrar na tela de confirmação
            Produto produto = db.Produtos.Find(id);
            if (produto == null) return HttpNotFound();
            return View(produto);
        }
        // ==========================================
        // POST: /Produto/Delete/5 (Executa a Exclusão)
        // ==========================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Login", "Admin");
            // 1. Encontra o produto no banco
            Produto produto = db.Produtos.Find(id);
            // 2. (OPCIONAL/BÔNUS) Apaga a imagem física da pasta para não acumular lixo
            if (!string.IsNullOrEmpty(produto.ImagemUrl) && produto.ImagemUrl != "~/Content/images/default.jpeg")
            {
                string caminhoFisico = Server.MapPath(produto.ImagemUrl);
                if (System.IO.File.Exists(caminhoFisico))
                {
                    System.IO.File.Delete(caminhoFisico);
                }
            }
            // 3. Remove do banco e salva
            db.Produtos.Remove(produto);
            db.SaveChanges();
            // 4. Volta para a lista
            return RedirectToAction("Index");
        }
    }
}