using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Controllers
{
    public class AdminPedidosController : Controller
    {
        public IActionResult Index()
        {
            return View(DadosEmMemoria.Pedidos);
        }

        public IActionResult Edit(int id)
        {
            var pedido = DadosEmMemoria.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        [HttpPost]
        public IActionResult Edit(int id, Pedido pedido)
        {
            var existente = DadosEmMemoria.Pedidos.FirstOrDefault(p => p.Id == id);
            if (existente == null) return NotFound();

            if (ModelState.IsValid)
            {
                existente.Status = pedido.Status;
                existente.Cliente = pedido.Cliente;
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public IActionResult Delete(int id)
        {
            var pedido = DadosEmMemoria.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var pedido = DadosEmMemoria.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
                DadosEmMemoria.Pedidos.Remove(pedido);
            return RedirectToAction(nameof(Index));
        }
    }
}