﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimplyWeather2.Models;
using SimplyWeather2.Services;
using SimplyWeather2.ViewModels;
using Xamarin.Essentials;

namespace SimplyWeather2.Radar
{
    public class RadarViewModel : BaseViewModel
    {
        private string _mapUrl_UL;
        public string MapUrl_UL
        {
            get => _mapUrl_UL;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_UL, nameof(MapUrl_UL));
        }
        private string _radarUrl_UL;
        public string RadarUrl_UL
        {
            get => _radarUrl_UL;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_UL, nameof(RadarUrl_UL));
        }

        private string _mapUrl_UM;
        public string MapUrl_UM
        {
            get => _mapUrl_UM;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_UM, nameof(MapUrl_UM));
        }
        private string _radarUrl_UM;
        public string RadarUrl_UM
        {
            get => _radarUrl_UM;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_UM, nameof(RadarUrl_UM));
        }

        private string _mapUrl_UR;
        public string MapUrl_UR
        {
            get => _mapUrl_UR;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_UR, nameof(MapUrl_UR));
        }
        private string _radarUrl_UR;
        public string RadarUrl_UR
        {
            get => _radarUrl_UR;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_UR, nameof(RadarUrl_UR));
        }

        private string _mapUrl_L;
        public string MapUrl_L
        {
            get => _mapUrl_L;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_L, nameof(MapUrl_L));
        }
        private string _radarUrl_L;
        public string RadarUrl_L
        {
            get => _radarUrl_L;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_L, nameof(RadarUrl_L));
        }

        private string _mapUrl_M;
        public string MapUrl_M
        {
            get => _mapUrl_M;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_M, nameof(MapUrl_M));
        }
        private string _radarUrl_M;
        public string RadarUrl_M
        {
            get => _radarUrl_M;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_M, nameof(RadarUrl_M));
        }

        private string _mapUrl_R;
        public string MapUrl_R
        {
            get => _mapUrl_R;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_R, nameof(MapUrl_R));
        }
        private string _radarUrl_R;
        public string RadarUrl_R
        {
            get => _radarUrl_R;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_R, nameof(RadarUrl_R));
        }

        private string _mapUrl_LL;
        public string MapUrl_LL
        {
            get => _mapUrl_LL;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_LL, nameof(MapUrl_LL));
        }
        private string _radarUrl_LL;
        public string RadarUrl_LL
        {
            get => _radarUrl_LL;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_LL, nameof(RadarUrl_LL));
        }

        private string _mapUrl_LM;
        public string MapUrl_LM
        {
            get => _mapUrl_LM;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_LM, nameof(MapUrl_LM));
        }
        private string _radarUrl_LM;
        public string RadarUrl_LM
        {
            get => _radarUrl_LM;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_LM, nameof(RadarUrl_LM));
        }

        private string _mapUrl_LR;
        public string MapUrl_LR
        {
            get => _mapUrl_LR;
            set => RaiseAndSetIfChanged(value, ref _mapUrl_LR, nameof(MapUrl_LR));
        }
        private string _radarUrl_LR;
        public string RadarUrl_LR
        {
            get => _radarUrl_LR;
            set => RaiseAndSetIfChanged(value, ref _radarUrl_LR, nameof(RadarUrl_LR));
        }

        private int _currentZoomLevel;
        public int CurrentZoomLevel
        {
            get => _currentZoomLevel;
            set
            {
                bool changed = RaiseAndSetIfChanged(value, ref _currentZoomLevel, nameof(CurrentZoomLevel));
                if ( changed && _initialized)
                {
                    _appPreferencesService?.SetZoomLevel(value);
                    UpdateRadar();
                    UpdateZoomText(value);
                }
            }
        }

        private float _horizontalCenterLocation;
        public float HorizontalCenterLocation
        {
            get => _horizontalCenterLocation;
            set => RaiseAndSetIfChanged(value, ref _horizontalCenterLocation, nameof(HorizontalCenterLocation));
        }

        private float _verticalCenterLocation;
        public float VerticalCenterLocation
        {
            get => _verticalCenterLocation;
            set => RaiseAndSetIfChanged(value, ref _verticalCenterLocation, nameof(VerticalCenterLocation));
        } 

        private string _zoomText;
        public string ZoomText
        {
            get => _zoomText;
            set => RaiseAndSetIfChanged(value, ref _zoomText, nameof(ZoomText));
        }

        private bool _showLocationUnkonwn;
        public bool ShowLocationUnknown
        {
            get => _showLocationUnkonwn;
            set => RaiseAndSetIfChanged(value, ref _showLocationUnkonwn, nameof(ShowLocationUnknown));
        }

        private readonly int CENTER_TILE_INDEX = 4;

        private readonly RadarService _radarService;
        private readonly WeatherLocationService _weatherLocationService;
        private readonly AppPreferencesService _appPreferencesService;
        private bool _initialized;

        public RadarViewModel(RadarService radarService, WeatherLocationService weatherLocationService, AppPreferencesService appPreferencesService)
        {
            _initialized = false;
            _radarService = radarService;
            _weatherLocationService = weatherLocationService;
            _appPreferencesService = appPreferencesService;

            CurrentZoomLevel = _appPreferencesService.GetZoomLevel();
            UpdateZoomText(CurrentZoomLevel);
        }

        public void UpdateRadar()
        {
            SimplyWeatherLocation location = _weatherLocationService.GetCurrentLocation();

            if(location.State == LocationState.LocationReady)
            {
                List<RadarTile> radarTiles = _radarService.GetRadarTiles(location, CurrentZoomLevel);

                UpdateMapImages(radarTiles);

                _initialized = true;

                ShowLocationUnknown = false;
            }
            else
            {
                ShowLocationUnknown = true;
            }
        }

        private void UpdateZoomText(int zoomLevel)
        {
            ZoomText = $"Zoom Level = {zoomLevel}";
        }

        private void UpdateMapImages(List<RadarTile> radarTiles)
        {
            MapUrl_UL = radarTiles[0].MapUrl;
            MapUrl_UM = radarTiles[1].MapUrl;
            MapUrl_UR = radarTiles[2].MapUrl;

            MapUrl_L = radarTiles[3].MapUrl;
            MapUrl_M = radarTiles[4].MapUrl;
            MapUrl_R = radarTiles[5].MapUrl;

            MapUrl_LL = radarTiles[6].MapUrl;
            MapUrl_LM = radarTiles[7].MapUrl;
            MapUrl_LR = radarTiles[8].MapUrl;


            RadarUrl_UL = radarTiles[0].RadarUrl;
            RadarUrl_UM = radarTiles[1].RadarUrl;
            RadarUrl_UR = radarTiles[2].RadarUrl;

            RadarUrl_L = radarTiles[3].RadarUrl;
            RadarUrl_M = radarTiles[4].RadarUrl;
            RadarUrl_R = radarTiles[5].RadarUrl;

            RadarUrl_LL = radarTiles[6].RadarUrl;
            RadarUrl_LM = radarTiles[7].RadarUrl;
            RadarUrl_LR = radarTiles[8].RadarUrl;

            HorizontalCenterLocation = 1/3f + GetDecimal(radarTiles[CENTER_TILE_INDEX].coordinate.X) * (1/3f);
            VerticalCenterLocation = 1/3f + GetDecimal(radarTiles[CENTER_TILE_INDEX].coordinate.Y) * (1/3f);
        }


        private float GetDecimal(double number)
        {
            return (float)Math.Abs(number) - Math.Abs((int)number);
        }
    }
}
