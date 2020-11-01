using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplyWeather2.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaiseAndSetIfChanged<T>(T newValue, ref T property, [CallerMemberName] string propertyName = "")
        {
            property = newValue;

            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
