using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace Probel.Gehova.Views.Converters
{
    public class EducatorToBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var result = (value is bool isEducator) ? FontWeights.Bold : FontWeights.Light;
            var result = FontWeights.Light;
            if (value is bool isEducator)
            {
                result = isEducator ? FontWeights.Bold : FontWeights.Light;
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
