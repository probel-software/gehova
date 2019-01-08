using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace Probel.Gehova.Views.Converters
{
    internal class DateToLongDateStringConverter : IValueConverter
    {
        #region Properties

        private ResourceLoader Resources => new ResourceLoader("Messages");

        #endregion Properties

        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dt) { return dt.ToLongDateString(); }
            else { return Resources.GetString("Error_NotAValidDate"); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();

        #endregion Methods
    }
}