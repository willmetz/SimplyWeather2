using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimplyWeather2.Services;
using SimplyWeather2.Views;

namespace SimplyWeather2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
    }
}
