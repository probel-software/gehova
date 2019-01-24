using GalaSoft.MvvmLight;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    [DebuggerDisplay("FirstName: {FirstName} - LastName: {LastName}")]
    public class PersonViewModel : ViewModelBase
    {
        #region Fields

        private string _category;
        private string _firstName;
        private bool _isEducator;
        private string _lastName;

        #endregion Fields

        #region Properties

        public string Category
        {
            get => _category;
            set
            {
                Set(ref _category, value, nameof(Category));
                IsEducator = value?.Contains("edu") ?? false;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        public bool IsEducator
        {
            get => _isEducator;
            private set => Set(ref _isEducator, value, nameof(IsEducator));
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value, nameof(LastName));
        }

        #endregion Properties
    }
}