using Probel.Gehova.ViewModels.Settings;
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

        public SettingsHomeViewModel SettingsHomeViewModel => _container.Resolve<SettingsHomeViewModel>();

        #endregion Properties
    }
}