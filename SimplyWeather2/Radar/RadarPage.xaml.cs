using System;
using System.Collections.Generic;
using SimplyWeather2.Services;
using Xamarin.Forms;

namespace SimplyWeather2.Radar
{
    public partial class RadarPage : ContentPage
    {
        private RadarViewModel _viewModel;
        private double _screenWidth;

        public RadarPage()
        {
            InitializeComponent();

            App app = ((App)Application.Current);
            RadarService radarService = app.GetService<RadarService>();
            WeatherLocationService weatherLocationService = app.GetService<WeatherLocationService>();

            _viewModel = new RadarViewModel(radarService, weatherLocationService);

            BindingContext = _viewModel;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.UpdateRadar();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            _screenWidth = width;
            UpdateGridRowWidths();
            base.OnSizeAllocated(width, height);
        }

        private void UpdateGridRowWidths()
        {
            foreach (ColumnDefinition def in MapGrid.ColumnDefinitions)
            {
                def.Width = _screenWidth / 3;
            }
        }
    }
}
