using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SimplyWeather2.Controls
{
    public partial class BasePage : ContentPage
    {
        public IList<View> BasePageContent => BaseContentGrid.Children;


        public BasePage()
        {
            InitializeComponent();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LocationSelectedMenuItem.Clicked += OnNavigateToUpdateLocation;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            LocationSelectedMenuItem.Clicked -= OnNavigateToUpdateLocation;
        }

        private void OnNavigateToUpdateLocation(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("UpdateLocation", true);
        }
    }
}
