using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SimplyWeather2.Services
{
    public interface WeatherLocationService
    {
        Task<Location> GetLocation();
    }

    public class WeatherLocationServiceImp : WeatherLocationService
    {
        public Task<Location> GetLocation()
        {
            return Task.FromResult(new Location(42.9564627, -85.7301288));
        }
    }
}
