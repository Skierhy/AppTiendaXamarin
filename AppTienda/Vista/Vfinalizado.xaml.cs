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
    public partial class Vfinalizado : ContentPage
    {
        public Vfinalizado()
        {
            InitializeComponent();
            BindingContext = new VMfinalizar(Navigation);
        }
    }
}