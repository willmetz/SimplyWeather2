using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using SimplyWeather2.Models;
using Xamarin.Essentials;

namespace SimplyWeather2.Services
{
    public interface WeatherLocationService
    {
        SimplyWeatherLocation GetCurrentLocation();
        Task<SimplyWeatherLocation> UpdateLocation(CancellationTokenSource cts);
    }

    public class WeatherLocationServiceImp : WeatherLocationService
    {
        private readonly AppPreferencesService _appPrefs;

        public WeatherLocationServiceImp(AppPreferencesService appPreferencesService)
        {
            _appPrefs = appPreferencesService;
        }

        public async Task<SimplyWeatherLocation> UpdateLocation(CancellationTokenSource cts)
        {
            SimplyWeatherLocation location = SimplyWeatherLocation.Unavailable();

            try
            {
                location = await GetLastKnownLocation();
            }
            catch (Exception)
            {
                Debug.WriteLine("Error retrieving last known location");
            }

            if(location?.State != LocationState.LocationReady)
            {
                Debug.WriteLine("Last known location is not available, getting new location");
                location = await FetchNewLocation(cts);
            }

            if(location.State == LocationState.LocationReady)
            {
                _appPrefs.SetLocationInfo(location);
            }

            return location;
        }

        private async Task<SimplyWeatherLocation> GetLastKnownLocation()
        {
            try
            {
                Xamarin.Essentials.Location lastKnownLocation = await Geolocation.GetLastKnownLocationAsync();

                if(lastKnownLocation != null)
                {
                    return SimplyWeatherLocation.Known(lastKnownLocation.Latitude, lastKnownLocation.Longitude);
                }
            }
            catch (FeatureNotSupportedException)
            {
                return SimplyWeatherLocation.Unavailable();
            }
            catch (FeatureNotEnabledException)
            {
                return SimplyWeatherLocation.Disabled();
            }
            catch (PermissionException)
            {
                return SimplyWeatherLocation.PermissionRequired();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error retrieving last known location: {e.StackTrace}");
                return SimplyWeatherLocation.Unknown();
            }

            throw new Exception("Unable to get Last location");

        }

        private async Task<SimplyWeatherLocation> FetchNewLocation(CancellationTokenSource cts)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(15));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException)
            {
                return SimplyWeatherLocation.Unavailable();
            }
            catch (FeatureNotEnabledException)
            {
                return SimplyWeatherLocation.Disabled();
            }
            catch (PermissionException)
            {
                return SimplyWeatherLocation.PermissionRequired();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving location: {ex.StackTrace}");                
            }

            return SimplyWeatherLocation.Unknown();
        }

        public SimplyWeatherLocation GetCurrentLocation()
        {
            return _appPrefs.GetLocation();
        }
    }

    
}
