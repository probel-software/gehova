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
    public class PlannerPickupRoundViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IUserMessenger _messenger;
        private readonly IVisualisationService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
        private ObservableCollection<DayPickupRoundModel> _pickupRounds;
        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public PlannerPickupRoundViewModel(IVisualisationService service, IUserMessenger messenger)
        {
            _messenger = messenger;
            _service = service;
        }

        #endregion Constructors

        #region Properties

        public string DisplayedWeekAsText
        {
            get => _displayedWeekAsText;
            set => Set(ref _displayedWeekAsText, value, nameof(DisplayedWeekAsText));
        }

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
            _messenger?.Say(Resources.GetString("Info_WeekUpdated"));
        }

        public void Refresh()
        {
            PickupRounds = null;
            ExecuteAsync(() =>
            {
                return new
                {
                    Monday = _service.GetSelectedWeekAsMonday().ToLongDateString(),
                    Friday = _service.GetSelectedWeekAsFriday().ToLongDateString()
                };
            }, r => DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), r.Monday, r.Friday));

            ExecuteAsync(() => _service.GetPickupRounds(), r => PickupRounds = new ObservableCollection<DayPickupRoundModel>(r));
        }

        #endregion Methods
    }
}