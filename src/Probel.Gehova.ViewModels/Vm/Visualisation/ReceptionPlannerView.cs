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
    public class ReceptionPlannerView : AsyncViewModel
    {
        #region Fields

        private readonly IVisualisationService _service;

        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private string _displayedWeekAsText;
        private ObservableCollection<ReceptionGroupModel> _receptionGroups;

        private RelayCommand<DateTimeOffset> _updateWeekCommand;

        #endregion Fields

        #region Constructors

        public ReceptionPlannerView(IVisualisationService service)
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
            Messenger?.Say(Resources.GetString("Info_WeekUpdated"));
        }

        public void Refresh()
        {
            ReceptionGroups = null;

            var monday = _service.GetSelectedWeekAsMonday().ToLongDateString();
            var friday = _service.GetSelectedWeekAsFriday().ToLongDateString();
            DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), monday, friday);

            //TODO: throw a StackOverflowException...
            //ExecuteAsync(() => _service.GetReceptionGroups(), r => ReceptionGroups = new ObservableCollection<ReceptionGroupModel>(r));
            var r = _service.GetReceptionGroups();
            ReceptionGroups = new ObservableCollection<ReceptionGroupModel>(r);
        }

        #endregion Methods
    }
}