using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Mapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Provisioning
{
    public class ProvisioningHomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IUserMessenger _messenger;
        private readonly IProvisioningService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private AbsenceDisplayModel _currentAbsence;
        private ObservableCollection<AbsenceDisplayModel> _currentPersonAbsences;
        private ObservableCollection<PersonFullDisplayModel> _people;
        private ObservableCollection<PersonFullDisplayModel> _peopleInCurrentPickupRound;
        private ObservableCollection<PersonFullDisplayModel> _peopleInCurrentTeam;
        private ObservableCollection<PersonFullDisplayModel> _peopleNotInAnyPickupRound;
        private ObservableCollection<PersonFullDisplayModel> _peopleNotInAnyTeam;

        private ObservableCollection<GroupDisplayModel> _pickupRounds;
        private RelayCommand _refreshAbsencesCommand;
        private RelayCommand<AbsenceDisplayModel> _removeAbsenceCommand;
        private PersonFullDisplayModel _selectedPerson;
        private GroupDisplayModel _selectedPickupRound;
        private GroupDisplayModel _selectedTeam;
        private ObservableCollection<GroupDisplayModel> _teams;

        #endregion Fields

        #region Constructors

        public ProvisioningHomeViewModel(IProvisioningService service, IUserMessenger messenger)
        {
            _messenger = messenger;
            _service = service;

            PropertyChanged += (s, e) => Refresh(e.PropertyName);
        }

        #endregion Constructors

        #region Properties

        public AbsenceDisplayModel CurrentAbsence
        {
            get => _currentAbsence;
            set => Set(ref _currentAbsence, value, nameof(CurrentAbsence));
        }

        public ObservableCollection<AbsenceDisplayModel> CurrentPersonAbsences
        {
            get => _currentPersonAbsences;
            set => Set(ref _currentPersonAbsences, value, nameof(CurrentPersonAbsences));
        }

        public ObservableCollection<PersonFullDisplayModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

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

        public ObservableCollection<GroupDisplayModel> PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public ICommand RefreshAbsencesCommand => _refreshAbsencesCommand ?? (_refreshAbsencesCommand = new RelayCommand(RefreshAbsence, CanRefreshAbsence));

        public ICommand RemoveAbsenceCommand => _removeAbsenceCommand ?? (_removeAbsenceCommand = new RelayCommand<AbsenceDisplayModel>(RemoveAbsence, CanRemoveAbsence));

        public PersonFullDisplayModel SelectedPerson
        {
            get => _selectedPerson;
            set => Set(ref _selectedPerson, value, nameof(SelectedPerson));
        }

        public GroupDisplayModel SelectedPickupRound
        {
            get => _selectedPickupRound;
            set => Set(ref _selectedPickupRound, value, nameof(SelectedPickupRound));
        }

        public GroupDisplayModel SelectedTeam
        {
            get => _selectedTeam;
            set => Set(ref _selectedTeam, value, nameof(SelectedTeam));
        }

        public ObservableCollection<GroupDisplayModel> Teams
        {
            get => _teams;
            set => Set(ref _teams, value, nameof(Teams));
        }

        #endregion Properties

        #region Methods

        private bool CanRefreshAbsence() => SelectedPerson != null && SelectedPerson.Id > 0;

        private bool CanRemoveAbsence(AbsenceDisplayModel absence) => absence != null && absence.Id > 0;

        private void RefreshAbsence()
        {
            if (SelectedPerson != null)
            {
                var result = _service.GetAbsencesOf(SelectedPerson);
                if (result != null) { CurrentPersonAbsences = new ObservableCollection<AbsenceDisplayModel>(result); }
            }
        }

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

        private void RemoveAbsence(AbsenceDisplayModel absence)
        {
            _service.Remove(absence);
            _messenger?.Say(Resources.GetString("Info_AbsenceRemoved"));

            var todel = (from a in CurrentPersonAbsences
                         where a.Id == absence.Id
                         select a).FirstOrDefault();
            CurrentPersonAbsences.Remove(todel);
        }

        private void UpdatePickupRounds()
        {
            var rnd = new GroupModel(SelectedPickupRound)
            {
                People = PeopleInCurrentPickupRound.ToPersonDisplayModel()
            };
            _service.UpdatePickupRound(rnd);
            Refresh();
        }

        private void UpdateTeams()
        {
            var rnd = new GroupModel(SelectedTeam)
            {
                People = PeopleInCurrentTeam.ToPersonDisplayModel()
            };
            _service.UpdateTeam(rnd);
            Refresh();
        }

        public void AddToPickupRoundAndUpdate(IEnumerable<long> items)
        {
            foreach (var id in items)
            {
                _peopleInCurrentPickupRound.Add(new PersonFullDisplayModel { Id = id });
            }
            UpdatePickupRounds();
        }

        public void AddToTeamAndUpdate(IEnumerable<long> items)
        {
            foreach (var id in items)
            {
                _peopleInCurrentTeam.Add(new PersonFullDisplayModel { Id = id });
            }
            UpdateTeams();
        }

        public PersonFullDisplayModel FindById(object selectedItem)
        {
            if (selectedItem is PersonFullDisplayModel person)
            {
                var res = (from p in People
                           where p.Id == person.Id
                           select p).Single();
                return res;
            }
            else { return null; }
        }

        public IEnumerable<PersonFullDisplayModel> FindByName(string criterion)
        {
            criterion = criterion.ToLower();
            var res = (from p in People
                       where p.FirstName.ToLower().Contains(criterion)
                          || p.LastName.ToLower().Contains(criterion)
                       select p).ToList();
            return res;
        }

        public void Refresh(string propertyName)
        {
            if (propertyName == nameof(SelectedTeam)) { RefreshPeopleInTeams(); }
            else if (propertyName == nameof(SelectedPickupRound)) { RefreshPeopleInPickupRounds(); }
        }

        public void Refresh()
        {
            var teams = _service.GetTeams();
            Teams = new ObservableCollection<GroupDisplayModel>(teams);

            var pickups = _service.GetPickupRounds();
            PickupRounds = new ObservableCollection<GroupDisplayModel>(pickups);

            var people = _service.GetPeople();
            People = new ObservableCollection<PersonFullDisplayModel>(people);

            if (Teams.Count() > 0) { SelectedTeam = Teams[0]; }
            if (PickupRounds.Count() > 0) { SelectedPickupRound = PickupRounds[0]; }

            RefreshAbsence();
        }

        public void RemoveFromPickupRoundAndUpdate(IEnumerable<long> items)
        {
            foreach (var id in items)
            {
                var toremove = (from p in _peopleInCurrentPickupRound
                                where p.Id == id
                                select p).SingleOrDefault();
                _peopleInCurrentPickupRound.Remove(toremove);
            }
            UpdatePickupRounds();
        }

        public void RemoveFromTeamAndUpdate(IEnumerable<long> items)
        {
            foreach (var id in items)
            {
                var toremove = (from p in _peopleInCurrentTeam
                                where p.Id == id
                                select p).SingleOrDefault();
                _peopleInCurrentTeam.Remove(toremove);
            }
            UpdateTeams();
        }

        #endregion Methods
    }
}