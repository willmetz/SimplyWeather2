using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SimplyWeather2.Models
{
    public class Forecast
    {
        public int CurrentTemperature;
        public int CurrentWindSpeed;
        public string CurrentConditions;
        public string CurrentWindDirection;
        public string CurrentConditionsImageUrl;
        public int FeelsLikeTemp;
        public int HighTemp;
        public int LowTemp;
        public string LocationName;

        public List<WeatherCondition> HourlyConditions;
    }

    public class WeatherCondition
    {
        public DateTime Time;
        public int Temperature;
        public string ImageUrl;
    }
}
