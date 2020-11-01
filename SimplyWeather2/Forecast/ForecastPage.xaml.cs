using System;
using System.Collections.Generic;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Forecast
{
    public partial class ForecastPage : ContentPage
    {
        ForecastViewModel _viewModel;

        public ForecastPage()
        {
            InitializeComponent();

            WeatherService weatherService = ((App)Application.Current).GetService<WeatherService>();

            _viewModel = new ForecastViewModel(weatherService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.UpdateExtendedForecast();
        }
    }
}
