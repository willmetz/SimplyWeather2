using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplyWeather2.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool RaiseAndSetIfChanged<T>(T newValue, ref T property, [CallerMemberName] string propertyName = "")
        {

            bool didPropertyChange = !EqualityComparer<T>.Default.Equals(property, newValue);
            property = newValue;

            var changed = PropertyChanged;
            if (changed == null)
                return didPropertyChange;

            changed(this, new PropertyChangedEventArgs(propertyName));

            return didPropertyChange;
        }
        #endregion
    }
}
