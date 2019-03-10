using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    [DebuggerDisplay("{TeamName}")]
    [Obsolete]
    public class PeopleBagViewModel : ViewModelBase
    {
        #region Fields

        private string _name;
        private ObservableCollection<PersonViewModel> _people;

        #endregion Fields

        #region Properties

        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        public ObservableCollection<PersonViewModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        #endregion Properties
    }
}