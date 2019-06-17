using Probel.Gehova.ViewModels.Vm.AppSettings;
using Windows.Storage;

namespace Probel.Gehova.Views.Helpers
{
    public class AppSettings : ISettings
    {
        #region Fields

        private const string HeaderContentHeight = "ContentHeight";
        private const string HeaderContentIconVisibility = "contentIconVisibility";
        private const string HeaderContentTextSize = "contentTextSize";
        private const string HeaderDayFontSize = "dayFontSize";
        private const string HeaderTeamMinWidth = "teamMinWidth";

        private readonly ApplicationDataContainer _ls = ApplicationData.Current.LocalSettings;

        #endregion Fields

        #region Properties

        public double ContentHeight
        {
            get
            {
                var contentHeight = _ls.Values[HeaderContentHeight];
                return (contentHeight == null) ? 20 : (double)contentHeight;
            }
            set => _ls.Values[HeaderContentHeight] = value;
        }

        public bool ContentIconVisibility
        {
            get
            {
                var contentIconVisibility = _ls.Values[HeaderContentIconVisibility];
                return (contentIconVisibility == null) ? true : (bool)contentIconVisibility;
            }
            set => _ls.Values[HeaderContentIconVisibility] = value;
        }

        public double ContentTextSize
        {
            get
            {
                var contentTextSize = _ls.Values[HeaderContentTextSize];
                return (contentTextSize == null) ? 12 : (double)contentTextSize;
            }
            set => _ls.Values[HeaderContentTextSize] = value;
        }

        public double DayFontSize
        {
            get
            {
                var dayFontSize = _ls.Values[HeaderDayFontSize];
                return (dayFontSize == null) ? 15 : (double)dayFontSize;
            }
            set => _ls.Values[HeaderDayFontSize] = value;
        }

        public double TeamMinWidth
        {
            get
            {
                var teamMinWidth = _ls.Values[HeaderTeamMinWidth];
                return (double)(teamMinWidth ?? 200d);
            }
            set => _ls.Values[HeaderTeamMinWidth] = value;
        }

        #endregion Properties
    }
}
