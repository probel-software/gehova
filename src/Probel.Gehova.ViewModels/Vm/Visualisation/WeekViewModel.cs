using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class WeekViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<DayViewModel> _days;

        #endregion Fields

        #region Properties

        public ObservableCollection<DayViewModel> Days
        {
            get => _days;
            set => Set(ref _days, value, nameof(Days));
        }

        #endregion Properties
    }
}