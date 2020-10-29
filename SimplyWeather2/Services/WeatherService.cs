﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
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
        private HttpClient _httpClient;

        public WeatherServiceImp()
        {
            _httpClient = new HttpClient();
        }


        public async Task<Models.Forecast> GetTodaysWeather(Location location)
        {
            WeatherForecast weatherForecast = await GetForecast(location);

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
                    forecast.CurrentConditionsImageUrl = weatherForecast.Current.Conditions[0].Icon;
                    forecast.CurrentConditions = weatherForecast.Current.Conditions[0].Description;
                }

                forecast.HourlyConditionsForDay = GetHourlyConditions(hourlyConditionsForDay);

                return forecast;
            }

            return null;
        }

        private async Task<WeatherForecast> GetForecast(Location location)
        {
            UriBuilder uriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5/onecall");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["lat"] = location.Latitude.ToString();
            query["lon"] = location.Longitude.ToString();
            query["appid"] = AppConfig.OPEN_WEATHER_API_KEY;
            query["exclude"] = "minutely,alerts";
            query["units"] = "imperial";
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);

            if(response?.IsSuccessStatusCode == true)
            {
                try
                {
                    string rawContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherForecast>(rawContent);
                }
                catch(Exception e)
                {
                    //TODO - handle things here
                    throw e;
                }
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
                    ImageUrl = hourlyCondition.Weather[0].Icon,
                    Temperature = (int)hourlyCondition.Temperature
                });
            }

            return weatherConditions;
        }
    }
}
