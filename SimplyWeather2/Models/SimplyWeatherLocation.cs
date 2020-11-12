using System;
namespace SimplyWeather2.Models
{
    public enum LocationState
    {
        PermissionRequired,
        LocationUnknown,
        LocationUnavailable,
        LocationDisabled,
        LocationReady
    }
    public class SimplyWeatherLocation
    {

        public readonly double Latitude;
        public readonly double Longitude;
        public readonly LocationState State;

        private SimplyWeatherLocation(double latitude , double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            State = LocationState.LocationReady;
        }

        private SimplyWeatherLocation(LocationState locationState)
        {
            State = locationState;
        }

        public static SimplyWeatherLocation Unknown()
        {
            return new SimplyWeatherLocation(LocationState.LocationUnknown);
        }

        public static SimplyWeatherLocation PermissionRequired()
        {
            return new SimplyWeatherLocation(LocationState.PermissionRequired);
        }

        public static SimplyWeatherLocation Known(double latitude, double longitude)
        {
            return new SimplyWeatherLocation(latitude, longitude);
        }

        public static SimplyWeatherLocation Unavailable()
        {
            return new SimplyWeatherLocation(LocationState.LocationUnavailable);
        }

        public static SimplyWeatherLocation Disabled()
        {
            return new SimplyWeatherLocation(LocationState.LocationDisabled);
        }
    }
}
