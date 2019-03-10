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
    public class PlannerReceptionViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IUserMessenger _messenger;
        private readonly IVisualisationService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
        private ObservableCollection<ReceptionGroupModel> _receptionGroups;
        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public PlannerReceptionViewModel(IVisualisationService service, IUserMessenger messenger)
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

        public ObservableCollection<ReceptionGroupModel> ReceptionGroups
        {
            get => _receptionGroups;
            set => Set(ref _receptionGroups, value, nameof(ReceptionGroups));
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
            ReceptionGroups = null;
            ExecuteAsync(() =>
            {
                return new
                {
                    Monday = _service.GetSelectedWeekAsMonday().ToString("dddd dd MMMM"),
                    Friday = _service.GetSelectedWeekAsFriday().ToString("dddd dd MMMM yyyy"),
                };
            }, r => DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), r.Monday, r.Friday));

            ExecuteAsync(() => _service.GetReceptionGroups(), r => ReceptionGroups = new ObservableCollection<ReceptionGroupModel>(r));
        }

        #endregion Methods
    }
}