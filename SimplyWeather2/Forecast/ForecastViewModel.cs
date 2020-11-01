using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;

namespace SimplyWeather2.Forecast
{
    public class ForecastViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;

        private ObservableCollection<DailyForecastItem> _dailyForecasts;
        public ObservableCollection<DailyForecastItem> DailyForecasts
        {
            get => _dailyForecasts;
            set => RaiseAndSetIfChanged(value, ref _dailyForecasts, nameof(DailyForecasts));
        }

        public ForecastViewModel(WeatherService weatherService)
        {
            _weatherService = weatherService;


        }

        public async Task UpdateExtendedForecast()
        {
            //dummy data
            DailyForecasts = new ObservableCollection<DailyForecastItem>
            {
                new DailyForecastItem(),
                new DailyForecastItem(),
                new DailyForecastItem(),
                new DailyForecastItem(),
                new DailyForecastItem(),
                new DailyForecastItem(),
                new DailyForecastItem(),
            };
        }
    }
}
