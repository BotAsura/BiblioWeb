using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class TbPedido
    {
        public TbPedido()
        {
            TbVentas = new HashSet<TbVentas>();
        }

        public int IdPedido { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public bool Visibilidad { get; set; }

        public virtual TbLibro IdLibroNavigation { get; set; }
        public virtual TbUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<TbVentas> TbVentas { get; set; }
    }
}
