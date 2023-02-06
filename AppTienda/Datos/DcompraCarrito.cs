using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database.Query;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using AppTienda.Modelo;
using AppTienda.Conexiones;
namespace AppTienda.Datos
{
    public class DcompraCarrito
    {
        public async Task InsertarCarrito(McompraCarrito recibirCarrito)
        {
            await Cconexion.conexionFireBase
                .Child("CompraCarrito")
                .PostAsync(new McompraCarrito()
            {
                Cantidad = recibirCarrito.Cantidad,
                PrecioUnidad = recibirCarrito.PrecioUnidad,
                Total = recibirCarrito.Total,
                IdProducto = recibirCarrito.IdProducto
            });
        }

        public async Task <List<McompraCarrito>> MostrarTotal(){

            var ListaCarrito = new List<McompraCarrito>();
            var mostrarEnviarProductos = new Mproductos();
            var funcionProductos = new Dproductos();
            var datos = (await Cconexion.conexionFireBase
                .Child("CompraCarrito")
                .OnceAsync<McompraCarrito>())
                .Where(dato => dato.Key != "Modelo")
                .Select(item => new McompraCarrito
                {
                    IdProducto = item.Object.IdProducto,
                    IdDetalleCompra = item.Key,
                    
                });

            foreach (var dato in datos) {
                var enviarDato = new McompraCarrito();
                enviarDato.IdProducto = dato.IdProducto;
                mostrarEnviarProductos.IdProducto = dato.IdProducto;
                var listaProductos = await funcionProductos.MostrarProductosID(mostrarEnviarProductos);
                enviarDato.PrecioProducto = listaProductos[0].Precio;
                ListaCarrito.Add(enviarDato);

            }
            return ListaCarrito;
        }
    }
}
