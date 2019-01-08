using Probel.Gehova.ViewModels.Provisioning;
using Probel.Gehova.ViewModels.Settings;
using Probel.Gehova.ViewModels.Visualisation;
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

        public ProvisioningHomeViewModel ProvisioningHomeViewModel => _container.Resolve<ProvisioningHomeViewModel>();
        public SettingsHomeViewModel SettingsHomeViewModel => _container.Resolve<SettingsHomeViewModel>();
        public VisualisationHomeViewModel VisualisationHomeViewModel => _container.Resolve<VisualisationHomeViewModel>();
        public AbsenceViewModel AbsenceViewModel => _container.Resolve<AbsenceViewModel>();
        #endregion Properties

    }
}