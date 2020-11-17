using System;
using System.Runtime.InteropServices;
using SimplyWeather2.Models;
using Xamarin.Essentials;

namespace SimplyWeather2.Services
{
    public interface AppPreferencesService
    {
        void SetLocationInfo(SimplyWeatherLocation location);
        void SetLocationName(string locationName);
        SimplyWeatherLocation GetLocation();
        string GetLocationName();
        int GetZoomLevel();
        void SetZoomLevel(int zoom);
    }

    public class AppPreferencesServiceImp : AppPreferencesService
    {
        private readonly string _latitudeKey = "Latitude";
        private readonly string _longitudeKey = "Longitude";
        private readonly string _locationNameKey = "LocationNameKey";
        private readonly string _zoomKey = "ZoomKey";

        public void SetLocationInfo(SimplyWeatherLocation location)
        {
            Preferences.Set(_latitudeKey, location.Latitude);
            Preferences.Set(_longitudeKey, location.Longitude);
        }

        public void SetLocationName(string locationName)
        {
            Preferences.Set(_locationNameKey, locationName);
        }

        public SimplyWeatherLocation GetLocation()
        {
            const double Unknown = -9999;
            double longitude = Preferences.Get(_longitudeKey, Unknown);
            double latitude = Preferences.Get(_latitudeKey, Unknown);

            if(latitude != Unknown && longitude != Unknown)
            {
                return SimplyWeatherLocation.Known(latitude, longitude);
            }

            return SimplyWeatherLocation.Unknown();
        }

        public string GetLocationName()
        {
            return Preferences.Get(_locationNameKey, string.Empty);
        }

        public int GetZoomLevel()
        {
            return Preferences.Get(_zoomKey, 5);
        }

        public void SetZoomLevel(int zoom)
        {
            Preferences.Set(_zoomKey, zoom);
        }
    }
}
