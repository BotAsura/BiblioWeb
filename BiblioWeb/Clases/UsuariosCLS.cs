using BiblioWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace BiblioWeb.Clases
{
    public class UsuariosCLS
    {
        private static string usuario;

        public string Usuario { get => usuario; set => usuario = value; }

        public string Registrar(TbUsuario user, TbCliente cliente) {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                try
                {
                    var getUser = db.TbUsuario.Where(x => x.Correo == user.Correo).FirstOrDefault();

                    if (getUser != null)
                    {
                        return "El usuario ya existe";
                    }

                    TbUsuario setUser = new TbUsuario();

                    setUser.Correo = user.Correo.ToLower();
                    setUser.Contraseña = user.Contraseña;

                    db.TbUsuario.Add(setUser);

                    db.SaveChanges();

                    getUser = db.TbUsuario.Where(x => x.Correo == user.Correo).FirstOrDefault();

                    TbCliente setCliente = new TbCliente();

                    setCliente.Nombre = cliente.Nombre.ToLower();
                    setCliente.ApellidoPat = cliente.ApellidoPat.ToLower();
                    setCliente.ApellidoMat = cliente.ApellidoMat.ToLower();
                    setCliente.Colonia = cliente.Colonia.ToLower();
                    setCliente.Calle = cliente.Calle.ToLower();
                    setCliente.Numero = cliente.Numero;
                    setCliente.IdUsuario = getUser.IdUsuario;

                    db.TbCliente.Add(setCliente);

                    db.SaveChanges();

                    return "Todo OK";
                }
                catch (System.Exception ex)
                {
                    return "Nada Ok " + ex.Message;
                }
            }
        }

        public string Iniciar_Sesion(TbUsuario user) {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                var getUser = db.TbUsuario.Where(x => x.Correo == user.Correo.ToLower() && x.Contraseña == user.Contraseña).FirstOrDefault();
                if (getUser != null) {
                    Usuario = getUser.Correo;
                    return "Correcto";
                }
                return "Contraseña y/o Usuario Incorrecto";
            }
        }

        public List<TbLibro> MostrarLibros() {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                return db.TbLibro.ToList();
            }
        }

        public List<string> MostrarUnLibro(int id) {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                List<string> libro = new List<string>();
                var Libro = db.TbLibro.Where(x => x.IdLibro == id).FirstOrDefault();
                if (Libro != null) {
                    libro.Add(Libro.Titulo);
                    libro.Add(Libro.Autor);
                    libro.Add(Libro.Genero);
                    libro.Add(Libro.Precio);
                    libro.Add(Libro.Ruta);
                    libro.Add(Libro.IdLibro.ToString());
                }
                return libro;

            }
        }

        public string RegistrarPedido(int id) {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                TbPedido setPedido = new TbPedido();

                var getUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();
                if (getUsuario != null)
                {
                    setPedido.IdUsuario = getUsuario.IdUsuario;
                    setPedido.IdLibro = id;

                    db.TbPedido.Add(setPedido);

                    db.SaveChanges();

                    return "Todo Bien";
                }
                return "Todo Mal";
            }
        }

        public List<TbLibro> MostrarPedidos() {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                var getUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();
                List<TbLibro> getLibrosPedidos = new List<TbLibro>();
                if (getUsuario != null)
                {
                    var getPedidos = db.TbPedido.Where(x => x.IdUsuario == getUsuario.IdUsuario).ToList();
                
                    for (int i = 0; i < getPedidos.Count; i++)
                    {
                        var Libro = db.TbLibro.Where(x => x.IdLibro == getPedidos[i].IdLibro).First();

                        getLibrosPedidos.Add(Libro);
                    }

                }
                return getLibrosPedidos;                
            }
        }

        public string Comprar(string[] books) {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                return "Hola";
            }
        }
    }
}
