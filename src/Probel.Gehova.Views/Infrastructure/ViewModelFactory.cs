using Probel.Gehova.ViewModels.Vm;
using Probel.Gehova.ViewModels.Vm.AppSettings;
using Probel.Gehova.ViewModels.Vm.Provisioning;
using Probel.Gehova.ViewModels.Vm.Settings;
using Probel.Gehova.ViewModels.Vm.Visualisation;
using Unity;

namespace Probel.Gehova.Views.Infrastructure
{
    public class ViewModelFactory : IViewModelFactory
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion Fields

        #region Constructors

        public ViewModelFactory(IUnityContainer container)
        {
            _container = container;
        }

        #endregion Constructors

        #region Properties

        public AddAbsenceViewModel AbsenceViewModel => _container.Resolve<AddAbsenceViewModel>();
        public AddPersonViewModel AddPersonViewModel => _container.Resolve<AddPersonViewModel>();
        public AddPickupRoundViewModel AddPickupRoundViewModel => _container.Resolve<AddPickupRoundViewModel>();
        public AddTeamViewModel AddTeamViewModel => _container.Resolve<AddTeamViewModel>();
        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
        public PlannerPickupRoundViewModel PickupRoundViewModel => _container.Resolve<PlannerPickupRoundViewModel>();
        public PlannerReceptionViewModel ReceptionPlannerViewModel => _container.Resolve<PlannerReceptionViewModel>();
        public ProvisioningHomeViewModel ProvisioningHomeViewModel => _container.Resolve<ProvisioningHomeViewModel>();
        public SettingsHomeViewModel SettingsHomeViewModel => _container.Resolve<SettingsHomeViewModel>();
        public PlannerTeamViewModel TeamPlannerViewModel => _container.Resolve<PlannerTeamViewModel>();
        public ApplicationSettingsViewModel ApplicationSettingsViewModel => _container.Resolve<ApplicationSettingsViewModel>();

        #endregion Properties
    }
}