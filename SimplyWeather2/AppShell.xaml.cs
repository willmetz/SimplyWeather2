using SimplyWeather2.Forecast;
using SimplyWeather2.Home;
using SimplyWeather2.Radar;
using Xamarin.Forms;

namespace SimplyWeather2
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Radar", typeof(RadarPage));
            Routing.RegisterRoute("Home", typeof(HomePage));
            Routing.RegisterRoute("Forecast", typeof(ForecastPage));
        }

    }
}
