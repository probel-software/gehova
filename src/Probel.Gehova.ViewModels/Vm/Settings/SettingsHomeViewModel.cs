using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.GuiUtils;
using Probel.Gehova.ViewModels.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public class SettingsHomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataReset _dataReset;
        private readonly ResourceLoader _resources = new ResourceLoader("Messages");
        private readonly IProvisioningService _service;
        private RelayCommand _addPerson;
        private IUserMessenger _messenger;
        private ObservableCollection<PersonFullDisplayViewModel> _people = new ObservableCollection<PersonFullDisplayViewModel>();
        private ObservableCollection<GroupDisplayModel> _pickupRounds = new ObservableCollection<GroupDisplayModel>();
        private ObservableCollection<ReceptionModel> _receptions;
        private RelayCommand _refreshCommand;
        private RelayCommand _removePersonCommand;
        private RelayCommand _removePickupRoundCommand;
        private RelayCommand _removeTeamCommand;
        private PersonFullDisplayViewModel _selectedPerson;
        private GroupDisplayModel _selectedPickupRound;
        private GroupDisplayModel _selectedTeam;
        private ObservableCollection<GroupDisplayModel> _teams = new ObservableCollection<GroupDisplayModel>();
        private RelayCommand<PersonFullDisplayViewModel> _updatePersonCommand;
        private RelayCommand<GroupDisplayModel> _updatePickupRoundCommand;
        private RelayCommand<GroupDisplayModel> _updateTeamCommand;
        public ObservableCollection<CategoryModel> _categories = new ObservableCollection<CategoryModel>();

        #endregion Fields

        #region Constructors

        public SettingsHomeViewModel(IProvisioningService service, IDataReset dataReset, IUserMessenger messenger)
        {
            _messenger = messenger;
            _service = service;
            _dataReset = dataReset;
        }

        #endregion Constructors

        #region Properties

        public ICommand AddPersonCommand => _addPerson ?? (_addPerson = new RelayCommand(AddPerson));

        public ObservableCollection<CategoryModel> Categories
        {
            get => _categories;
            set => Set(ref _categories, value, nameof(Categories));
        }

        public ObservableCollection<PersonFullDisplayViewModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        public ObservableCollection<GroupDisplayModel> PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public ObservableCollection<ReceptionModel> Receptions
        {
            get => _receptions;
            set => Set(ref _receptions, value, nameof(Receptions));
        }

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));

        public ICommand RemovePersonCommand => _removePersonCommand ?? (_removePersonCommand = new RelayCommand(RemovePerson, CanRemovePerson));

        public ICommand RemovePickupRoundCommand => _removePickupRoundCommand ?? (_removePickupRoundCommand = new RelayCommand(RemovePickupRound, CanRemovePickupRound));

        public ICommand RemoveTeamCommand => _removeTeamCommand ?? (_removeTeamCommand = new RelayCommand(RemoveTeam, CanRemoveTeam));

        public PersonFullDisplayViewModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                Set(ref _selectedPerson, value, nameof(SelectedPerson));
                _removePersonCommand?.RaiseCanExecuteChanged();
            }
        }

        public GroupDisplayModel SelectedPickupRound
        {
            get => _selectedPickupRound;
            set
            {
                Set(ref _selectedPickupRound, value, nameof(SelectedPickupRound));
                _removePickupRoundCommand?.RaiseCanExecuteChanged();
            }
        }

        public GroupDisplayModel SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                Set(ref _selectedTeam, value, nameof(SelectedTeam));
                _removeTeamCommand?.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<GroupDisplayModel> Teams
        {
            get => _teams;
            set => Set(ref _teams, value, nameof(Teams));
        }

        public ICommand UpdatePersonCommand => _updatePersonCommand ?? (_updatePersonCommand = new RelayCommand<PersonFullDisplayViewModel>(UpdatePerson, CanUpdatePerson));

        public ICommand UpdatePickupRoundCommand => _updatePickupRoundCommand ?? (_updatePickupRoundCommand = new RelayCommand<GroupDisplayModel>(UpdatePickupRound, CanUpdatePickupRound));

        public ICommand UpdateTeamCommand => _updateTeamCommand ?? (_updateTeamCommand = new RelayCommand<GroupDisplayModel>(UpdateTeam, CanUpdateTeam));

        #endregion Properties

        #region Methods

        [Obsolete]
        private void AddPerson()
        {
            People.Add(new PersonFullDisplayViewModel
            {
                FirstName = _resources.GetString("Label_New"),
                LastName = _resources.GetString("Label_Person"),
                Categories = new ObservableCollection<CategoryViewModel>(Categories.ToViewModel())
            });
            _messenger.Say(_resources.GetString("Info_PleaseUpdateToSave"));
        }

        private bool CanRemovePerson() => SelectedPerson != null && SelectedPerson.Id > 0;

        private bool CanRemovePickupRound() => SelectedPickupRound != null && SelectedPickupRound.Id > 0;

        private bool CanRemoveTeam() => SelectedTeam != null && SelectedTeam.Id > 0;

        private bool CanUpdatePerson(PersonFullDisplayViewModel person) => person != null;

        private bool CanUpdatePickupRound(GroupDisplayModel pickupRound) => pickupRound != null;

        private bool CanUpdateTeam(GroupDisplayModel team) => team != null;

        private void RefreshCategories()
        {
            var c = _service.GetCategories();
            Categories = new ObservableCollection<CategoryModel>(c);
        }

        private void RefreshPeople()
        {
            if (Categories.Count == 0) { RefreshCategories(); }
            var p = _service.GetPeople().ToList();
            People = new ObservableCollection<PersonFullDisplayViewModel>(p.ToViewModel(Categories));
        }

        private void RefreshPickupRounds()
        {
            var r = _service.GetPickupRounds();
            PickupRounds = new ObservableCollection<GroupDisplayModel>(r);
        }

        private void RefreshReceptions()
        {
            var c = _service.GetReceptions();
            Receptions = new ObservableCollection<ReceptionModel>(c);
        }

        private void RefreshTeams()
        {
            var t = _service.GetTeams();
            Teams = new ObservableCollection<GroupDisplayModel>(t);
        }

        private void RemovePerson()
        {
            _service.Remove(SelectedPerson.ToModel());
            _messenger.Say(string.Format(_resources.GetString("Info_PersonRemoved"), $"{SelectedPerson.FirstName} {SelectedPerson.LastName}"));
            RefreshPeople();
        }

        private void RemovePickupRound()
        {
            _service.Remove(SelectedPickupRound);
            _messenger.Say(string.Format(_resources.GetString("Info_PickupRoundRemoved"), SelectedPickupRound.Name));
            RefreshPickupRounds();
        }

        private void RemoveTeam()
        {
            _service.Remove(SelectedTeam);
            _messenger.Say(string.Format(_resources.GetString("Info_TeamRemoved"), SelectedTeam.Name));
            RefreshTeams();
        }

        private void UpdatePerson(PersonFullDisplayViewModel person)
        {
            if (person.Id != 0)
            {
                _service.Update(person.ToModel());
                var p = _service.GetPerson(person.Id);
                person.CategoryDisplay = p.Category;

                _messenger?.Say(string.Format(_resources.GetString("Info_PersonUpdated"), $"{person.FirstName} {person.LastName}"));
            }
            else
            {
                _service.Create(person.ToModel());
                _messenger?.Say(string.Format(_resources.GetString("Info_PersonAdded"), $"{person.FirstName} {person.LastName}"));
            }
        }

        private void UpdatePickupRound(GroupDisplayModel pickupRound)
        {
            if (pickupRound == null || string.IsNullOrEmpty(pickupRound.Name)) { return; }
            else if (pickupRound.Id > 0)
            {
                _service.UpdateTeam(pickupRound);
                _messenger?.Say(string.Format(_resources.GetString("Info_PickupRoundUpdated"), pickupRound.Name));
            }
            else
            {
                _service.CreatePickupRound(pickupRound);
                _messenger?.Say(string.Format(_resources.GetString("Info_PickupRoundCreated"), pickupRound.Name));
            }
        }

        private void UpdateTeam(GroupDisplayModel team)
        {
            if (team == null || string.IsNullOrEmpty(team.Name)) { return; }
            else if (team.Id > 0)
            {
                _service.UpdateTeam(team);
                _messenger?.Say(string.Format(_resources.GetString("Info_TeamUpdated"), team.Name));
            }
            else
            {
                _service.CreatePickupRound(team);
                _messenger?.Say(string.Format(_resources.GetString("Info_TeamCreated"), team.Name));
            }
        }

        public void Refresh()
        {
            RefreshCategories();
            RefreshPeople();
            RefreshTeams();
            RefreshPickupRounds();
            RefreshReceptions();
        }

        public void RefreshSelectedPerson(object item)
        {
            if (item is PersonFullDisplayViewModel selectedPerson)
            {
                SelectedPerson = selectedPerson;
                var ids = _service.GetReceptionsOf(SelectedPerson.Id);
                var r = _service.GetReceptions()
                                .ToViewModel()
                                .CheckUserReception(ids);

                SelectedPerson.Receptions = new ObservableCollection<ReceptionViewModel>(r);
            }
        }

        #endregion Methods
    }
}