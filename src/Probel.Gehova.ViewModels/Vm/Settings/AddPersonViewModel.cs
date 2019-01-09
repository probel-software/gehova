using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public class AddPersonViewModel : ViewModelBase
    {
        #region Fields

        private readonly IProvisioningService _service;

        private RelayCommand _addCommand;
        private ObservableCollection<CategoryViewModel> _categories;
        private string _firstName;
        private bool _isAbleToAdd;
        private bool _isLunchtime;
        private bool _isReceptionEvening;
        private bool _isReceptionMorning;
        private string _lastName;

        #endregion Fields

        #region Constructors

        public AddPersonViewModel(IProvisioningService service)
        {
            _service = service;
        }

        #endregion Constructors

        #region Properties

        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(AddPerson, CanAddPerson));

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set => Set(ref _categories, value, nameof(Categories));
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value, nameof(FirstName));
                CanAddPerson(); //Hack: this line do not work _addCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsAbleToAdd
        {
            get => _isAbleToAdd;
            set => Set(ref _isAbleToAdd, value, nameof(IsAbleToAdd));
        }

        public bool IsLunchtime
        {
            get => _isLunchtime;
            set => Set(ref _isLunchtime, value, nameof(IsLunchtime));
        }

        public bool IsReceptionEvening
        {
            get => _isReceptionEvening;
            set => Set(ref _isReceptionEvening, value, nameof(_isReceptionEvening));
        }

        public bool IsReceptionMorning
        {
            get => _isReceptionMorning;
            set => Set(ref _isReceptionMorning, value, nameof(IsReceptionMorning));
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value, nameof(LastName));
                CanAddPerson(); //Hack: this line do not work _addCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void AddPerson()
        {
            _service.Create(new PersonModel
            {
                FirstName = FirstName,
                LastName = LastName,
                IsLunchTime = IsLunchtime,
                IsReceptionEvening = IsReceptionEvening,
                IsReceptionMorning = IsReceptionMorning,
                Categories = GetCategories(),
            });
        }

        private bool CanAddPerson()
        {
            var canadd = !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
            IsAbleToAdd = canadd;
            return canadd;
        }

        private IEnumerable<CategoryModel> GetCategories()
        {
            var result = (from c in Categories
                          where c.IsSelected
                          select c).ToModel();
            return result;
        }

        #endregion Methods
    }
}