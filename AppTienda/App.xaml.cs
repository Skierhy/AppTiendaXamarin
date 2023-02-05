using AppTienda.Vista;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTienda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new Vcompras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
