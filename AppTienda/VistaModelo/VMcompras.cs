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

namespace AppTienda.VistaModelo
{
    public class VMcompras : BaseViewModel
    {
        #region VARIABLES
        List<Mproductos> _ListaProductos;
        List<McompraCarrito> _ListaCompraCarritoTotal;
        double totalProductos =0;
        string _stringTotalProductos = "Total: $ 0";
        #endregion
        #region CONSTRUCTOR
        public VMcompras(INavigation navigation, StackLayout StackLayout1)
        {
            Navigation = navigation;
            MostrarProductos(StackLayout1);
        }
        #endregion
        #region OBJETOS
        public List<Mproductos> ListaProductos
        {
            get { return _ListaProductos; }
            set { SetValue(ref _ListaProductos, value); }
        }

        public string StringTotalProductos
        {
            get { return _stringTotalProductos; }
            set { SetValue(ref _stringTotalProductos, value); }
        }


        public List<McompraCarrito> ListaCompraCarritoTotal
        {
            get { return _ListaCompraCarritoTotal; }
            set { SetValue(ref _ListaCompraCarritoTotal, value); }
        }
        #endregion
        #region PROCESOS

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
                HeightRequest = 170,
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
            foreach (var item in ListaCompraCarritoTotal)
            {
                totalProductos += Convert.ToDouble( item.PrecioProducto);
            }
            StringTotalProductos = "Total: $ " + totalProductos;


        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await MostrarTotal());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
