using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SimplyWeather2.Controls
{
    public class MarkerView : SKCanvasView
    {
        public static readonly BindableProperty HorizontalOffsetProperty = BindableProperty.Create("HorizontalOffset", typeof(float), typeof(MarkerView), 0.5f, propertyChanged:OnOffsetChanged);
        public static readonly BindableProperty VerticalOffsetProperty = BindableProperty.Create("VerticalOffset", typeof(float), typeof(MarkerView), 0.5f, propertyChanged:OnOffsetChanged);
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create("MarkerColor", typeof(Color), typeof(MarkerView), Color.Red);

        private static void OnOffsetChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MarkerView view)
            {
                view.InvalidateSurface();
            }
        }


        public float HorizontalOffset
        {
            get => (float)GetValue(HorizontalOffsetProperty);
            set
            {
                SetValue(HorizontalOffsetProperty, value);
                InvalidateSurface();
            }
        }

        public float VerticalOffset
        {
            get => (float)GetValue(VerticalOffsetProperty);
            set
            {
                SetValue(VerticalOffsetProperty, value);
                InvalidateSurface();
            }
        }

        public Color MarkerColor
        {
            get => (Color)GetValue(MarkerColorProperty);
            set
            {
                SetValue(MarkerColorProperty, value);
                _markerPaint.Color = value.ToSKColor();
                InvalidateSurface();
            }
        }

        private SKPaint _markerPaint;
        private SKPaint _innerCirclePaint;

        public MarkerView()
        {
            _markerPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = MarkerColor.ToSKColor(),
                StrokeWidth = 1
            };

            _innerCirclePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.White.ToSKColor(),
                StrokeWidth = 1
            };
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(e.Info.Width * HorizontalOffset, e.Info.Height * VerticalOffset);
            float radius = 60;
            float sweepAngle = 40;
            
            SKRect circleContainer = new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);

            SKPath markerPath = new SKPath();

            markerPath.MoveTo(center);
            markerPath.ArcTo(circleContainer, 270 - sweepAngle/2f, sweepAngle, false);
            markerPath.Close();
            canvas.DrawPath(markerPath, _markerPaint);

            //draw top circle
            float arcLength = (float)Math.PI * 2 * radius * (sweepAngle / 360f );
            float innerCircleRadius = arcLength / 2f;
            canvas.DrawCircle(center.X, center.Y - radius, innerCircleRadius, _markerPaint);


            //draw inner circle
            canvas.DrawCircle(center.X, center.Y - radius, innerCircleRadius / 3f, _innerCirclePaint);
        }

    }
}

