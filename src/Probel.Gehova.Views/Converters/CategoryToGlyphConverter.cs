using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Probel.Gehova.Views.Converters
{
    public class CategoryToGlyphConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str)
            {
                if (str.Contains("drv")) { return Symbol.Contact2; }
                else if (str.Contains("phm")) { return Symbol.Contact; }
                else if (str.Contains("edu")) { return Symbol.AddFriend; }
                else { return Symbol.Help; }
            }
            else { return Symbol.Help; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();

        #endregion Methods
    }
}