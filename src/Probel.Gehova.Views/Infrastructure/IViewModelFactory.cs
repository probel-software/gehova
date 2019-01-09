using Probel.Gehova.ViewModels.Vm;
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
        ProvisioningHomeViewModel ProvisioningHomeViewModel { get; }
        SettingsHomeViewModel SettingsHomeViewModel { get; }
        VisualisationHomeViewModel VisualisationHomeViewModel { get; }
        MainViewModel MainViewModel { get; }
        #endregion Properties
    }
}