using System;
using System.Collections.Generic;
using SimplyWeather2.ViewModels;
using SimplyWeather2.Views;
using Xamarin.Forms;

namespace SimplyWeather2
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
