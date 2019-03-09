using NLog;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.ViewModels.Infrastructure;
using Probel.Gehova.ViewModels.Vm.AppSettings;
using Probel.Gehova.Views.Helpers;
using System;
using Unity;

namespace Probel.Gehova.Views.Infrastructure
{
    public static class IocFactory
    {
        #region Fields

        private static readonly IUnityContainer _container = new UnityContainer();
        private static bool _isLoaded = false;

        #endregion Fields

        #region Properties

        public static IViewModelFactory ViewModel
        {
            get
            {
                if (_isLoaded) { return new ViewModelFactory(_container); }
                else { throw new InvalidOperationException($"AppContainer not loaded. Did you called 'Init()'?"); }
            }
        }

        #endregion Properties

        #region Methods

        public static void Init()
        {
            if (!_isLoaded)
            {
                _container.RegisterType<IProvisioningService, ProvisioningService>();
                _container.RegisterType<IVisualisationService, VisualisationService>();
                _container.RegisterType<IFileLocator, UwpDbLocator>();
                _container.RegisterType<IDataReset, DataReset>();
                _container.RegisterType<IProvisioningService, ProvisioningService>();
                _container.RegisterFactory<ILogger>(_ => LogManager.GetCurrentClassLogger());
                _container.RegisterType<IUpdateService, UpdateService>();
                //----
                _container.RegisterType<IUserMessenger, InAppMessenger>();
                _container.RegisterType<ISettings, AppSettings>();

                _isLoaded = true;
            }
        }

        #endregion Methods
    }
}