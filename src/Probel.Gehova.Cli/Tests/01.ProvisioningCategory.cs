using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;

namespace Probel.Gehova.Cli.Tests
{
    public sealed class ProvisioningCategory : ITestCase
    {
        #region Fields

        private readonly IProvisioningService _service = new ProvisioningService();

        #endregion Fields

        #region Properties

        public int Order => 1;
        public string Title => "Categories";

        #endregion Properties

        #region Methods

        private void DisplayCategories()
        {
            var categories = _service.GetCategories();

            foreach (var cat in categories)
            {
                Output.WriteLine($"[{cat.Id,-2}] - {cat.Display,-10} ({cat.Key})");
            }
        }

        public void Execute()
        {
            Output.WriteTitle("Get categories");
            DisplayCategories();

            var cat = new CategoryModel() { Display = Guid.NewGuid().ToString(), Key = "zz" };
            Output.WriteTitle("Add new category");
            _service.Create(cat);
            DisplayCategories();

            Output.WriteTitle("Update category name");
            cat.Display += "zzzz";
            _service.Update(cat);
            DisplayCategories();

            Output.WriteTitle("Delete category");
            _service.Remove(cat);
            DisplayCategories();
        }

        #endregion Methods
    }
}