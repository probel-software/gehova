using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Cli.Helpers;
using System.Reflection;

namespace Probel.Gehova.Cli.Tests
{
    public class MiscResources : ITestCase
    {
        public string Title => "Check resources retrieving";

        public int Order => 12;

        public void Execute()
        {
            var rm = new AssetManager(this);
            var text = rm.GetScript("Probel.Gehova.Cli.Assets.test_data.sql");

            Output.WriteTitle("Sql script:");
            Output.WriteLine(text);
        }
    }
}
