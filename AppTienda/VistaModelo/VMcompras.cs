using AppTienda.Datos;
using AppTienda.Modelo;
using AppTienda.Vista;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.SharedTransitions;
using static Xamarin.Essentials.Permissions;

namespace AppTienda.VistaModelo
{
    public class VMcompras : BaseViewModel
    {
        #region VARIABLES
        List<Mproductos> _ListaProductos;
        List<McompraCarrito> _ListaCompraCarritoTotal;
        List<McompraCarrito> _ListaCompraCarrito;
        double totalProductos =0;
        double compra = 0;
        int _Cantidad;
        string _stringTotalProductos = "Subtotal: $0";
        string _StringTotalCarrito = "$0";
        string _stringCompra = "$0";
        string _stringEnvio = "$0";
        bool _VisibleCarrito;
        bool _VisibleCompras;
        #endregion
        #region CONSTRUCTOR
        public VMcompras(INavigation navigation, StackLayout StackLayout1)
        {
            Navigation = navigation;
            MostrarProductos(StackLayout1);
            VisibleCarrito = false;
            VisibleCompras = true;
        }
        #endregion
        #region OBJETOS

        public string StringCompra
        {
            get { return _stringCompra; }
            set { SetValue(ref _stringCompra, value); }
        }
        public string StringEnvio
        {
            get { return _stringEnvio; }
            set { SetValue(ref _stringEnvio, value); }
        }
        public List<Mproductos> ListaProductos
        {
            get { return _ListaProductos; }
            set { SetValue(ref _ListaProductos, value); }
        }

        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        }
        public string StringTotalProductos
        {
            get { return _stringTotalProductos; }
            set { SetValue(ref _stringTotalProductos, value); }
        }

        public string StringTotalCarrito
        {
            get { return _StringTotalCarrito; }
            set { SetValue(ref _StringTotalCarrito, value); }
        }

        public bool VisibleCarrito
        {
            get { return _VisibleCarrito; }
            set { SetValue(ref _VisibleCarrito, value); }
        }

        public bool VisibleCompras
        {
            get { return _VisibleCompras; }
            set { SetValue(ref _VisibleCompras, value); }
        }


        public List<McompraCarrito> ListaCompraCarritoTotal
        {
            get { return _ListaCompraCarritoTotal; }
            set { SetValue(ref _ListaCompraCarritoTotal, value); }
        }

        public List<McompraCarrito> ListaCompraCarrito
        {
            get { return _ListaCompraCarrito; }
            set { SetValue(ref _ListaCompraCarrito, value); }
        }
        #endregion
        #region PROCESOS

        public async Task MostrarCarrito() {
            var funcion = new DcompraCarrito();
            ListaCompraCarrito = await funcion.MostrarCarrito();
        }

        public  async Task  MostrarCarrito(Grid Gmain, Frame fTotal, StackLayout detallesDeCarrito)
        {
            await Task.WhenAll(
                fTotal.FadeTo(0, 500),
                Gmain.TranslateTo(0, -1000, 500, Easing.CubicIn),
                detallesDeCarrito.TranslateTo(0, 0, 500, Easing.CubicIn)
                );
            VisibleCompras = false;
            VisibleCarrito = true;
        }

        public async Task MostrarCompras(Grid Gmain, Frame fTotal, StackLayout detallesDeCarrito)
        {
            await Task.WhenAll(
                fTotal.FadeTo(1, 500),
                Gmain.TranslateTo(0, 0, 500, Easing.CubicIn),
                detallesDeCarrito.TranslateTo(0, 1000, 500, Easing.CubicIn)
                );
            VisibleCompras = true;
            VisibleCarrito = false;
        }
        public async Task MostrarProductos(StackLayout StackLayout1)
        {
            var funcion = new Dproductos();

            ListaProductos = await funcion.MostrarProductos();
            foreach (var item in ListaProductos) {
                DibujarProductos(item, StackLayout1);
            }
        }
        public void DibujarProductos(Mproductos item, StackLayout StackLayout1)
        {

            var frame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#9292c6"),
                HeightRequest = 150,
                Margin = 15,
                Padding = 0,
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(150) },
                    new ColumnDefinition { Width = GridLength.Star },
                }
            };

            var stackLayout1 = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new Image
                    {
                            Source = item.ImgProducto,
                            HeightRequest = 120
                    }
                }
            };

            grid.Children.Add(stackLayout1, 0, 0);

            var stackLayout2 = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = item.Descripcion,
                        FontSize = 18,
                        CharacterSpacing = 1,
                        TextColor = Color.FromHex("#262531"),
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        Text = "$"+item.Precio,
                        TextColor = Color.FromHex("#565b77"),
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        Text = item.Unidad,
                        TextColor = Color.FromHex("#dbd4f6"),
                        FontAttributes = FontAttributes.Italic,
                        FontSize = 13
                    },
                }
            };

            grid.Children.Add(stackLayout2, 1, 0);

            frame.Content = grid;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (object sender, EventArgs e) =>
            {
                var page = (App.Current.MainPage as SharedTransitionNavigationPage).CurrentPage;
                SharedTransitionNavigationPage.SetBackgroundAnimation(page, BackgroundAnimation.Flip);
                SharedTransitionNavigationPage.SetTransitionDuration(page, 1000);
                SharedTransitionNavigationPage.SetTransitionSelectedGroup(page, "producto");
                await Navigation.PushAsync(new VagregarCarrito(item));
            };
            StackLayout1.Children.Add(frame);
            grid.GestureRecognizers.Add(tapGestureRecognizer);

        }
        public async Task MostrarTotal()
        {
            var funcion = new DcompraCarrito();
            ListaCompraCarritoTotal = await funcion.MostrarTotal();
            totalProductos = 0;
            Cantidad = 0;
            compra = 0;
            foreach (var item in ListaCompraCarritoTotal)
            {
                totalProductos += Convert.ToDouble( item.Total);
                Cantidad++;
            }
            StringTotalProductos = "Subtotal: $" + totalProductos;
            StringTotalCarrito = "$" + totalProductos;

            if (totalProductos >= 5000) {
                StringCompra = "$" + totalProductos;
                StringEnvio = "Gratis";
            }else if (totalProductos < 5000)
            {
                if (Cantidad >= 1) {
                    compra = totalProductos + 2000;
                    StringCompra = "$" + compra;
                    StringEnvio = "$2000";
                }
                
            }
        }
        public async Task ComprarCarrito()
        {
            if(Cantidad == 0)
            {
                await Navigation.PushAsync(new Verror());
            }
            else {
                var funcion = new Dproductos();
                await funcion.ComprarCarrito();
                await Navigation.PushAsync(new Vfinalizado());
            }
            
        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await MostrarTotal());
        public ICommand ComprarAsyncommand => new Command(async () => await ComprarCarrito());
        #endregion
    }
}
