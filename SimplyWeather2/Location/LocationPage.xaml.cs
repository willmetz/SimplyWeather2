using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Location
{
    public partial class LocationPage : ContentPage
    {
        LocationViewModel _viewModel;

        public LocationPage()
        {
            InitializeComponent();

            App myApp = (App)Application.Current;

            WeatherLocationService locationService = myApp.GetService<WeatherLocationService>();
            AppPreferencesService appPreferencesService = myApp.GetService<AppPreferencesService>();
            WeatherService weatherService = myApp.GetService<WeatherService>();

            _viewModel = new LocationViewModel(locationService, appPreferencesService, weatherService);

            BindingContext = _viewModel;
        }
    }
}
