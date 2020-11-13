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
        Task<Models.Forecast> GetTodaysWeather(SimplyWeatherLocation location);
        Task<List<DayForecast>> GetExtendedForecast(SimplyWeatherLocation location);
        Task<string> GetForecastLocationName(SimplyWeatherLocation location);
    }

    public class WeatherServiceImp : WeatherService
    {
        private readonly WeatherApi _weatherApi;
        private readonly string _imagePrefix = "http://openweathermap.org/img/wn/";

        public WeatherServiceImp(WeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }


        public async Task<Models.Forecast> GetTodaysWeather(SimplyWeatherLocation location)
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

        public async Task<List<DayForecast>> GetExtendedForecast(SimplyWeatherLocation location)
        {
            WeatherForecast weatherForecast = await _weatherApi.GetForecast(location);

            if(weatherForecast != null)
            {
                List<DayForecast> daysForecast = GetDayForecast(weatherForecast.Daily);

                return daysForecast;
            }

            return null;
        }

        private List<DayForecast> GetDayForecast(List<DailyConditions> dailyConditions)
        {
            List<DayForecast> forecast = new List<DayForecast>();

            foreach(DailyConditions conditions in dailyConditions)
            {
                DateTime time = DateTimeOffset.FromUnixTimeSeconds(conditions.Timestamp).UtcDateTime;

                forecast.Add(new DayForecast
                {
                    TimeStamp = time.ToLocalTime(),
                    HiTemp = conditions.Temperatures?.Max != null ? (int)conditions.Temperatures.Max : 0,
                    LowTemp = conditions.Temperatures?.Min != null ? (int)conditions.Temperatures.Min : 0,
                    WindSpeed = (int)conditions.WindSpeed,
                    WindDirection = CardinalDirection(conditions.WindDirection),
                    ImageUrl = GetUrlForImageIcon(conditions.Weather[0].Icon),
                    ConditionsDescription = conditions.Weather[0].Description
                }) ;
            }

            return forecast;
        }

        private string CardinalDirection(int windDirection)
        {
            if( windDirection >= 23 && windDirection < 68)
            {
                return "NE";
            }
            else if(windDirection >= 68 && windDirection < 113)
            {
                return "N";
            }
            else if(windDirection >= 113 && windDirection < 158)
            {
                return "NW";
            }
            else if(windDirection >= 158 && windDirection < 203)
            {
                return "W";
            }
            else if (windDirection >= 203 && windDirection < 248)
            {
                return "SW";
            }
            else if (windDirection >= 248 && windDirection < 293)
            {
                return "S";
            }
            else if (windDirection >= 293 && windDirection < 338)
            {
                return "SE";
            }

            return "E";
        }

        public async Task<string> GetForecastLocationName(SimplyWeatherLocation location)
        {
            CitiesInCircle citiesInCircle = await _weatherApi.GetLocationName(location);

            if(citiesInCircle != null && citiesInCircle.Cities?.Count > 0)
            {
                return citiesInCircle.Cities.FirstOrDefault().CityName;
            }

            return "Unknown";
        }
    }
}
