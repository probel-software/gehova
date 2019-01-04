using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Probel.Gehova.ViewModels.Provisioning
{
    public class ProvisioningHomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IProvisioningService _service;

        private IEnumerable<PersonFullDisplayModel> _people;
        private ObservableCollection<PersonFullDisplayModel> _peopleInCurrentPickupRound;
        private ObservableCollection<PersonFullDisplayModel> _peopleInCurrentTeam;
        private ObservableCollection<PersonFullDisplayModel> _peopleNotInAnyPickupRound;
        private ObservableCollection<PersonFullDisplayModel> _peopleNotInAnyTeam;

        private ObservableCollection<PickupRoundDisplayModel> _pickupRounds;
        private PickupRoundDisplayModel _selectedPickupRound;
        private TeamDisplayModel _selectedTeam;
        private ObservableCollection<TeamDisplayModel> _teams;

        #endregion Fields

        #region Constructors

        public ProvisioningHomeViewModel(IProvisioningService service)
        {
            _service = service;

            PropertyChanged += (s, e) => Refresh(e.PropertyName);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<PersonFullDisplayModel> PeopleInCurrentPickupRound
        {
            get => _peopleInCurrentPickupRound;
            set => Set(ref _peopleInCurrentPickupRound, value, nameof(PeopleInCurrentPickupRound));
        }

        public ObservableCollection<PersonFullDisplayModel> PeopleInCurrentTeam
        {
            get => _peopleInCurrentTeam;
            set => Set(ref _peopleInCurrentTeam, value, nameof(PeopleInCurrentTeam));
        }

        public ObservableCollection<PersonFullDisplayModel> PeopleNotInAnyPickupRound
        {
            get => _peopleNotInAnyPickupRound;
            set => Set(ref _peopleNotInAnyPickupRound, value, nameof(PeopleNotInAnyPickupRound));
        }

        public ObservableCollection<PersonFullDisplayModel> PeopleNotInAnyTeam
        {
            get => _peopleNotInAnyTeam;
            set => Set(ref _peopleNotInAnyTeam, value, nameof(PeopleNotInAnyTeam));
        }

        public ObservableCollection<PickupRoundDisplayModel> PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public PickupRoundDisplayModel SelectedPickupRound
        {
            get => _selectedPickupRound;
            set => Set(ref _selectedPickupRound, value, nameof(SelectedPickupRound));
        }

        public TeamDisplayModel SelectedTeam
        {
            get => _selectedTeam;
            set => Set(ref _selectedTeam, value, nameof(SelectedTeam));
        }

        public ObservableCollection<TeamDisplayModel> Teams
        {
            get => _teams;
            set => Set(ref _teams, value, nameof(Teams));
        }

        #endregion Properties

        #region Methods

        private void RefreshPeopleInPickupRounds()
        {
            if (SelectedPickupRound != null)
            {
                var @in = (from p in _people
                           where p.PickupRoundId == SelectedPickupRound.Id
                           select p).ToList();
                PeopleInCurrentPickupRound = new ObservableCollection<PersonFullDisplayModel>(@in);

                var @out = (from p in _people
                            where p.PickupRoundId <= 0
                            select p).ToList();
                PeopleNotInAnyPickupRound = new ObservableCollection<PersonFullDisplayModel>(@out);
            }
        }

        private void RefreshPeopleInTeams()
        {
            if (SelectedTeam != null)
            {
                var @in = (from p in _people
                           where p.TeamId == SelectedTeam.Id
                           select p).ToList();
                PeopleInCurrentTeam = new ObservableCollection<PersonFullDisplayModel>(@in);

                var @out = (from p in _people
                            where p.TeamId <= 0
                            select p).ToList();
                PeopleNotInAnyTeam = new ObservableCollection<PersonFullDisplayModel>(@out);
            }
        }

        public void Refresh(string propertyName)
        {
            if (propertyName == nameof(SelectedTeam)) { RefreshPeopleInTeams(); }
            else if (propertyName == nameof(SelectedPickupRound)) { RefreshPeopleInPickupRounds(); }
        }

        public void Refresh()
        {
            var teams = _service.GetTeams();
            Teams = new ObservableCollection<TeamDisplayModel>(teams);

            var pickups = _service.GetPickupRounds();
            PickupRounds = new ObservableCollection<PickupRoundDisplayModel>(pickups);

            _people = _service.GetPeople();

            if (Teams.Count() > 0) { SelectedTeam = Teams[0]; }
            if (PickupRounds.Count() > 0) { SelectedPickupRound = PickupRounds[0]; }
        }

        #endregion Methods
    }
}