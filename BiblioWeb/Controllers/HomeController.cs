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
        [HttpGet]
        public IActionResult Ventas(int id) { 

            return View(new UsuariosCLS().MostrarUnLibro(id));
        }
        [HttpPost]
        public IActionResult Ventas(string id) {
            new UsuariosCLS().RegistrarPedido(int.Parse(id));
            return RedirectToAction("Carrito","Home");
        }
        [HttpGet]
        public IActionResult Login() { 
            return View();
        }   
        [HttpPost]
        public IActionResult Login(TbUsuario user)
        {
            string mensaje = new UsuariosCLS().Iniciar_Sesion(user);
            if (mensaje != "Correcto") {
                return View();
            }
            return RedirectToAction("Menu");
            
        }
        [HttpGet]
        public IActionResult Registrar() {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(TbUsuario user,TbCliente cliente) {
            ViewBag.Error = new UsuariosCLS().Registrar(user, cliente);
            return View();
        }
        [HttpGet]
        public IActionResult Carrito() {
            ViewBag.Precio = new UsuariosCLS().PrecioTotal();
            ViewBag.ListaID = new UsuariosCLS().GetIdPedido();
            return View(new UsuariosCLS().MostrarPedidos());
        }
        [HttpPost]
        public IActionResult Eliminar(int idEliminarPedido) {
            new UsuariosCLS().EliminarPedido(idEliminarPedido);
            return RedirectToAction("Carrito");
        }
        [HttpPost]
        public IActionResult Compra() {
            new UsuariosCLS().Comprar();
            return RedirectToAction("Carrito");
        }
        
        public IActionResult Menu() {

            return View(new UsuariosCLS().MostrarLibros());
        }
    }
}