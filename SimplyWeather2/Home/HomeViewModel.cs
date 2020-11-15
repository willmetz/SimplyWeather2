using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using SimplyWeather2.Models;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimplyWeather2.Home
{
    public class HomeViewModel : BaseViewModel
    {

        private string _locationName;
        public string LocationName
        {
            get => _locationName;
            set => RaiseAndSetIfChanged(value, ref _locationName, nameof(LocationName));
        }

        private string _temperature;
        public string Temperature
        {
            get => _temperature;
            set => RaiseAndSetIfChanged(value, ref _temperature, nameof(Temperature));
        }

        private string _windSpeed;
        public string WindSpeed
        {
            get => _windSpeed;
            set => RaiseAndSetIfChanged(value, ref _windSpeed, nameof(WindSpeed));
        }

        private string _conditionDescription;
        public string ConditionDescription
        {
            get => _conditionDescription;
            set => RaiseAndSetIfChanged(value, ref _conditionDescription, nameof(ConditionDescription));
        }

        private string _feelsLikeTemperature;
        public string FeelsLikeTemperature
        {
            get => _feelsLikeTemperature;
            set => RaiseAndSetIfChanged(value, ref _feelsLikeTemperature, nameof(FeelsLikeTemperature));
        }

        private string _highLowTemp;
        public string HighLowTemp
        {
            get => _highLowTemp;
            set => RaiseAndSetIfChanged(value, ref _highLowTemp, nameof(HighLowTemp));
        }

        private string _currentConditionsImage;
        public string CurrentConditionsImage
        {
            get => _currentConditionsImage;
            set => RaiseAndSetIfChanged(value, ref _currentConditionsImage, nameof(CurrentConditionsImage));            
        } 

        private ObservableCollection<HourlyForecastItem> _hourlyForecastItems;
        public ObservableCollection<HourlyForecastItem> HourlyForecastItems
        {
            get => _hourlyForecastItems;
            set => RaiseAndSetIfChanged(value, ref _hourlyForecastItems, nameof(HourlyForecastItems));
        }

        private bool _showLocationUnkonwn;
        public bool ShowLocationUnknown
        {
            get => _showLocationUnkonwn;
            set => RaiseAndSetIfChanged(value, ref _showLocationUnkonwn, nameof(ShowLocationUnknown));
        }

        private WeatherService _weatherService;
        private WeatherLocationService _weatherLocationService;
        private AppPreferencesService _appPreferencesService;

        public HomeViewModel(WeatherService weatherService, WeatherLocationService weatherLocationService, AppPreferencesService appPreferencesService)
        {
            _weatherService = weatherService;
            _weatherLocationService = weatherLocationService;
            _appPreferencesService = appPreferencesService;
        }

        public async Task FetchForecast()
        {
            SimplyWeatherLocation currentLocation = _weatherLocationService.GetCurrentLocation();

            if(currentLocation?.State == LocationState.LocationReady)
            {
                ShowLocationUnknown = false;
                Models.Forecast forecast = await _weatherService.GetTodaysWeather(currentLocation);

                UpdateCurrentConditions(forecast);
                UpdateHourlyConditions(forecast.HourlyConditionsForDay);

                LocationName = _appPreferencesService.GetLocationName();
            }
            else
            {
                LocationUnknown();
            }
            
        }

        private void UpdateCurrentConditions(Models.Forecast forecast)
        {
            Temperature = $"{forecast.CurrentTemperature}\u00B0";
            WindSpeed = $"{forecast.CurrentWindSpeed} mph";
            ConditionDescription = forecast.CurrentConditions;
            HighLowTemp = $"{forecast.HighTemp}\u00B0/{forecast.LowTemp}\u00B0";
            FeelsLikeTemperature = $"Feels Like {forecast.FeelsLikeTemp}\u00B0";
            CurrentConditionsImage = forecast.CurrentConditionsImageUrl;
        }

        private void UpdateHourlyConditions(List<WeatherCondition> hourlyConditions)
        {
            List<HourlyForecastItem> forecastItems = new List<HourlyForecastItem>();

            foreach(WeatherCondition weatherCondition in hourlyConditions)
            {
                forecastItems.Add(new HourlyForecastItem
                {
                    Temperature = $"{weatherCondition.Temperature}\u00B0",
                    TimeOfDay = weatherCondition.Time.ToString("h:mm tt", DateTimeFormatInfo.InvariantInfo),
                    ImageUrl = weatherCondition.ImageUrl
                });
            }

            HourlyForecastItems = new ObservableCollection<HourlyForecastItem>(forecastItems);
        }

        private void LocationUnknown()
        {
            ShowLocationUnknown = true;
        }
    }
}
