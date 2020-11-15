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

            App myApp = ((App)Application.Current);
            WeatherService weatherService = myApp.GetService<WeatherService>();
            WeatherLocationService weatherLocationService = myApp.GetService<WeatherLocationService>();
            AppPreferencesService appPreferencesService = myApp.GetService<AppPreferencesService>();

            _viewModel = new HomeViewModel(weatherService, weatherLocationService, appPreferencesService);

            BindingContext = _viewModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.FetchForecast();
            UpdateLocation.Clicked += OnNavigateToUpdateLocation;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            UpdateLocation.Clicked -= OnNavigateToUpdateLocation;
        }
    }
}
