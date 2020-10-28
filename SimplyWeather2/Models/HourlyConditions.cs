using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimplyWeather2.Models
{
    public class HourlyConditions
    {
        [JsonProperty("dt")]
        public long TimeStamp { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLikeTemperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        [JsonProperty("clouds")]
        public int CloudinessPercentage { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public int WindDirection { get; set; }

        [JsonProperty("weather")]
        public IList<Weather> Weather { get; set; }

        [JsonProperty("pop")]
        public double PropbabilityOfPercipitation { get; set; }
    }

}
