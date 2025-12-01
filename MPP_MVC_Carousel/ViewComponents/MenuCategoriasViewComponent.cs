using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPP_MVC_Carousel.Data;
using System.Threading.Tasks;

namespace MPP_MVC_Carousel.ViewComponents
{
    public class MenuCategoriasViewComponent : ViewComponent
    {
        private readonly PessoasDataContext _context;

        public MenuCategoriasViewComponent(PessoasDataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Pega todas as categorias ordenadas pelo nome
            var categorias = await _context.Categorias.OrderBy(x => x.Nome).ToListAsync();
            return View(categorias);
        }
    }
}
