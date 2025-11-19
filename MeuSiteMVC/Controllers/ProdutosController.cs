using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {
            var categoriasComProdutos = DadosEmMemoria.Categorias.Select(c => new
            {
                Categoria = c,
                Produtos = DadosEmMemoria.Produtos.Where(p => p.CategoriaId == c.Id).ToList()
            }).ToList();

            return View(categoriasComProdutos);
        }

        public IActionResult Detalhes(int id)
        {
            var produto = DadosEmMemoria.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }
    }
}