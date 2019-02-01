using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Mapper;
using System;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class VisualisationHomeViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IVisualisationService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
        private WeekViewModel _groups;
        private WeekViewModel _lunchtime;
        private WeekViewModel _pickupRounds;
        private WeekViewModel _receptionEvening;
        private WeekViewModel _receptionMorning;
        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public VisualisationHomeViewModel(IVisualisationService service)
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

        public WeekViewModel Groups
        {
            get => _groups;
            set => Set(ref _groups, value, nameof(Groups));
        }

        public WeekViewModel Lunchtime
        {
            get => _lunchtime;
            set => Set(ref _lunchtime, value, nameof(Lunchtime));
        }

        public IMessenger Messenger { get; set; }

        public WeekViewModel PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public WeekViewModel ReceptionEvening
        {
            get => _receptionEvening;
            set => Set(ref _receptionEvening, value, nameof(ReceptionEvening));
        }

        public WeekViewModel ReceptionMorning
        {
            get => _receptionMorning;
            set => Set(ref _receptionMorning, value, nameof(ReceptionMorning));
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
            var monday = _service.GetSelectedWeekAsMonday().ToLongDateString();
            var friday = _service.GetSelectedWeekAsFriday().ToLongDateString();
            DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), monday, friday);

            ExecuteAsync(() => _service.GetGroups(), c => Groups = new WeekReceptionMapper(c).Get().Result);
            ExecuteAsync(() => _service.GetReceptionMorning(), c => ReceptionMorning = new WeekReceptionMapper(c).Get().Result);
            ExecuteAsync(() => _service.GetReceptionEvening(), c => ReceptionEvening = new WeekReceptionMapper(c).Get().Result);
            ExecuteAsync(() => _service.GetLunchtime(), c => Lunchtime = new WeekReceptionMapper(c).Get().Result);
            ExecuteAsync(() => _service.GetPickupRounds(), c => PickupRounds = new WeekPickupRoundMapper(c).Get().Result);
        }

        #endregion Methods
    }
}