using NLog;
using NLog.Config;
using NLog.Targets;
using Probel.Gehova.Cli.Helpers;

namespace Probel.Gehova.Cli
{
    internal class Program
    {
        #region Methods

        private static void ConfigureLog()
        {
            var config = new LoggingConfiguration();

            var logconsole = new ColoredConsoleTarget("logconsole");

            config.AddRule(LogLevel.Warn, LogLevel.Fatal, logconsole);

            LogManager.Configuration = config;
        }


        private static void Main(string[] args)
        {
            ConfigureLog();

            Output.WriteLine("Clearing data...");
            TestCaseManager.ResetData();
            Output.WriteLine("Executing Test case(s)");
            TestCaseManager.ExecuteLast();

            Output.Pause();
        }

        #endregion Methods
    }
}