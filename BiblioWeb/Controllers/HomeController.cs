using BiblioWeb.Clases;
using BiblioWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
        [Authorize(Roles = "Usuario")]
        public IActionResult Ventas(int id) { 

            return View(new UsuariosCLS().MostrarUnLibro(id));
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Ventas(string id) {
            new UsuariosCLS().RegistrarPedido(int.Parse(id));
            return RedirectToAction("Carrito","Home");
        }
        [HttpGet]
        public IActionResult Login() { 
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> LoginAsync(TbUsuario user)
        {
            string mensaje = new UsuariosCLS().Iniciar_Sesion(user);
            if (mensaje != "Correcto") {
                return View();
            }
            var claims = new List<Claim>
                    {
                        new Claim("Usuario", user.Correo),
                        new Claim("Contraseña", user.Contraseña),

                    };

            claims.Add(new Claim(ClaimTypes.Role, "Usuario"));

            var claimsIdentityAdmin = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentityAdmin));
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
        [Authorize(Roles = "Usuario")]
        public IActionResult Carrito() {
            ViewBag.Precio = new UsuariosCLS().PrecioTotal();
            ViewBag.ListaID = new UsuariosCLS().GetIdPedido();
            return View(new UsuariosCLS().MostrarPedidos());
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Eliminar(int idEliminarPedido) {
            new UsuariosCLS().EliminarPedido(idEliminarPedido);
            return RedirectToAction("Carrito");
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Compra() {
            new UsuariosCLS().Comprar();
            return RedirectToAction("Carrito");
        }
        [Authorize(Roles = "Usuario")]
        public IActionResult Menu() {

            return View(new UsuariosCLS().MostrarLibros());
        }
    }
}