using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCBasico.Context;
using MVCBasico.Models;
using System;
using System.Linq;
using System.Text;

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
        public ActionResult Login(Usuario usuarioLogueado)
        {
            //if (ModelState.IsValid)
            {
                //using (_context)
                {
                    //var obj = _context.Usuarios.Where(a => a.Apodo.Equals(usuarioLogueado.Apodo) && a.Contraseña.Equals(usuarioLogueado.Contraseña)).FirstOrDefault();
                    //if (obj != null)
                    if(true)
                    {
                        //HttpContext.Session.Set("UserID", BitConverter.GetBytes(obj.Id));
                        //HttpContext.Session.Set("UserName", Encoding.ASCII.GetBytes(obj.Apodo));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View(usuarioLogueado);
                    }
                }
            }
            return View(usuarioLogueado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOu2t()
        {
            //HttpContext.Session.Set("UserName", null);
            return RedirectToAction("Login", "Login");
        }


        public IActionResult LogOut()
        {
            return View("Login");
        }
    }
}
