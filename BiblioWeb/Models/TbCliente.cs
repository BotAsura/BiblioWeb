using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class TbCliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Telefono { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int? Numero { get; set; }
        public int IdUsuario { get; set; }

        public virtual TbUsuario IdUsuarioNavigation { get; set; }
    }
}
