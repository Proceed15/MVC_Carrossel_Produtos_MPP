using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Data;
using MPP_MVC_Carousel.Models;

namespace MPP_MVC_Carousel.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly PessoasDataContext _context;

        public CategoriaController(PessoasDataContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // POST: Categoria/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
    {
    // AQUI ESTÁ A CORREÇÃO:
    // Removemos a validação da lista de produtos, pois ela vem vazia na criação
    ModelState.Remove("Produtos");

    if (ModelState.IsValid)
    {
        _context.Add(categoriaModel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    // Se chegou aqui, é porque tem erro. Vamos ver qual é no debug ou na tela.
    return View(categoriaModel);
    }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var categoriaModel = await _context.Categorias.FindAsync(id);
            if (categoriaModel == null) return NotFound();

            return View(categoriaModel);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.Id) return NotFound();
            ModelState.Remove("Produtos");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaModelExists(categoriaModel.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaModel);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var categoriaModel = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);

            if (categoriaModel == null) return NotFound();

            return View(categoriaModel);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 🛡️ SEGURANÇA: Verifica se existem produtos nesta categoria
            bool temProdutos = await _context.Produtos.AnyAsync(p => p.CategoriaId == id);

            if (temProdutos)
            {
                // Se tiver produtos, não deixa apagar e mostra mensagem de erro
                var categoriaParaExibir = await _context.Categorias.FindAsync(id);
                ViewBag.ErroExclusao = "Não é possível excluir esta categoria porque existem produtos vinculados a ela.";
                return View(categoriaParaExibir);
            }

            var categoriaModel = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoriaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaModelExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}





