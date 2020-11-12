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
    }

    public class AppPreferencesServiceImp
    {
        private readonly string LatitudeKey = "Latitude";
        private readonly string LongitudeKey = "Longitude";
        private readonly string LocationNameKey = "LocationNameKey";

        public void SetLocationInfo(SimplyWeatherLocation location)
        {
            Preferences.Set(LatitudeKey, location.Latitude);
            Preferences.Set(LongitudeKey, location.Longitude);
        }

        public void SetLocationName(string locationName)
        {
            Preferences.Set(LocationNameKey, locationName);
        }

        public SimplyWeatherLocation GetLocation()
        {
            const double Unknown = -9999;
            double longitude = Preferences.Get(LongitudeKey, Unknown);
            double latitude = Preferences.Get(LatitudeKey, Unknown);

            if(latitude != Unknown && longitude != Unknown)
            {
                return SimplyWeatherLocation.Known(latitude, longitude);
            }

            return SimplyWeatherLocation.Unknown();
        }

        public string GetLocationName()
        {
            return Preferences.Get(LocationNameKey, string.Empty);
        }
    }
}
