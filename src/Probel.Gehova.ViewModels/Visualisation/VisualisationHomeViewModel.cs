using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Mapper;
using System;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Visualisation
{
    public class VisualisationHomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IVisualisationService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
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
            var rm = _service.GetReceptionMorning();
            var lt = _service.GetLunchtime();
            var re = _service.GetReceptionEvening();
            var pu = _service.GetPickupRounds();

            var rmMapper = new WeekReceptionMapper(rm);
            var ltmMapper = new WeekReceptionMapper(lt);
            var remMapper = new WeekReceptionMapper(re);
            var puMapper = new WeekPickupRoundMapper(pu);

            ReceptionMorning = rmMapper.Get().Result;
            Lunchtime = ltmMapper.Get().Result;
            ReceptionEvening = remMapper.Get().Result;
            PickupRounds = puMapper.Get().Result;

            DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), _service.GetSelectedWeek().ToLongDateString());
        }

        #endregion Methods
    }
}