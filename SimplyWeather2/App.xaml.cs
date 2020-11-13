using System;
using SimpleInjector;
using SimplyWeather2.Api;
using SimplyWeather2.Location;
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
            RegisterRoutes();

            MainPage = new AppShell();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("UpdateLocation", typeof(LocationPage));
        }

        private void RegisterServices()
        {
            _container.Register<WeatherService, WeatherServiceImp>(Lifestyle.Singleton);
            _container.Register<WeatherLocationService, WeatherLocationServiceImp>(Lifestyle.Singleton);
            _container.Register<WeatherApi, WeatherApiImp>(Lifestyle.Singleton);
            _container.Register<RadarService, RadarServiceImp>();
            _container.Register<AppPreferencesService, AppPreferencesServiceImp>(Lifestyle.Singleton);
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

        
    }
}
