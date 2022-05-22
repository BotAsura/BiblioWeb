using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class TbLibro
    {
        public TbLibro()
        {
            TbPedido = new HashSet<TbPedido>();
        }

        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Precio { get; set; }
        public string Ruta { get; set; }

        public virtual ICollection<TbPedido> TbPedido { get; set; }
    }
}
