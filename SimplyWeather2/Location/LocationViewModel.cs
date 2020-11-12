using System;
using System.Threading.Tasks;
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

        public Command OnUpdateLocation;

        private readonly WeatherLocationService _locationService;
        private readonly AppPreferencesService _appPreferenceService;

        public LocationViewModel(WeatherLocationService locationService, AppPreferencesService appPreferencesService)
        {
            _locationService = locationService;
            _appPreferenceService = appPreferencesService;
            OnUpdateLocation = new Command(async () => await LocationUpdateRequested());

        }

        private async Task LocationUpdateRequested()
        {
            SimplyWeatherLocation location =  await _locationService.UpdateLocation(new System.Threading.CancellationTokenSource());

            //fetch location info from weather API

            //update prefs with location info

            //update the UI
        }
    }
}
