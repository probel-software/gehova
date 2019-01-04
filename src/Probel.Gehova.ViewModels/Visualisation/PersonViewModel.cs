using GalaSoft.MvvmLight;
using System.Diagnostics;

namespace Probel.Gehova.ViewModels.Visualisation
{
    [DebuggerDisplay("FirstName: {FirstName} - LastName: {LastName}")]
    public class PersonViewModel : ViewModelBase
    {
        #region Fields

        private string _firstName;
        private string _lastName;

        #endregion Fields

        #region Properties

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value, nameof(LastName));
        }

        #endregion Properties
    }
}