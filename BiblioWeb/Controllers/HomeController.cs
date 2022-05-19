using BiblioWeb.Clases;
using BiblioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login() { 
            return View();
        }
        [HttpGet]
        public IActionResult Registrar() {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(TbUsuario user,TbCliente cliente) {
            ViewBag.Error = new UsuariosCLS().Reggistrar(user,cliente);
            return View();
        }
    }
}
