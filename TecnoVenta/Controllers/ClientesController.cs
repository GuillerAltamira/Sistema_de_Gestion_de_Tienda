using Microsoft.AspNetCore.Mvc;
using TecnoVenta.Data;
using TecnoVenta.Models;

namespace TecnoVenta.Controllers
{
    public class ClientesController : Controller
    {
        ClienteDAO dao = new ClienteDAO();

        public IActionResult Index()
        {
            var lista = dao.ObtenerClientes();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            dao.AgregarCliente(cliente);
            return RedirectToAction("Index");
        }
    }
}