using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class TbUsuario
    {
        public TbUsuario()
        {
            TbCliente = new HashSet<TbCliente>();
            TbVentas = new HashSet<TbVentas>();
        }

        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<TbCliente> TbCliente { get; set; }
        public virtual ICollection<TbVentas> TbVentas { get; set; }
    }
}
