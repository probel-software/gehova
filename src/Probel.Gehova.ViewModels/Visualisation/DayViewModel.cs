using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Visualisation
{
    [DebuggerDisplay("{DayName}")]
    public class DayViewModel : ViewModelBase
    {
        #region Fields

        private string _dayName;
        private ObservableCollection<PeopleBagViewModel> _peopleBags;

        #endregion Fields

        #region Properties

        public string DayName
        {
            get => _dayName;
            set => Set(ref _dayName, value, nameof(DayName));
        }

        public ObservableCollection<PeopleBagViewModel> PeopleBags
        {
            get => _peopleBags;
            set => Set(ref _peopleBags, value, nameof(PeopleBags));
        }

        #endregion Properties
    }
}