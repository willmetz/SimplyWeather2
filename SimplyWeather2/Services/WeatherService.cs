using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SimplyWeather2.Models;
using Xamarin.Essentials;

namespace SimplyWeather2.Services
{
    public interface WeatherService
    {
        Task<WeatherForecast> GetForecast(Location location);
    }

    public class WeatherServiceImp : WeatherService
    {
        private HttpClient _httpClient;

        public WeatherServiceImp()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openweathermap.org/data/2.5")
            };
        }

        public async Task<WeatherForecast> GetForecast(Location location)
        {
            UriBuilder uriBuilder = new UriBuilder("/onecall");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["lat"] = location.Latitude.ToString();
            query["lon"] = location.Longitude.ToString();
            query["appid"] = "";
            query["exclude"] = "minutely,alerts";
            uriBuilder.Query = query.ToString();
            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);

            if(response?.IsSuccessStatusCode == true)
            {
                try
                {
                    string rawContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<WeatherForecast>(rawContent);
                }
                catch(Exception)
                {
                    //TODO - handle things here
                }
            }

            return null;
        }
    }
}
