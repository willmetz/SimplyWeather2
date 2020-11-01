using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimplyWeather2.Api;
using SimplyWeather2.Models;
using Xamarin.Essentials;

namespace SimplyWeather2.Services
{
    

    public interface WeatherService
    {
        Task<Models.Forecast> GetTodaysWeather(Location location);
    }

    public class WeatherServiceImp : WeatherService
    {
        private readonly WeatherApi _weatherApi;
        private readonly string _imagePrefix = "http://openweathermap.org/img/wn/";

        public WeatherServiceImp(WeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }


        public async Task<Models.Forecast> GetTodaysWeather(Location location)
        {
            WeatherForecast weatherForecast = await _weatherApi.GetForecast(location);

            if(weatherForecast != null)
            {
                List<HourlyConditions> hourlyConditionsForDay = GetHourlyConditionsForDay(weatherForecast.Hourly);
                Models.Forecast forecast = new Models.Forecast
                {
                    CurrentTemperature = weatherForecast.Current?.Temp != null ? (int)weatherForecast.Current.Temp : 0,
                    HighTemp = GetHighTempForDay(hourlyConditionsForDay),
                    LowTemp = GetLowempForDay(hourlyConditionsForDay),
                    CurrentWindSpeed = weatherForecast.Current?.WindSpeed != null ? (int)weatherForecast.Current.WindSpeed : 0,
                    FeelsLikeTemp = weatherForecast.Current?.FeelsLikeTemp != null ? (int)weatherForecast.Current.FeelsLikeTemp : 0,
                };

                if (weatherForecast.Current?.Conditions?.Count > 0 )
                {
                    forecast.CurrentConditionsImageUrl = GetUrlForImageIcon(weatherForecast.Current.Conditions[0].Icon);
                    forecast.CurrentConditions = weatherForecast.Current.Conditions[0].Description;
                }

                forecast.HourlyConditionsForDay = GetHourlyConditions(hourlyConditionsForDay);

                return forecast;
            }

            return null;
        }

        

        private List<HourlyConditions> GetHourlyConditionsForDay(List<HourlyConditions> hourlyConditions)
        {
            DateTime today = DateTime.Now;

            var todaysConditions = hourlyConditions.Where(hourlyCondition => {
                DateTime time = DateTimeOffset.FromUnixTimeSeconds(hourlyCondition.TimeStamp).UtcDateTime;
                return time.ToLocalTime().DayOfYear == today.DayOfYear;
            }).ToList();

            return todaysConditions;
        }


        private int GetHighTempForDay(List<HourlyConditions> hourlyConditions)
        {
            int highTemp = -999;

            foreach(HourlyConditions conditions in hourlyConditions)
            {
                if(conditions.Temperature > highTemp)
                {
                    highTemp = (int)conditions.Temperature;
                }
            }

            return highTemp;
        }

        private int GetLowempForDay(List<HourlyConditions> hourlyConditions)
        {
            int lowTemp = 999;

            foreach (HourlyConditions conditions in hourlyConditions)
            { 
                if (conditions.Temperature < lowTemp)
                {
                    lowTemp = (int)conditions.Temperature;
                }
            }

            return lowTemp;
        }

        private List<WeatherCondition> GetHourlyConditions(List<HourlyConditions> hourlyConditions)
        {
            List<WeatherCondition> weatherConditions = new List<WeatherCondition>();

            foreach(HourlyConditions hourlyCondition in hourlyConditions)
            {
                DateTime time = DateTimeOffset.FromUnixTimeSeconds(hourlyCondition.TimeStamp).UtcDateTime;

                weatherConditions.Add(new WeatherCondition
                {
                    Time = time.ToLocalTime(),
                    ImageUrl = GetUrlForImageIcon(hourlyCondition.Weather[0].Icon),
                    Temperature = (int)hourlyCondition.Temperature
                });
            }

            return weatherConditions;
        }

        private string GetUrlForImageIcon(string icon)
        {
            return _imagePrefix + icon + ".png";
        }
    }
}
