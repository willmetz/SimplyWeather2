using System;
using System.Collections.Generic;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Radar
{
    public partial class RadarPage : ContentPage
    {
        private RadarViewModel _viewModel;
        private double _screenWidth = -1;

        public RadarPage()
        {
            InitializeComponent();

            App app = ((App)Application.Current);
            RadarService radarService = app.GetService<RadarService>();
            WeatherLocationService weatherLocationService = app.GetService<WeatherLocationService>();
            AppPreferencesService appPreferencesService = app.GetService<AppPreferencesService>();

            _viewModel = new RadarViewModel(radarService, weatherLocationService, appPreferencesService);

            BindingContext = _viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.UpdateRadar();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if(_screenWidth < 0)
            {
                _screenWidth = width;
                UpdateGridRowWidths();
            }
            base.OnSizeAllocated(width, height);
        }

        private void UpdateGridRowWidths()
        {
            var widthHeight = _screenWidth / 3;

            foreach (ColumnDefinition def in MapGrid.ColumnDefinitions)
            {                
                def.Width = widthHeight;
            }
        }
    }
}
