﻿using AppTienda.VistaModelo;
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
        }
    }
}