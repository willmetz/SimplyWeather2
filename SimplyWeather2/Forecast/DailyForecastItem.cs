using System;
using SimplyWeather2.Models;

namespace SimplyWeather2.Forecast
{
    public class DailyForecastItem
    {
        public string CalendarDay { get; private set; }
        public string HiTemp { get; private set; }
        public string LowTemp { get; private set; }
        public string WindSpeed { get; private set; }
        public string ImageUrl { get; private set; }
        public string ConditionDescription { get; private set; }

        public DailyForecastItem(DayForecast forecast)
        {
            ConditionDescription = forecast.ConditionsDescription;
            ImageUrl = forecast.ImageUrl;
            WindSpeed = $"{forecast.WindSpeed} mph ({forecast.WindDirection})";
            HiTemp = $"Hi Temp: {forecast.HiTemp}";
            LowTemp = $"Low Temp: {forecast.LowTemp}";
            CalendarDay = forecast.TimeStamp.ToString("dddd, MMMM d");
        }
    }
}
