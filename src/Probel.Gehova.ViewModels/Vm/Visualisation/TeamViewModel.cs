using GalaSoft.MvvmLight;
using Probel.Gehova.ViewModels.Vm.AppSettings;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class TeamViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISettings _settings;

        private long _contentHeight;
        private long _contentTextSize;

        #endregion Fields

        #region Constructors

        public TeamViewModel(ISettings settings)
        {
            _settings = settings;

            ContentHeight = (long)settings.ContentHeight;
            ContentTextSize = (long)settings.ContentTextSize;
        }

        #endregion Constructors

        #region Properties

        public long ContentHeight
        {
            get => _contentHeight;
            set => Set(ref _contentHeight, value, nameof(ContentHeight));
        }

        public long ContentTextSize
        {
            get => _contentTextSize;
            set => Set(ref _contentTextSize, value, nameof(ContentTextSize));
        }

        #endregion Properties
    }
}
