using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class TbVentas
    {
        public int IdVentas { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPedido { get; set; }

        public virtual TbPedido IdPedidoNavigation { get; set; }
    }
}
