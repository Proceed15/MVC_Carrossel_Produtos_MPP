using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MPP_MVC_Carousel.Data;

namespace MPP_MVC_Carousel.Controllers
{
    public class PessoaController : Controller
    {
        private readonly PessoasDataContext _context;
        private string _filePath;
        public PessoaController(PessoasDataContext context, IWebHostEnvironment env)
        {
            _filePath = env.WebRootPath;
            _context = context;
        }

        // GET: Pessoa
        public async Task<IActionResult> Index()
        {
            ViewBag.Path = _filePath;
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Nome")] PessoaModel pessoaModel, IFormFile Imagem)
{
    // 1. Validação Manual do Arquivo
    if (Imagem == null || Imagem.Length == 0)
    {
        ModelState.AddModelError("Foto", "O campo Foto é obrigatório.");
    }
    else if (!ValidaImagem(Imagem))
    {
        ModelState.AddModelError("Foto", "Formato inválido.");
    }
    else
    {
        // TRUQUE DO MESTRE:
        // Se a imagem veio correta, removemos o erro automático que diz que "Foto" é nula.
        // Fazemos isso porque vamos preencher a Foto manualmente logo abaixo.
        ModelState.Remove("Foto");
    }

    // 2. Agora o ModelState.IsValid vai dar True (se o Nome estiver ok)
    if (ModelState.IsValid)
    {
        try
        {
            // Salva o arquivo e pega o nome
            var nomeDoArquivo = await SalvarArquivo(Imagem);
            
            // Atribui o nome ao modelo
            pessoaModel.Foto = nomeDoArquivo; 
    
            _context.Add(pessoaModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Erro ao salvar: " + ex.Message);
        }
    }

        // Se falhar, volta pra View (o arquivo será perdido visualmente, isso é padrão do HTML)
        return View(pessoaModel);
    }
        public bool ValidaImagem(IFormFile Imagem)
        {
            string[] permitidos = { "image/jpeg", "image/bmp", "image/gif", "image/png" };
            return permitidos.Contains(Imagem.ContentType);
        }

        public async Task<string> SalvarArquivo(IFormFile Imagem)
        {
            var nome = Guid.NewGuid().ToString() + Imagem.FileName;
            var filePath = _filePath + "\\fotos";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var caminhoCompleto = Path.Combine(filePath, nome);
    
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                // ✅ Agora usamos 'await' para esperar a conclusão antes de sair do 'using'
                await Imagem.CopyToAsync(stream);
            }

            return nome;
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaModel);
        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            string filePathName = _filePath + "\\fotos\\" + pessoaModel.Foto;

            if (System.IO.File.Exists(filePathName))
                System.IO.File.Delete(filePathName);

            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Id == id);
        }
    }
}