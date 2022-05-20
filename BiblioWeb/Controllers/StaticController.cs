using Microsoft.AspNetCore.Mvc;

namespace BiblioWeb.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult Introduccion()
        {
            return View();
        }
        public IActionResult Nosotros() { 
            return View();
        }
        public IActionResult Agradecimientos() {
            return View();
        }
    }
}
