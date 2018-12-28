using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;

namespace Probel.Gehova.Cli.Tests
{
    public class VisualisationWeeks : ITestCase
    {
        #region Properties

        private readonly IVisualisationService _service = new VisualisationService();
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