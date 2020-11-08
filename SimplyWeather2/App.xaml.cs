using SimpleInjector;
using SimplyWeather2.Api;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2
{
    public partial class App : Application
    {
        private Container _container;

        public TService GetService<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public App()
        {
            InitializeComponent();

            _container = new Container();
            RegisterServices();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterServices()
        {
            _container.Register<WeatherService, WeatherServiceImp>(Lifestyle.Singleton);
            _container.Register<WeatherLocationService, WeatherLocationServiceImp>(Lifestyle.Singleton);
            _container.Register<WeatherApi, WeatherApiImp>(Lifestyle.Singleton);
            _container.Register<RadarService, RadarServiceImp>();
        }
    }
}
