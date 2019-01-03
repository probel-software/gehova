using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;

namespace Probel.Gehova.Cli.Tests
{
    public class VisualisationWeeks : ITestCase
    {
        #region Fields

        private readonly IVisualisationService _service;

        #endregion Fields

        #region Constructors

        public VisualisationWeeks()
        {
            var dbl = new MyDocumentLocator();
            _service = new VisualisationService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 10;
        public string Title => "Display weeks";

        #endregion Properties

        #region Methods

        public void Execute()
        {
            var rm = _service.GetReceptionMorning();
            var lt = _service.GetLunchtime();
            var re = _service.GetReceptionEvening();

            Output.WriteTitle("Reception morning:");
            Output.Write(rm);

            Output.WriteTitle("Lunchtime:");
            Output.Write(lt);

            Output.WriteTitle("Reception evening:");
            Output.Write(re);
        }

        #endregion Methods
    }
}