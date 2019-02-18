using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class PickupRoundViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IVisualisationService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
        private ObservableCollection<DayPickupRoundModel> _pickupRounds;
        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public PickupRoundViewModel(IVisualisationService service)
        {
            _service = service;
        }

        #endregion Constructors

        #region Properties

        public string DisplayedWeekAsText
        {
            get => _displayedWeekAsText;
            set => Set(ref _displayedWeekAsText, value, nameof(DisplayedWeekAsText));
        }

        public IMessenger Messenger { get; set; }

        public ObservableCollection<DayPickupRoundModel> PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public ICommand UpdateWeekCommand => _updateWeekCommand ?? (_updateWeekCommand = new RelayCommand<DateTimeOffset>(UpdateWeek));

        #endregion Properties

        #region Methods

        private bool CanUpdateWeek(DateTimeOffset dtOffset)
        {
            var date = new DateTime(dtOffset.Ticks);
            return (date > DateTime.MinValue && date < DateTime.MaxValue);
        }

        private void UpdateWeek(DateTimeOffset dtOffset)
        {
            var date = new DateTime(dtOffset.Ticks);
            _service.SetSelectedWeek(date);
            Refresh();
            Messenger?.Say(Resources.GetString("Info_WeekUpdated"));
        }

        public void Refresh()
        {
            PickupRounds = null;

            var monday = _service.GetSelectedWeekAsMonday().ToLongDateString();
            var friday = _service.GetSelectedWeekAsFriday().ToLongDateString();
            DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), monday, friday);

            ExecuteAsync(() => _service.GetPickupRounds(), r => PickupRounds = new ObservableCollection<DayPickupRoundModel>(r));
        }

        #endregion Methods
    }
}