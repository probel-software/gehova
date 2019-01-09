using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using System;
using System.Windows.Input;

namespace Probel.Gehova.ViewModels.Vm.Provisioning
{
    public class AddAbsenceViewModel : ViewModelBase
    {
        #region Fields

        private readonly IProvisioningService _service;
        private RelayCommand _addAbsenceCommand;
        private DateTimeOffset _from;
        private long _personId;
        private DateTimeOffset _to;

        #endregion Fields

        #region Constructors

        public AddAbsenceViewModel(IProvisioningService service)
        {
            _service = service;
            From = new DateTime(DateTime.Today.Ticks);
            To = From.AddDays(2);
        }

        #endregion Constructors

        #region Properties

        public ICommand AddAbsenceCommand => _addAbsenceCommand ?? (_addAbsenceCommand = new RelayCommand(AddAbsence, CanAddAbsence));

        public DateTimeOffset From
        {
            get => _from;
            set
            {
                Set(ref _from, value, nameof(From));
                _addAbsenceCommand?.RaiseCanExecuteChanged();
            }
        }

        public long PersonId
        {
            get => _personId;
            set => Set(ref _personId, value, nameof(PersonId));
        }

        public DateTimeOffset To
        {
            get => _to;
            set
            {
                Set(ref _to, value, nameof(To));
                _addAbsenceCommand?.RaiseCanExecuteChanged();
            }
        }

        #endregion Properties

        #region Methods

        private void AddAbsence()
        {
            var abs = new AbsenceDisplayModel
            {
                PersonId = PersonId,
                From = new DateTime(From.Ticks),
                To = new DateTime(To.Ticks).Date,
            };
            _service.Create(abs);
        }

        private bool CanAddAbsence()
        {
            var result = PersonId > 0
                      && From < To;
            return result;
        }

        #endregion Methods
    }
}