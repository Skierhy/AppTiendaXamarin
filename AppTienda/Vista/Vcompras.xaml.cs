using AppTienda.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTienda.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vcompras : ContentPage
    {
        VMcompras vm;
        public Vcompras()
        {
            InitializeComponent();
            vm = new VMcompras(Navigation, productosLista);
            BindingContext = vm;
            this.Appearing += Vcompras_Appearing;
        }

        private async void Vcompras_Appearing(object sender, EventArgs e)
        {
            await vm.MostrarTotal();
            await vm.MostrarCarrito();
        }

        private async void SwipeCarrito(object sender, SwipedEventArgs e)
        {
            await vm.MostrarCarrito(Gmain, fTotal,detallesDeCarrito);
        }

        private async void SwipeCompras(object sender, SwipedEventArgs e)
        {
            await vm.MostrarCompras(Gmain, fTotal, detallesDeCarrito);
        }
    }
}