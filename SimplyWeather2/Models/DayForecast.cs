using System;
namespace SimplyWeather2.Models
{
    public class DayForecast
    {
        public DateTime TimeStamp;
        public int HiTemp;
        public int LowTemp;
        public int WindSpeed;
        public string WindDirection;
        public string ImageUrl;
        public string ConditionsDescription;
    }
}
