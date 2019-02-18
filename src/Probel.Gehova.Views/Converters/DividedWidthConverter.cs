using System;
using Windows.UI.Xaml.Data;

namespace Probel.Gehova.Views.Converters
{
    public class DividedWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double width)
            {
                if (parameter is string param && double.TryParse(param, out var columns))
                {                    
                    return width / columns;
                }
                else { return width; }
            }
            else { return 100; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
