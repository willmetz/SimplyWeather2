using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using SimplyWeather2.Models;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;
using Xamarin.Essentials;

namespace SimplyWeather2.Home
{
    public class HomeViewModel : BaseViewModel
    {

        private string _locationName;
        public string LocationName
        {
            get => _locationName;
            set
            {
                _locationName = value;
                RaiseAndSetIfChanged(nameof(LocationName));
            }
        }

        private string _temperature;
        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                RaiseAndSetIfChanged(nameof(Temperature));
            }
        }

        private string _windSpeed;
        public string WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                RaiseAndSetIfChanged(nameof(WindSpeed));
            }
        }

        private string _conditionDescription;
        public string ConditionDescription
        {
            get => _conditionDescription;
            set
            {
                _conditionDescription = value;
                RaiseAndSetIfChanged(nameof(ConditionDescription));
            }
        }

        private string _feelsLikeTemperature;
        public string FeelsLikeTemperature
        {
            get => _feelsLikeTemperature;
            set
            {
                _feelsLikeTemperature = value;
                RaiseAndSetIfChanged(nameof(FeelsLikeTemperature));
            }
        }

        private string _highLowTemp;
        public string HighLowTemp
        {
            get => _highLowTemp;
            set
            {
                _highLowTemp = value;
                RaiseAndSetIfChanged(nameof(HighLowTemp));
            }
        }

        private string _currentConditionsImage;
        public string CurrentConditionsImage
        {
            get => _currentConditionsImage;
            set
            {
                _currentConditionsImage = value;
                RaiseAndSetIfChanged(nameof(CurrentConditionsImage));
            }
        } 

        private ObservableCollection<HourlyForecastItem> _hourlyForecastItems;
        public ObservableCollection<HourlyForecastItem> HourlyForecastItems
        {
            get => _hourlyForecastItems;
            set
            {
                _hourlyForecastItems = value;
                RaiseAndSetIfChanged(nameof(HourlyForecastItems));
            }
        }

        private WeatherService _weatherService;
        private WeatherLocationService _weatherLocationService;

        public HomeViewModel(WeatherService weatherService, WeatherLocationService weatherLocationService)
        {
            _weatherService = weatherService;
            _weatherLocationService = weatherLocationService;

            LocationName = "Grand Rapids";
        }

        public async Task FetchForecast()
        {
            Location currentLocation = await _weatherLocationService.GetLocation();

            if(currentLocation != null)
            {
                Models.Forecast forecast = await _weatherService.GetTodaysWeather(currentLocation);

                UpdateCurrentConditions(forecast);
                UpdateHourlyConditions(forecast.HourlyConditionsForDay);
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

        }
    }
}
