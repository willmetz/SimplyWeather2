using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SimplyWeather2.Models
{
    public class CitiesInCircle
    {
        [JsonProperty("list")]
        public List<CityInfo> Cities;
    }

    public class CityInfo
    {
        [JsonProperty("name")]
        public string CityName { get; set; }
    }
}
