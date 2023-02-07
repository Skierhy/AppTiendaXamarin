using System;
using System.Collections.Generic;
using System.Text;

namespace AppTienda.Modelo
{
    public class McompraCarrito
    {
        public string Cantidad { get; set; }
        public string PrecioUnidad { get; set; }
        public string Total { get; set; }
        public string IdProducto { get; set; }
        public string IdDetalleCompra { get; set; }

        public string PrecioProducto { get; set; }

        public string Img { get; set; }

        public string Descripcion { get; set; }

    }
}
