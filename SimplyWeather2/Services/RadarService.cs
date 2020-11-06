using System;
using System.Collections.Generic;
using SimplyWeather2.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SimplyWeather2.Services
{
    public interface RadarService
    {
        List<RadarTile> GetRadarTiles(Location location, int zoom);
    }


    public class RadarServiceImp : RadarService
    {
        public RadarServiceImp()
        {
        }

        public List<RadarTile> GetRadarTiles(Location location, int zoom)
        {
            Point centerTileCoordinates = GetCenterCoordinate(location, zoom);

            RadarTile centerTile = new RadarTile()
            {
                coordinate = centerTileCoordinates,
                MapUrl = GetMapUrl(centerTileCoordinates, zoom),
                RadarUrl = GetRadarUrl(centerTileCoordinates, zoom)
            };
        }


        private Point GetCenterCoordinate(Location location, int zoom)
        {
            double n = Math.Pow(2, zoom);
            double xTile = n * ((location.Longitude + 180.0) / 360.0);

            //ytile = int((1.0 - math.asinh(math.tan(lat_rad)) / math.pi) / 2.0 * n)
            double latInRads = DegreesToRadians(location.Latitude);
            double yTile = ((1.0 - asinh(Math.Tan(latInRads)) / Math.PI) / 2.0 * n);

            return new Point(xTile, yTile);
        }

        private string GetMapUrl(Point coordinate, int zoom)
        {
            return $"https://api.mapbox.com/styles/v1/mapbox/streets-v11/tiles/{zoom}/{(int)coordinate.X}/{(int)coordinate.Y}?access_token={AppConfig.MAP_BOX_API_KEY}";
        }

        private string GetRadarUrl(Point coordinate, int zoom)
        {
            return $"https://tile.openweathermap.org/map/precipitation_new/{zoom}/{(int)coordinate.X}/{(int)coordinate.Y}.png?appid={AppConfig.OPEN_WEATHER_API_KEY}";
        }

        private double DegreesToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180.0;
        }

        private double asinh(double value)
        {
            int sign = value < 0 ? -1 : 1;

            //credit from: https://pub.dev/documentation/dart_numerics/latest/dart_numerics/asinh.html
            if (Math.Abs(value) >= 268435456.0) // 2^28, taken from freeBSD
                return sign * (Math.Log(Math.Abs(value)) + Math.Log(2.0));

            return sign * Math.Log(Math.Abs(value) + Math.Sqrt((value * value) + 1));
        }
    }
}
