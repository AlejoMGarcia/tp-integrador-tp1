using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCBasico.Context;
using MVCBasico.Models;
using System.Threading.Tasks;

namespace MVCBasico.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly SubastaDatabaseContext _context;

        public LoginController(ILogger<LoginController> logger, SubastaDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UsuarioLoginVM usuarioLogueado)
        {
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Apodo == usuarioLogueado.Apodo 
                && u.Contraseña == usuarioLogueado.Contraseña);

            if (usuario != null)
            {
                //HttpContext.Session.Set("UserID", BitConverter.GetBytes(obj.Id));
                //HttpContext.Session.Set("UserName", Encoding.ASCII.GetBytes(obj.Apodo));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorLogin = "Usuario o contraseña ingresado invalido.";
                return View(usuarioLogueado);
            }
        }

        public IActionResult LogOut()
        {
            return View("Login");
        }
    }
}
