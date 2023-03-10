using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AppTienda.Modelo;
using AppTienda.Conexiones;

namespace AppTienda.Datos
{
    public class Dproductos
    {
        public async Task<List<Mproductos>> MostrarProductos()
        {
            return (
                await Cconexion.conexionFireBase.Child("Productos").OnceAsync<Mproductos>()).Select(item => new Mproductos
                    {
                        Descripcion = item.Object.Descripcion,
                        ImgProducto = item.Object.ImgProducto,
                        Precio = item.Object.Precio,
                        Unidad = item.Object.Unidad,
                        IdProducto = item.Key
                    }
            ).ToList();
        }

        public async Task<List<Mproductos>> MostrarProductosID(Mproductos enviarProductos)
        {
            return (
                await Cconexion.conexionFireBase
                .Child("Productos")
                .OnceAsync<Mproductos>())
                .Where(dato => dato.Key == enviarProductos.IdProducto)
                .Select(item => new Mproductos
                {
                    Descripcion = item.Object.Descripcion,
                    ImgProducto = item.Object.ImgProducto,
                    Precio = item.Object.Precio,
                    Unidad = item.Object.Unidad,
                    IdProducto = item.Key
                }
            ).ToList();
        }

        public async Task ComprarCarrito()
        {
            await Cconexion.conexionFireBase
                .Child("CompraCarrito")
                .DeleteAsync();
        }
    }
}
