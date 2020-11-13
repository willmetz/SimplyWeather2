using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SimplyWeather2.Models;
using Xamarin.Essentials;

namespace SimplyWeather2.Api
{
    public interface WeatherApi
    {
        Task<WeatherForecast> GetForecast(SimplyWeatherLocation location);
        Task<CitiesInCircle> GetLocationName(SimplyWeatherLocation location);
    }

    public class WeatherApiImp : WeatherApi
    {

        private readonly HttpClient _httpClient;

        public WeatherApiImp()
        {
            _httpClient = new HttpClient(new HttpLogger(new HttpClientHandler()));
            _httpClient.BaseAddress = new UriBuilder("https://api.openweathermap.org/data/2.5/").Uri;
        }

        public async Task<WeatherForecast> GetForecast(SimplyWeatherLocation location)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["lat"] = location.Latitude.ToString();
            query["lon"] = location.Longitude.ToString();
            query["appid"] = AppConfig.OPEN_WEATHER_API_KEY;
            query["exclude"] = "minutely,alerts";
            query["units"] = "imperial";

            HttpResponseMessage response = await _httpClient.GetAsync("onecall?" + query.ToString());

            if (response?.IsSuccessStatusCode == true)
            {
                try
                {
                    string rawContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherForecast>(rawContent);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error decoding json: {e.Message}");
                }
            }

            return null;
        }

        public async Task<CitiesInCircle> GetLocationName(SimplyWeatherLocation location)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["lat"] = location.Latitude.ToString();
            query["lon"] = location.Longitude.ToString();
            query["appid"] = AppConfig.OPEN_WEATHER_API_KEY;
            query["cnt"] = "1";

            HttpResponseMessage response = await _httpClient.GetAsync("find?" + query.ToString());

            if (response?.IsSuccessStatusCode == true)
            {
                try
                {
                    string rawContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<CitiesInCircle>(rawContent);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error decoding json: {e.Message}");
                }
            }

            return null;
        }
    }
}
