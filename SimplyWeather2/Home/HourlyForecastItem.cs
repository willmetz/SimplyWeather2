using System;
namespace SimplyWeather2.Home
{
    public class HourlyForecastItem
    {
        private string _temperature;
        public string Temperature {
            get
            {
                return $"{_temperature} degrees";
            }
            set
            {
                _temperature = value;
            }
        }

        private string _timeOfDay;
        public string TimeOfDay
        {
            get => "10pm";   
        }

        private string _imageUrl;
        public string ImageUrl {
            get => "image";
        }


    }
}
