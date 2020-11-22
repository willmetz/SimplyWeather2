using System;
using System.Collections.Generic;
using SimplyWeather2.Controls;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Forecast
{
    public partial class ForecastPage : BasePage
    {
        ForecastViewModel _viewModel;

        public ForecastPage()
        {
            InitializeComponent();

            App app = (App)Application.Current;

            WeatherService weatherService = app.GetService<WeatherService>();
            WeatherLocationService weatherLocationService = app.GetService<WeatherLocationService>();

            _viewModel = new ForecastViewModel(weatherService, weatherLocationService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.UpdateExtendedForecast();
        }
    }
}
