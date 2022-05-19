using BiblioWeb.Models;
using System.Linq;

namespace BiblioWeb.Clases
{
    public class UsuariosCLS
    {
        public string Reggistrar(TbUsuario user, TbCliente cliente) {
            using (BiblioWebDbContext db = new BiblioWebDbContext())
            {
                try
                {
                    TbUsuario setUser = new TbUsuario();

                    setUser.Correo = user.Correo;
                    setUser.Contraseña = user.Contraseña;

                    db.TbUsuario.Add(setUser);

                    db.SaveChanges();

                    var getUser = db.TbUsuario.Where(x => x.Correo == user.Correo).First();

                    TbCliente setCliente = new TbCliente();

                    setCliente.Nombre = cliente.Nombre;
                    setCliente.ApellidoPat = cliente.ApellidoPat;
                    setCliente.ApellidoMat = cliente.ApellidoMat;
                    setCliente.IdUsuario = getUser.IdUsuario;

                    db.TbCliente.Add(setCliente);

                    db.SaveChanges();

                    return "Todo OK";
                }
                catch (System.Exception ex)
                {
                    return "Nada Ok " +ex.Message;
                }
            
            }        
        }

    }
}
