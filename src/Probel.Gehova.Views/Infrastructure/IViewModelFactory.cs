using Probel.Gehova.ViewModels.Vm;
using Probel.Gehova.ViewModels.Vm.AppSettings;
using Probel.Gehova.ViewModels.Vm.Provisioning;
using Probel.Gehova.ViewModels.Vm.Settings;
using Probel.Gehova.ViewModels.Vm.Visualisation;

namespace Probel.Gehova.Views.Infrastructure
{
    public interface IViewModelFactory
    {
        #region Properties

        AddAbsenceViewModel AbsenceViewModel { get; }
        AddPersonViewModel AddPersonViewModel { get; }
        AddPickupRoundViewModel AddPickupRoundViewModel { get; }
        AddTeamViewModel AddTeamViewModel { get; }
        MainViewModel MainViewModel { get; }
        PlannerReceptionViewModel ReceptionPlannerViewModel { get; }
        ProvisioningHomeViewModel ProvisioningHomeViewModel { get; }
        SettingsHomeViewModel SettingsHomeViewModel { get; }
        PlannerPickupRoundViewModel PickupRoundViewModel { get; }
        PlannerTeamViewModel TeamPlannerViewModel { get; }
        ApplicationSettingsViewModel ApplicationSettingsViewModel { get; }
        #endregion Properties
    }
}
