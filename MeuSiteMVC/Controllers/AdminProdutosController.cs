using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Controllers
{
    public class AdminProdutosController : Controller
    {
        public IActionResult Index()
        {
            var produtos = DadosEmMemoria.Produtos;
            return View(produtos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Id = DadosEmMemoria.Produtos.Count > 0
                    ? DadosEmMemoria.Produtos.Max(p => p.Id) + 1
                    : 1;
                DadosEmMemoria.Produtos.Add(produto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View(produto);
        }

        [HttpPost]
        public ActionResult SaveData(Produto produto)
        {

            if (produto.Nome != null && produto.UploadImage != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(produto.UploadImage.FileName);
                string extension = Path.GetExtension(produto.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                produto.ImagemUrl = fileName;
                produto.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images"),fileName));
                db.Products.Add(produto);
                db.SaveChanges();
            }
            var result = "Successfully Added";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public IActionResult Edit(int id)
        {
            var produto = DadosEmMemoria.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(int id, Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            var existente = DadosEmMemoria.Produtos.FirstOrDefault(p => p.Id == id);
            if (existente == null) return NotFound();

            if (ModelState.IsValid)
            {
                existente.Nome = produto.Nome;
                existente.Descricao = produto.Descricao;
                existente.Preco = produto.Preco;
                existente.CategoriaId = produto.CategoriaId;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View(produto);
        }

        public IActionResult Delete(int id)
        {
            var produto = DadosEmMemoria.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var produto = DadosEmMemoria.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                DadosEmMemoria.Produtos.Remove(produto);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}