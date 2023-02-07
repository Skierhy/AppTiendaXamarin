using AppTienda.Datos;
using AppTienda.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTienda.VistaModelo
{
    public class VMagregarCarrito:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _Cantidad;
        public Mproductos EnviarDatosProductos { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMagregarCarrito(INavigation navigation, Mproductos EnviarProductos)
        {
            Navigation = navigation;
            EnviarDatosProductos = EnviarProductos;
            Texto = "$"+ EnviarDatosProductos.Precio;
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        }
        #endregion
        #region PROCESOS
        //public async Task ValidarCompra()
        //{
        //    var funcion = new DcompraCarrito();
            
        //}
        public  async Task InsertarCarrito()
        {
            if (Cantidad == 0)
            {
                Cantidad = 1;
            }
            var funcion = new DcompraCarrito();
            var recibirCarrito = new McompraCarrito();
            recibirCarrito.Cantidad = Cantidad.ToString();
            recibirCarrito.IdProducto = EnviarDatosProductos.IdProducto;
            recibirCarrito.PrecioUnidad = EnviarDatosProductos.Precio;
            double total = 0;
            double precioCompra = Convert.ToDouble(EnviarDatosProductos.Precio);
            double cantidad = Convert.ToDouble(Cantidad);
            total = precioCompra * cantidad;
            recibirCarrito.Total = total.ToString();
            await funcion.InsertarCarrito(recibirCarrito);
            await Volver();
        }
        public async Task Volver()
        {
            await Navigation.PopAsync();
        }
        public void Aumentar()
        {
            Cantidad += 1;
        }
        public void Decrementar()
        {
            if (Cantidad >= 0)
            {
                Cantidad -= 1;
            }
        }
        #endregion
        #region COMANDOS
        public ICommand Regresarcommand => new Command(async () => await Volver());
        public ICommand Aumentarcommand => new Command(Aumentar);
        public ICommand Decrementarcommand => new Command(Decrementar);
        public ICommand InsertarCarritocommand => new Command(async () => await InsertarCarrito());
        

        #endregion
    }
}
