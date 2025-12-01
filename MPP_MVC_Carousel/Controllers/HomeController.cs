using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Importante para o Include
using MPP_MVC_Carousel.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MPP_MVC_Carousel.Controllers
{
    public class HomeController : Controller
    {
        private readonly PessoasDataContext _context;

        public HomeController(PessoasDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Busca Categorias e já carrega os Produtos delas (Eager Loading)
            // Filtramos apenas categorias que tenham pelo menos 1 produto
            var vitrine = await _context.Categorias
                .Include(c => c.Produtos)
                .Where(c => c.Produtos.Any())
                .ToListAsync();

            return View(vitrine);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}