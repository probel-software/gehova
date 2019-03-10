using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Probel.Gehova.Views.Converters
{
    public class CategoryToGlyphConverter : IValueConverter
    {
        #region Fields

        private readonly FontFamily _fontFamily = new FontFamily("Segoe MDL2 Assets");

        #endregion Fields

        #region Methods

        private FontIcon Get(string glyph)
        {
            var icon = new FontIcon
            {
                FontFamily = _fontFamily,
                Glyph = glyph,
            };
            return icon;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str)
            {
                str = str.ToLower();

                var icons = new List<FontIcon>();
                if (str.Contains("edu")) { icons.Add(Get("\uE890")); }
                if (str.Contains("drv")) { icons.Add(Get("\uE804")); }

                return icons;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();

        #endregion Methods
    }
}