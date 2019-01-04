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
        private ObservableCollection<TeamViewModel> _teams;

        #endregion Fields

        #region Properties

        public string DayName
        {
            get => _dayName;
            set => Set(ref _dayName, value, nameof(DayName));
        }

        public ObservableCollection<TeamViewModel> Teams
        {
            get => _teams;
            set => Set(ref _teams, value, nameof(Teams));
        }

        #endregion Properties
    }
}