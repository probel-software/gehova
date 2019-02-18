using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Visualisation
{
    public class TeamPlannerViewModel : AsyncViewModel
    {
        #region Fields

        private readonly IVisualisationService _service;

        private readonly ResourceLoader Resources = new ResourceLoader("Messages");

        private ObservableCollection<DayModel> _days;

        private string _displayedWeekAsText;

        #endregion Fields

        #region Constructors

        public TeamPlannerViewModel(IVisualisationService service)
        {
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

        public IMessenger Messenger { get; set; }

        #endregion Properties

        #region Methods

        public void Refresh()
        {
            Days = null;

            var monday = _service.GetSelectedWeekAsMonday().ToLongDateString();
            var friday = _service.GetSelectedWeekAsFriday().ToLongDateString();
            DisplayedWeekAsText = string.Format(Resources.GetString("Title_SelectedWeek"), monday, friday);

            //TODO: throw a StackOverflowException...
            //ExecuteAsync(() => _service.GetReceptionGroups(), r => ReceptionGroups = new ObservableCollection<ReceptionGroupModel>(r));
            var r = _service.GetAllTeams();
            Days = new ObservableCollection<DayModel>(r);
        }

        #endregion Methods
    }
}