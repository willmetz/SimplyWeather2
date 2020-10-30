using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimplyWeather2.Api
{
    public class HttpLogger : DelegatingHandler
    {
        public HttpLogger(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Request:");
            Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine("Response:");
            Debug.WriteLine(response.ToString());
            if (response.Content != null)
            {
                string rawResponse = await response.Content.ReadAsStringAsync();
                string formattedResponse = JToken.Parse(rawResponse).ToString(Formatting.Indented);

                Debug.WriteLine(formattedResponse);
            }

            return response;
        }
    }
}
