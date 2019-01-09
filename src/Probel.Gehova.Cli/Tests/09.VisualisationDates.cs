using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;

namespace Probel.Gehova.Cli.Tests
{
    public class VisualisationDates : ITestCase
    {
        #region Fields

        private readonly IVisualisationService _service;

        #endregion Fields

        #region Constructors

        public VisualisationDates()
        {
            var dbl = new MyDocumentLocator();
            _service = new VisualisationService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 9;
        public string Title => "Visualisation of dates and weeks";

        #endregion Properties

        #region Methods

        public void Execute()
        {
            var current = _service.GetSelectedWeekAsMonday();
            Output.Write(current);

            _service.SetSelectedWeek(DateTime.Today);

            current = _service.GetSelectedWeekAsMonday();
            Output.Write(current);
        }

        #endregion Methods
    }
}