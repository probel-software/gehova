using GalaSoft.MvvmLight;
using Probel.Gehova.Business.Services;
using System;

namespace Probel.Gehova.ViewModels.Vm
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly IVisualisationService _service;

        #endregion Fields

        #region Constructors

        public MainViewModel(IVisualisationService service)
        {
            _service = service;
        }

        #endregion Constructors

        #region Methods

        public void LoadDefaultWeek() => _service.SetSelectedWeek(DateTime.Today);

        #endregion Methods
    }
}