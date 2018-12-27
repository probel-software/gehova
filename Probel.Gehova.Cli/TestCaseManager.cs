using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using Probel.Gehova.Cli.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Cli
{
    public static class TestCaseManager
    {
        #region Fields

        private static IEnumerable<ITestCase> _testCases = null;

        #endregion Fields

        #region Properties

        private static IEnumerable<ITestCase> TestCases
        {
            get
            {
                if (_testCases == null) { _testCases = GetTestCases(); }
                return _testCases;
            }
        }

        #endregion Properties

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

            return instances.OrderBy(e => e.Order);
        }

        public static void ExecuteAll()
        {
            foreach (var testCase in TestCases)
            {
                Output.WriteBigTitle(testCase.Title);
                testCase.Execute();
            }
        }

        public static void Execute(int order)
        {
            var testcase = (from t in TestCases
                            where t.Order == order
                            select t).FirstOrDefault();
            if (testcase != null)
            {
                Output.WriteBigTitle(testcase.Title);
                testcase.Execute();
            }
            else
            {
                Output.WriteError($"Test case n° {order} not found!");
            }
        }

        public static void ExecuteLast()
        {
            var testcase = (from t in TestCases
                            where t.Order == TestCases.Max(e => e.Order)
                            select t).FirstOrDefault();

            Output.WriteBigTitle(testcase.Title);
            testcase.Execute();
        }

        public static void ResetData()
        {
            var script = @"D:\Projects\gehova\sql\test_data.sql";
            var service = new DbAdminService();
            service.ExecuteScript(script);
        }
        #endregion Methods
    }
}