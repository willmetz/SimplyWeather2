using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimplyWeather2.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HourlyForecastCell : Grid
    {
        public HourlyForecastCell()
        {
            InitializeComponent();
        }
    }
}
