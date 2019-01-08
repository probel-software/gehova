using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Visualisation
{
    [DebuggerDisplay("{TeamName}")]
    public class PeopleBagViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<PersonViewModel> _people;
        private string _name;

        #endregion Fields

        #region Properties

        public ObservableCollection<PersonViewModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        #endregion Properties
    }
}