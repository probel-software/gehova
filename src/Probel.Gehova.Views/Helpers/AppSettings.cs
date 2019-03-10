using Probel.Gehova.ViewModels.Vm.AppSettings;
using Windows.Storage;

namespace Probel.Gehova.Views.Helpers
{
    public class AppSettings : ISettings
    {
        #region Fields

        private const string s_ContentHeight = "ContentHeight";
        private const string s_ContentIconVisibility = "contentIconVisibility";
        private const string s_contentTextSize = "contentTextSize";

        private readonly ApplicationDataContainer ls = ApplicationData.Current.LocalSettings;

        #endregion Fields

        #region Properties

        public double ContentHeight
        {
            get
            {
                var contentHeight = ls.Values[s_ContentHeight];
                return (contentHeight == null) ? 20 : (double)contentHeight;
            }
            set => ls.Values[s_ContentHeight] = value;
        }

        public bool ContentIconVisibility
        {
            get
            {
                var contentIconVisibility = ls.Values[s_ContentIconVisibility];
                return (contentIconVisibility == null) ? true : (bool)contentIconVisibility;
            }
            set => ls.Values[s_contentTextSize] = value;
        }

        public double ContentTextSize
        {
            get
            {
                var contentTextSize = ls.Values[s_contentTextSize];
                return (contentTextSize == null) ? 12 : (double)contentTextSize;
            }
            set => ls.Values[s_ContentIconVisibility] = value;
        }

        #endregion Properties
    }
}