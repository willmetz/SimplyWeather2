using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Radar
{
    public partial class RadarPage : ContentPage
    {
        public RadarPage()
        {
            InitializeComponent();

            BindingContext = new RadarViewModel();
        }
    }
}
