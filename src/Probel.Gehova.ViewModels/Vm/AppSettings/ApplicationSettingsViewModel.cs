using GalaSoft.MvvmLight;

namespace Probel.Gehova.ViewModels.Vm.AppSettings
{
    public class ApplicationSettingsViewModel : ViewModelBase
    {
        #region Fields

        private readonly ISettings _settings;

        #endregion Fields

        #region Constructors

        public ApplicationSettingsViewModel(ISettings settings) => _settings = settings;

        #endregion Constructors

        #region Properties

        public double ContentHeight
        {
            get => _settings.ContentHeight;
            set
            {
                _settings.ContentHeight = value;
                RaisePropertyChanged(nameof(ContentHeight));
            }
        }

        public bool ContentIconVisibility
        {
            get => _settings.ContentIconVisibility;
            set
            {
                _settings.ContentIconVisibility = value;
                RaisePropertyChanged(nameof(ContentIconVisibility));
            }
        }

        public double ContentTextSize
        {
            get => _settings.ContentTextSize;
            set
            {
                _settings.ContentTextSize = value;
                RaisePropertyChanged(nameof(ContentTextSize));
            }

            #endregion Properties
        }
    }
}