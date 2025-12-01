using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Data;
using System.Threading.Tasks;

namespace MPP_MVC_Carousel.Controllers
{
    public class AdminController : Controller
    {
        private readonly PessoasDataContext _context;

        public AdminController(PessoasDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Busca dados para os cards do dashboard
            ViewBag.TotalProdutos = await _context.Produtos.CountAsync();
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();
            ViewBag.TotalPessoas = await _context.PessoaModel.CountAsync();
            
            // Define que esta View usará o layout de Admin
            return View();
        }
    }
}

