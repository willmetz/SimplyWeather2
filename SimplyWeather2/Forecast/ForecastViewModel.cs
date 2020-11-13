using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using SimplyWeather2.Models;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;
using Xamarin.Essentials;

namespace SimplyWeather2.Forecast
{
    public class ForecastViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private readonly WeatherLocationService _weatherLocationService;

        private ObservableCollection<DailyForecastItem> _dailyForecasts;
        public ObservableCollection<DailyForecastItem> DailyForecasts
        {
            get => _dailyForecasts;
            set => RaiseAndSetIfChanged(value, ref _dailyForecasts, nameof(DailyForecasts));
        }

        public ForecastViewModel(WeatherService weatherService, WeatherLocationService weatherLocationService)
        {
            _weatherService = weatherService;
            _weatherLocationService = weatherLocationService;

        }

        public async Task UpdateExtendedForecast()
        {
            DailyForecasts = new ObservableCollection<DailyForecastItem>();

            SimplyWeatherLocation location = _weatherLocationService.GetCurrentLocation();
            List<DayForecast> forecast = await _weatherService.GetExtendedForecast(location);

            forecast.ForEach(day => DailyForecasts.Add(new DailyForecastItem(day)));

        }
    }
}
