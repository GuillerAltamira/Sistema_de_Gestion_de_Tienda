using Microsoft.AspNetCore.Mvc;
using TecnoVenta.Data;

namespace TecnoVenta.Controllers {
    public class LoginController : Controller {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string nombreUsuario, string contraseña) {
            var usuario = usuarioDAO.ValidarLogin(nombreUsuario, contraseña);
            if (usuario != null) {
                HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
