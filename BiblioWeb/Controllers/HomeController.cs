using BiblioWeb.Clases;
using BiblioWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BiblioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "Usuario")]
        public IActionResult Ventas(int id) {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View(new UsuariosCLS().MostrarUnLibro(id));
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Ventas(string id) {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            new UsuariosCLS().RegistrarPedido(int.Parse(id));
            return RedirectToAction("Carrito","Home");
        }
        [HttpGet]
        public IActionResult Login() { 
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> Login(TbUsuario user)
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
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Registrar() {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(TbUsuario user,TbCliente cliente) {
            ViewBag.Error = new UsuariosCLS().Registrar(user, cliente);
            return RedirectToAction("Login");
        }
        [HttpGet]
        [Authorize(Roles = "Usuario")]
        public IActionResult Carrito() {
            ViewBag.Precio = new UsuariosCLS().PrecioTotal();
            ViewBag.ListaID = new UsuariosCLS().GetIdPedido();
            ViewBag.Usuario = new UsuariosCLS().Usuario;
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
            
            return RedirectToAction("Ticket");
        }
        [HttpGet]
        [Authorize(Roles = "Usuario")]
        public IActionResult Menu() {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View(new UsuariosCLS().MostrarLibros());
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Menu(string palabraClave)
        {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View(new UsuariosCLS().Filtro(palabraClave));
        }
        [HttpGet]
        [Authorize(Roles = "Usuario")]
        public IActionResult Vender() {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult Vender(TbLibro librio, IFormFile image) {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string extension = Path.GetExtension(image.FileName);
            string fileName = DateTime.Now.ToString("yymmssfff");
            string path = Path.Combine(wwwRootPath + "/img/", fileName + extension);
            string ruta = "/img/" + fileName + extension;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            new UsuariosCLS().AgregarLibro(librio, ruta);
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View();
        }        
        [HttpGet]
        [Authorize(Roles = "Usuario")]
        public IActionResult UserConfig() {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            return View(new UsuariosCLS().infoCliente());
        }
        [Authorize(Roles = "Usuario")]
        public IActionResult Ticket() {
            UsuariosCLS obj = new UsuariosCLS();
            obj.Comprar();
            List<TicketCLS> list = obj.Ticket();
            if (list.Count != 0)
            {              
                ViewBag.Ticket = list[0].Total;
                ViewBag.Nombre = list[0].Nombre;
                ViewBag.Fecha = list[0].Fecha;
                return View(list); 
            }
            return RedirectToAction("Carrito");
        }
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public IActionResult UserConfig(TbCliente cliente, TbUsuario usuario)
        {
            ViewBag.Usuario = new UsuariosCLS().Usuario;
            new UsuariosCLS().ModificarCLiente(cliente, usuario);
            return View(new UsuariosCLS().infoCliente());
        }
    }
}