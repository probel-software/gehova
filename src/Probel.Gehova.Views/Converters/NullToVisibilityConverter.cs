using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Probel.Gehova.Views.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => null;

        #endregion Methods
    }
}