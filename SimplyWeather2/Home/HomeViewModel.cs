using System;
using System.Collections.Generic;
using SimplyWeather2.ViewModels;

namespace SimplyWeather2.Home
{
    public class HomeViewModel : BaseViewModel
    {

        private string _locationName;
        public string LocationName
        {
            get => _locationName;
            set {
                _locationName = value;
                RaiseAndSetIfChanged(nameof(LocationName));
            }
        }

        private List<HourlyForecastItem> _hourlyForecastItems;
        public List<HourlyForecastItem> HourlyForecastItems
        {
            get => _hourlyForecastItems;
            set {
                _hourlyForecastItems = value;
                RaiseAndSetIfChanged(nameof(HourlyForecastItems));
            }
        }

        public HomeViewModel()
        {
            HourlyForecastItems = new List<HourlyForecastItem>
            {
                new HourlyForecastItem{Temperature = "55" },
                new HourlyForecastItem{Temperature = "56" },
                new HourlyForecastItem{Temperature = "57" },
                new HourlyForecastItem{Temperature = "58" }
            };

            LocationName = "Grand Rapids";
        }
    }
}
