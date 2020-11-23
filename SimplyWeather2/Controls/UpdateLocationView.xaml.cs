using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SimplyWeather2.Controls
{
    public partial class UpdateLocationView : StackLayout
    {
        public UpdateLocationView()
        {
            InitializeComponent();
        }

        void OnUpdateLocation(object sender, EventArgs args)
        {
            Shell.Current.GoToAsync("UpdateLocation", true);
        }
    }
}
