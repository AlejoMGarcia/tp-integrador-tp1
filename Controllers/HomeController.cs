using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCBasico.Context;
using MVCBasico.Models;
using System.Diagnostics;

namespace MVCBasico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SubastaDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, SubastaDatabaseContext context)
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
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    //var obj = _context.Usuarios.Where(a => a.Apodo.Equals(usuarioLogueado.Apodo) && a.Contraseña.Equals(usuarioLogueado.Contraseña)).FirstOrDefault();
                    //if (obj != null)
                    if(true)
                    {
                        //Session["UserID"] = obj.UserId.ToString();
                        //Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(usuarioLogueado);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
