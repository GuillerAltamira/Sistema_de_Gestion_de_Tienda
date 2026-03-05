using Microsoft.AspNetCore.Mvc;
using TecnoVenta.Models;
using TecnoVenta.Data;

namespace TecnoVenta.Controllers
{
    public class ProductosController : Controller
    {
        ProductoDAO dao = new ProductoDAO();

        public IActionResult Index()
        {
            var lista = dao.ObtenerProductos();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Producto producto)
        {
            dao.AgregarProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var lista = dao.ObtenerProductos();
            var producto = lista.FirstOrDefault(p => p.Id == id);

            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            dao.ActualizarProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            dao.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}
