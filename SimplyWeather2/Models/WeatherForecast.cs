using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimplyWeather2.Models
{
    public class WeatherForecast
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }

        [JsonProperty("current")]
        public CurrentConditions Current { get; set; }

        [JsonProperty("hourly")]
        public List<HourlyConditions> Hourly { get; set; }

        [JsonProperty("daily")]
        public List<DailyConditions> Daily { get; set; }
    }
}
