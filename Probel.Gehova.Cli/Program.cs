using NLog;
using NLog.Config;
using NLog.Targets;
using Probel.Gehova.Cli.Helpers;
using Probel.Gehova.Cli.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Cli
{
    internal class Program
    {
        #region Methods

        private static IEnumerable<ITestCase> GetTestCases()
        {
            var testCases = (from t in typeof(ITestCase).Assembly.GetTypes()
                             where typeof(ITestCase).IsAssignableFrom(t)
                             && t.IsInterface == false
                             select t);

            var instances = new List<ITestCase>();
            foreach (var testCase in testCases)
            {
                if (Activator.CreateInstance(testCase) is ITestCase instance) { instances.Add(instance); }
            }

            return instances.OrderBy(e=>e.Order);
        }

        private static void Main(string[] args)
        {
            ConfigureLog();
            foreach (var testCase in GetTestCases())
            {
                Output.WriteBigTitle(testCase.Title);
                testCase.Execute();
            }

            Pause();
        }

        private static void ConfigureLog()
        {
            var config = new LoggingConfiguration();

            var logconsole = new ColoredConsoleTarget("logconsole");

            config.AddRule(LogLevel.Warn, LogLevel.Fatal, logconsole);

            LogManager.Configuration = config;
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to sontinue...");
            Console.ReadLine();
        }

        #endregion Methods
    }
}