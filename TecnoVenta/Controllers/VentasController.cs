using Microsoft.AspNetCore.Mvc;
using TecnoVenta.Data;
using TecnoVenta.Models;

namespace TecnoVenta.Controllers
{
    public class VentasController : Controller
    {
        VentaDAO ventaDAO = new VentaDAO();
        ClienteDAO clienteDAO = new ClienteDAO();
        ProductoDAO productoDAO = new ProductoDAO();

        public IActionResult Index()
        {
            var lista = ventaDAO.ObtenerVentas();
            return View(lista);
        }

        public IActionResult Crear()
        {
            ViewBag.Clientes = clienteDAO.ObtenerClientes();
            ViewBag.Productos = productoDAO.ObtenerProductos();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Venta venta)
        {
            // ⚡ Calcular total
            var producto = productoDAO.ObtenerProductos().Find(p => p.Id == venta.ProductoId);
            venta.Total = producto.Precio * venta.Cantidad;

            // ⚡ Guardar venta
            ventaDAO.AgregarVenta(venta);

            // ⚡ HU-05: Descontar stock vendrá aquí después

            TempData["Success"] = "Venta registrada correctamente.";
            return RedirectToAction("Index");
        }
    }
}