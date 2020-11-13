using System;
using System.Collections.Generic;
using SimplyWeather2.Controls;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Home
{
    public partial class HomePage : BasePage
    {
        private HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();

            WeatherService weatherService = ((App)Application.Current).GetService<WeatherService>();
            WeatherLocationService weatherLocationService = ((App)Application.Current).GetService<WeatherLocationService>();

            _viewModel = new HomeViewModel(weatherService, weatherLocationService);

            _viewModel.OnNavigateToLocationPage = navigateToLocationPage;

            BindingContext = _viewModel;

        }

        private async void navigateToLocationPage()
        {
            await Shell.Current.GoToAsync("UpdateLocation", true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.FetchForecast();
        }

    }
}
