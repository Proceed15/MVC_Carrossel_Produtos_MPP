using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Data;
using MPP_MVC_Carousel.Models;

namespace MPP_MVC_Carousel.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly PessoasDataContext _context;
        private readonly string _filePath;

        public ProdutoController(PessoasDataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _filePath = env.WebRootPath;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            var produtos = _context.Produtos.Include(p => p.Categoria);
            return View(await produtos.ToListAsync());
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,CategoriaId")] ProdutoModel produto, IFormFile ImagemUpload)
        {
            if (ImagemUpload == null || ImagemUpload.Length == 0)
            {
                ModelState.AddModelError("Imagem", "A imagem do produto é obrigatória.");
            }
            else if (!ValidaImagem(ImagemUpload))
            {
                ModelState.AddModelError("Imagem", "Formato inválido (use JPG, PNG, GIF).");
            }
            else
            {
                ModelState.Remove("Imagem");
            }

            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                try
                {
                    produto.Imagem = await SalvarArquivo(ImagemUpload);
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao salvar: " + ex.Message);
                }
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // ---------------------------------------------------------
        // CORREÇÃO AQUI: Método PorCategoria separado e completo
        // ---------------------------------------------------------
        public async Task<IActionResult> PorCategoria(int id)
        {
            // 1. Busca a categoria pelo ID
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            // 2. Preenche os dados para a View
            ViewBag.CategoriaNome = categoria.Nome;
            ViewBag.CategoriaDescricao = categoria.Descricao;

            // 3. Busca os produtos dessa categoria
            var produtos = await _context.Produtos
                .Where(p => p.CategoriaId == id)
                .Include(p => p.Categoria)
                .ToListAsync();

            // 4. Retorna a View Index com a lista filtrada
            return View("Index", produtos);
        }

        // ---------------------------------------------------------
        // CORREÇÃO AQUI: Método Details separado e limpo
        // ---------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // Funções auxiliares
        public bool ValidaImagem(IFormFile imagem)
        {
            string[] permitidos = { "image/jpeg", "image/bmp", "image/gif", "image/png" };
            return permitidos.Contains(imagem.ContentType);
        }

        public async Task<string> SalvarArquivo(IFormFile imagem)
        {
            var nome = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
            var pastaFotos = Path.Combine(_filePath, "produtos");

            if (!Directory.Exists(pastaFotos)) Directory.CreateDirectory(pastaFotos);

            var caminhoCompleto = Path.Combine(pastaFotos, nome);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }
            return nome;
        }
        // ---------------------------------------------------------
        // COLE ISSO DENTRO DO SEU PRODUTOCONTROLLER
        // ---------------------------------------------------------

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            // Carrega as categorias para o Dropdown
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            
            return View(produto);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Imagem,CategoriaId")] ProdutoModel produto, IFormFile ImagemUpload)
        {
            if (id != produto.Id) return NotFound();

            // LOGICA DA IMAGEM NO EDIT:
            // 1. Se o usuário enviou uma nova imagem (ImagemUpload != null)
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                if (ValidaImagem(ImagemUpload))
                {
                    // Salva a nova imagem
                    string novaFoto = await SalvarArquivo(ImagemUpload);
                    
                    // (Opcional) Poderíamos apagar a antiga aqui se quiséssemos
                    // DeletaArquivoFisico(produto.Imagem); 

                    // Atualiza o nome no objeto
                    produto.Imagem = novaFoto;
                }
                else
                {
                    ModelState.AddModelError("Imagem", "Formato inválido.");
                }
            }
            // 2. Se NÃO enviou imagem nova, ele mantém a string 'produto.Imagem' 
            // que veio via <input type="hidden"> da View. Nada a fazer.

            // Remove validações desnecessárias para o ModelState ficar válido
            ModelState.Remove("ImagemUpload"); 
            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Produtos.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null) return NotFound();

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            // 1. Apaga do Banco
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            // 2. Apaga a foto da pasta (Limpeza)
            if (!string.IsNullOrEmpty(produto.Imagem))
            {
                var caminhoArquivo = Path.Combine(_filePath, "produtos", produto.Imagem);
                if (System.IO.File.Exists(caminhoArquivo))
                {
                    System.IO.File.Delete(caminhoArquivo);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        // Se você precisar do Edit e Delete, eles entrariam aqui abaixo...
    }
}





