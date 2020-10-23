using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Home
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
        }
    }
}
