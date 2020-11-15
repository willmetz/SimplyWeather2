using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SimplyWeather2.Models;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;
using Xamarin.Forms;

namespace SimplyWeather2.Location
{
    public class LocationViewModel : BaseViewModel
    {

        private string _currentLocation;
        public string CurrentLocation
        {
            get => _currentLocation;
            set => RaiseAndSetIfChanged(value, ref _currentLocation, nameof(CurrentLocation));
        }

        public ICommand OnUpdateLocation { get; private set; }

        private readonly WeatherLocationService _locationService;
        private readonly AppPreferencesService _appPreferenceService;
        private readonly WeatherService _weatherService;

        public LocationViewModel(WeatherLocationService locationService, AppPreferencesService appPreferencesService, WeatherService weatherService)
        {
            _locationService = locationService;
            _appPreferenceService = appPreferencesService;
            _weatherService = weatherService;
            OnUpdateLocation = new Command(async () => await LocationUpdateRequested());

            if(_appPreferenceService.GetLocationName() != string.Empty)
            {
                CurrentLocation = _appPreferenceService.GetLocationName();
            }
            else
            {
                CurrentLocation = "Unknown";
            }

        }

        private async Task LocationUpdateRequested()
        {
            SimplyWeatherLocation location =  await _locationService.UpdateLocation(new System.Threading.CancellationTokenSource());

            if(location.State == LocationState.LocationReady)
            {
                //fetch location info from weather API
                string cityName = await _weatherService.GetForecastLocationName(location);

                //update prefs with location info
                _appPreferenceService.SetLocationName(cityName);

                //update the UI
                CurrentLocation = cityName;
            }
            
        }
    }
}
