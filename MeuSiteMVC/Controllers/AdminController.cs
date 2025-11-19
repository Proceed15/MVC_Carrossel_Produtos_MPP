using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CadastrarProduto()
        {
            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Id = DadosEmMemoria.Produtos.Count > 0 ? DadosEmMemoria.Produtos.Max(p => p.Id) + 1 : 1;
                DadosEmMemoria.Produtos.Add(produto);
                return RedirectToAction("Index", "Produtos");
            }
            ViewBag.Categorias = DadosEmMemoria.Categorias;
            return View(produto);
        }

        public IActionResult CadastrarCategoria()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.Id = DadosEmMemoria.Categorias.Count > 0 ? DadosEmMemoria.Categorias.Max(c => c.Id) + 1 : 1;
                DadosEmMemoria.Categorias.Add(categoria);
                return RedirectToAction("CadastrarProduto");
            }
            return View(categoria);
        }
    }
}