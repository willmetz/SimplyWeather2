using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimplyWeather2.Models
{
    public class CurrentConditions
    {
        [JsonProperty("dt")]
        public int TimeStamp { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLikeTemp { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        [JsonProperty("uvi")]
        public double UvIndex { get; set; }

        [JsonProperty("clouds")]
        public int CloudinessPercent { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public int WindDirection { get; set; }

        [JsonProperty("weather")]
        public IList<Weather> Conditions { get; set; }
    }
}
