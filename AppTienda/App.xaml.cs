using AppTienda.Vista;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SharedTransitions;
namespace AppTienda
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SharedTransitionNavigationPage( new Vcompras());
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
