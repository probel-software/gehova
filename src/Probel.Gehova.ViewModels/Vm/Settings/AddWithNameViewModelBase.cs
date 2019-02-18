using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public class AddPickupRoundViewModel : AddWithNameViewModelBase
    {
        #region Constructors

        public AddPickupRoundViewModel(IProvisioningService service) : base(service)
        {
        }

        #endregion Constructors

        #region Properties

        protected override string ResourceString => "Title_AddPickupRound";

        #endregion Properties

        #region Methods

        protected override void Add()
        {
            var t = new GroupModel { Name = Name };
            Service.CreatePickupRound(t);
        }

        #endregion Methods
    }

    public class AddTeamViewModel : AddWithNameViewModelBase
    {
        #region Constructors

        public AddTeamViewModel(IProvisioningService service) : base(service)
        {
        }

        #endregion Constructors

        #region Properties

        protected override string ResourceString => "Title_AddTeam";

        #endregion Properties

        #region Methods

        protected override void Add()
        {
            var pur = new GroupModel { Name = Name };
            Service.CreatePickupRound(pur);
        }

        #endregion Methods
    }

    public abstract class AddWithNameViewModelBase : ViewModelBase, IAddWithNameViewModel
    {
        #region Fields

        private readonly ResourceLoader _resources = new ResourceLoader("Messages");
        private RelayCommand _addCommand;
        private bool _isAbleToAdd;
        private string _name;

        #endregion Fields

        #region Constructors

        public AddWithNameViewModelBase(IProvisioningService service)
        {
            Service = service;
        }

        #endregion Constructors

        #region Properties

        protected abstract string ResourceString { get; }

        protected IProvisioningService Service { get; }

        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(Add, CanAdd));

        public bool IsAbleToAdd
        {
            get => _isAbleToAdd;
            set => Set(ref _isAbleToAdd, value, nameof(IsAbleToAdd));
        }

        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value, nameof(Name));
                CanAdd();
            }
        }

        public string PrimaryButtonText => _resources.GetString("Label_Add");

        public string SecondaryButtonText => _resources.GetString("Label_Cancel");

        public string Title => _resources.GetString(ResourceString);

        #endregion Properties

        #region Methods

        private bool CanAdd()
        {
            var canadd = !string.IsNullOrEmpty(Name);
            IsAbleToAdd = canadd;
            return canadd;
        }

        protected abstract void Add();

        #endregion Methods
    }
}