using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class PlannerTeamViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IUserMessenger _messenger;
        private readonly IVisualisationService _service;

        private readonly ResourceLoader Resources = new ResourceLoader("Messages");

        private ObservableCollection<DayModel> _days;

        private string _displayedWeekAsText;

        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public PlannerTeamViewModel(IVisualisationService service, IUserMessenger messenger)
        {
            _messenger = messenger;
            _service = service;
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<DayModel> Days
        {
            get => _days;
            set => Set(ref _days, value, nameof(Days));
        }

        public string DisplayedWeekAsText
        {
            get => _displayedWeekAsText;
            set => Set(ref _displayedWeekAsText, value, nameof(DisplayedWeekAsText));
        }

        public ICommand UpdateWeekCommand => _updateWeekCommand ?? (_updateWeekCommand = new RelayCommand<DateTimeOffset>(UpdateWeek));

        #endregion Properties

        #region Methods

        private void UpdateWeek(DateTimeOffset dtOffset)
        {
            var date = new DateTime(dtOffset.Ticks);
            _service.SetSelectedWeek(date);
            Refresh();
            _messenger?.Say(Resources.GetString("Info_WeekUpdated"));
        }

        public void Refresh()
        {
            Days = null;
            ExecuteAsync(() =>
            {
                return new
                {
                    Monday = _service.GetSelectedWeekAsMonday().ToLongDateString(),
                    Friday = _service.GetSelectedWeekAsFriday().ToLongDateString()
                };
            }, r => DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), r.Monday, r.Friday));

            ExecuteAsync(() => _service.GetAllTeams(), r => Days = new ObservableCollection<DayModel>(r));
        }

        #endregion Methods
    }
}
