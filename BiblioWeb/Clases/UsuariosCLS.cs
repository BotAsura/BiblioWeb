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
                    libro.Add(Libro.Descripcion);
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
                    setPedido.Visibilidad = true;

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
                        var Libro = db.TbLibro.Where(x => x.IdLibro == getPedidos[i].IdLibro && getPedidos[i].Visibilidad).FirstOrDefault();

                        if (Libro != null)
                        {
                            getLibrosPedidos.Add(Libro);
                        }
                    }

                }
                return getLibrosPedidos;
            }
        }
        public List<int> GetIdPedido() {
            using (BiblioWebDbContext db = new BiblioWebDbContext()){
                List<int> lista = new List<int>();
                var getUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();
                if (getUsuario != null){
                    var getPedidos = db.TbPedido.Where(x => x.IdUsuario == getUsuario.IdUsuario && x.Visibilidad == true).ToList();                    
                        for (int i = 0; i < getPedidos.Count; i++)
                        {
                            lista.Add(getPedidos[i].IdPedido);
                        }                    
                }
                return lista;
            }
        }
        public string EliminarPedido(int idLibro)
        {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                var getPedido = db.TbPedido.Where(x => x.IdPedido == idLibro).FirstOrDefault();

                if (getPedido != null)
                {
                    try
                    {
                        db.TbPedido.Remove(getPedido);

                        db.SaveChanges();

                        return "Pedido Eliminado con exito";
                    }
                    catch (System.Exception)
                    {

                        return "El pedido no se pudo eliminar";
                    }
                }
                return "El pedido no se pudo eliminar";
            }
        }
        public string PrecioTotal() {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                float total = 0.0f;
                var getUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();
                if (getUsuario != null)
                {
                    var getPedidos = db.TbPedido.Where(x => x.IdUsuario == getUsuario.IdUsuario ).ToList();

                    for (int i = 0; i < getPedidos.Count; i++)
                    {
                        if (getPedidos[i].Visibilidad)
                        {
                            var getBook = db.TbLibro.Where(x => x.IdLibro == getPedidos[i].IdLibro).FirstOrDefault();
                            total += float.Parse(getBook.Precio.Substring(1)); 
                        }
                    }
                }
                return "$" + total.ToString();
            }        
        }            
        public string Comprar() {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                try
                {
                    var getIdUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();

                    if (getIdUsuario != null)
                    {
                        var getPedidos = db.TbPedido.Where(x => x.IdUsuario == getIdUsuario.IdUsuario).ToList();
                        for (int i = 0; i < getPedidos.Count; i++)
                        {
                            getPedidos[i].Visibilidad = false;

                            TbVentas setVenta = new TbVentas();

                            setVenta.Fecha = System.DateTime.Now;
                            setVenta.IdPedido = getPedidos[i].IdPedido;

                            db.TbPedido.Update(getPedidos[i]);  
                            db.TbVentas.Add(setVenta);

                            db.SaveChanges();
                        }
                        return "Compra realizada con éxito";
                    }
                    return "Su compra no se pudo realizar";
                }
                catch (System.Exception)
                {
                    return "Su compra no se pudo realizar";
                }
            }
        }
        public string AgregarLibro(TbLibro libro, string ruta) {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                TbLibro setLibro = new TbLibro();

                setLibro.Titulo = libro.Titulo;
                setLibro.Autor = libro.Autor;
                setLibro.Genero = libro.Genero;
                setLibro.Precio = libro.Precio;
                setLibro.Descripcion = libro.Descripcion;
                setLibro.Cantidad = libro.Cantidad;
                setLibro.Ruta = ruta;

                try
                {
                    db.TbLibro.Add(setLibro);

                    db.SaveChanges();

                    return "Todo Bien";
                }
                catch (System.Exception)
                {
                    return "Todo Mal";                    
                }

            }
        }
        public List<TbCliente> infoCliente() {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                var getUsuario = db.TbUsuario.Where(x => x.Correo == Usuario).FirstOrDefault();
                if (getUsuario != null)
                {
                    var getInfo = db.TbCliente.Where(x => x.IdUsuario == getUsuario.IdUsuario).ToList();
                    return getInfo;
                }
                return null;
            }        
        }
        public string ModificarCLiente(TbCliente cliente, TbUsuario usuario) {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                var getUsuario = db.TbUsuario.Where(x => x.Correo == usuario.Correo).FirstOrDefault();
                if (getUsuario != null)
                {
                    TbUsuario setUsuario = getUsuario;

                    setUsuario.Contraseña = usuario.Contraseña;

                    try
                    {
                        db.TbUsuario.Update(setUsuario);
                        db.SaveChanges();

                        var getCliente = db.TbCliente.Where(x => x.IdUsuario == getUsuario.IdUsuario).FirstOrDefault();
                        if (getCliente != null)
                        {
                            TbCliente setCliente = getCliente;

                            setCliente.Nombre = cliente.Nombre;
                            setCliente.ApellidoPat = cliente.ApellidoPat;
                            setCliente.ApellidoMat = cliente.ApellidoMat;
                            setCliente.Colonia = cliente.Colonia;
                            setCliente.Calle = cliente.Calle;
                            setCliente.Numero = cliente.Numero;

                            db.TbCliente.Update(setCliente);
                            db.SaveChanges();

                            return "Se actualizo tu informacion con exito";
                        }
                    }
                    catch (System.Exception)
                    {

                        return "No se pudo actualizar tu informacion";
                    }
                }
                return "No se pudo actualizar tu informacion";
            }
        }
    }
}
