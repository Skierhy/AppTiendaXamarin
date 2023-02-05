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
    public class VMcompras : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        List<Mproductos> _ListaProductos;
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

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
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
                BackgroundColor = Color.FromHex("#4f5a85"),
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
                        TextColor = Color.FromHex("#fffaf2"),
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        Text = "$"+item.Precio,
                        TextColor = Color.FromHex("#c0daed"),
                        FontSize = 22,
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        Text = item.Unidad,
                        TextColor = Color.FromHex("#dee4ed"),
                        FontAttributes = FontAttributes.Italic,
                        FontSize = 10
                    },
                }
            };

            grid.Children.Add(stackLayout2, 1, 0);

            frame.Content = grid;
            StackLayout1.Children.Add(frame);

        }
        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
