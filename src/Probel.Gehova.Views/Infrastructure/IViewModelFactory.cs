using Probel.Gehova.ViewModels.Provisioning;
using Probel.Gehova.ViewModels.Settings;
using Probel.Gehova.ViewModels.Visualisation;

namespace Probel.Gehova.Views.Infrastructure
{
    public interface IViewModelFactory
    {
        #region Properties

        SettingsHomeViewModel SettingsHomeViewModel { get; }
        VisualisationHomeViewModel VisualisationHomeViewModel { get; }
        ProvisioningHomeViewModel ProvisioningHomeViewModel { get; }

        #endregion Properties
    }
}