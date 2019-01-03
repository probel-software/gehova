using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.ViewModels.Infrastructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Settings
{
    public class SettingsHomeViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDataReset _dataReset;
        private readonly IProvisioningService _service;
        private readonly ResourceLoader Resources = new ResourceLoader("Messages");
        private ICommand _addPerson;
        private ObservableCollection<PersonFullDisplayViewModel> _people = new ObservableCollection<PersonFullDisplayViewModel>();
        private ObservableCollection<PickupRoundDisplayModel> _pickupRounds = new ObservableCollection<PickupRoundDisplayModel>();
        private ICommand _refreshCommand;
        private ObservableCollection<TeamDisplayModel> _teams = new ObservableCollection<TeamDisplayModel>();
        private ICommand _updatePickupRoundCommand;
        private ICommand _updateTeamCommand;
        private ICommand _updateUserCommand;
        public ObservableCollection<CategoryModel> _categories = new ObservableCollection<CategoryModel>();

        #endregion Fields

        #region Constructors

        public SettingsHomeViewModel(IProvisioningService service, IDataReset dataReset)
        {
            _service = service;
            _dataReset = dataReset;
        }

        #endregion Constructors

        #region Properties

        public ICommand CreatePersonCommand => _addPerson ?? (_addPerson = new RelayCommand(CreatePerson));

        public ObservableCollection<CategoryModel> Categories
        {
            get => _categories;
            set => Set(ref _categories, value, nameof(Categories));
        }

        public IMessenger Messenger { get; set; }

        public ObservableCollection<PersonFullDisplayViewModel> People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        public ObservableCollection<PickupRoundDisplayModel> PickupRounds
        {
            get => _pickupRounds;
            set => Set(ref _pickupRounds, value, nameof(PickupRounds));
        }

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));

        public ObservableCollection<TeamDisplayModel> Teams
        {
            get => _teams;
            set => Set(ref _teams, value, nameof(Teams));
        }

        public ICommand UpdatePersonCommand => _updateUserCommand ?? (_updateUserCommand = new RelayCommand<PersonFullDisplayViewModel>(UpdatePerson, CanUpdatePerson));
        public ICommand UpdatePickupRoundCommand => _updatePickupRoundCommand ?? (_updatePickupRoundCommand = new RelayCommand<PickupRoundDisplayModel>(UpdatePickupRound, CanUpdatePickupRound));
        public ICommand UpdateTeamCommand => _updateTeamCommand ?? (_updateTeamCommand = new RelayCommand<TeamDisplayModel>(UpdateTeam, CanUpdateTeam));

        #endregion Properties

        #region Methods

        private void CreatePerson()
        {
            People.Add(new PersonFullDisplayViewModel
            {
                FirstName = Resources.GetString("Label_New"),
                LastName = Resources.GetString("Label_Person"),
                Categories = new ObservableCollection<CategoryViewModel>(Categories.ToViewModel())
            });
            Messenger.Say(Resources.GetString("Info_PleaseUpdateToSave"));
        }

        private bool CanUpdatePerson(PersonFullDisplayViewModel person) => person != null;

        private bool CanUpdatePickupRound(PickupRoundDisplayModel pickupRound) => pickupRound != null && pickupRound.Id > 0;

        private bool CanUpdateTeam(TeamDisplayModel team) => team != null && team.Id > 0;

        private void UpdatePerson(PersonFullDisplayViewModel person)
        {
            if (person.Id != 0)
            {
                _service.Update(person.ToModel());
                var p = _service.GetPerson(person.Id);
                person.CategoryDisplay = p.Category;

                Messenger?.Say(string.Format(Resources.GetString("Info_PersonUpdated"), $"{person.FirstName} {person.LastName}"));
            }
            else
            {
                _service.Create(person.ToModel());
                Messenger?.Say(string.Format(Resources.GetString("Info_PersonAdded"), $"{person.FirstName} {person.LastName}"));
            }
        }

        private void UpdatePickupRound(PickupRoundDisplayModel pickupRound)
        {
            _service.Update(pickupRound);
            Messenger?.Say(string.Format(Resources.GetString("Info_PickupRoundUpdated"), pickupRound.Name));
        }

        private void UpdateTeam(TeamDisplayModel team)
        {
            _service.Update(team);
            Messenger?.Say(string.Format(Resources.GetString("Info_TeamUpdated"), team.Name));
        }

        public void Refresh()
        {
            //#if DEBUG
            //            _dataReset.Execute();
            //#endif
            var t = _service.GetTeams();
            var r = _service.GetPickupRounds();
            var p = _service.GetPeople().ToList();
            var c = _service.GetCategories();

            People = new ObservableCollection<PersonFullDisplayViewModel>(p.ToViewModel(c));
            Teams = new ObservableCollection<TeamDisplayModel>(t);
            PickupRounds = new ObservableCollection<PickupRoundDisplayModel>(r);
            Categories = new ObservableCollection<CategoryModel>(c);
        }

        #endregion Methods
    }
}