using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Visualisation
{
    [DebuggerDisplay("{TeamName}")]
    public class TeamViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<PersonViewModel> _people;
        private string _teamName;

        #endregion Fields

        #region Properties

        public ObservableCollection<PersonViewModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        public string TeamName
        {
            get => _teamName;
            set => Set(ref _teamName, value, nameof(TeamName));
        }

        #endregion Properties
    }
}