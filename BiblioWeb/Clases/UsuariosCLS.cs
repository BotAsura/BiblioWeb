using BiblioWeb.Models;
using System.Linq;

namespace BiblioWeb.Clases
{
    public class UsuariosCLS
    {
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

        public string Iniciar_Sesion(TbUsuario user) {
            using (BiblioWebDbContext db = new BiblioWebDbContext()) {
                var getUser = db.TbUsuario.Where(x => x.Correo == user.Correo.ToLower() && x.Contraseña == user.Contraseña).FirstOrDefault();
                if (getUser != null) {
                    return "Correcto";
                }
                return "Contraseña y/o Usuario Incorrecto";
            }                       
        }
    }
}
