using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Probel.Gehova.ViewModels.Vm
{
    public static class VersionExtensions
    {
        #region Methods

        public static Version AsVersion(this PackageVersion src)
        {
            var ver = new Version(src.Major, src.Minor, src.Build, src.Revision);
            return ver;
        }

        #endregion Methods
    }

    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly IVisualisationService _service;
        private readonly IUpdateService _updateService;

        private bool _isUpdating;

        #endregion Fields

        #region Constructors

        public MainViewModel(IVisualisationService service, IUpdateService updateService)
        {
            _service = service;
            _updateService = updateService;
        }

        #endregion Constructors

        #region Properties

        public bool IsUpdating
        {
            get => _isUpdating;
            set => Set(ref _isUpdating, value, nameof(IsUpdating));
        }

        #endregion Properties

        #region Methods

        public async void ExecuteUpdate()
        {
            var ver = Package.Current.Id.Version.AsVersion();
            var isUpToDate = _updateService.CheckVersion(ver);

            if (!isUpToDate)
            {
                IsUpdating = true;
                await Task.Run(() => _updateService.UpdateToVersion(ver));
                IsUpdating = false;
            }
        }

        public void LoadDefaultWeek() => _service.SetSelectedWeek(DateTime.Today);

        #endregion Methods
    }
}