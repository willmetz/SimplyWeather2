using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Forecast
{
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage()
        {
            InitializeComponent();
            BindingContext = new ForecastViewModel();
        }
    }
}
