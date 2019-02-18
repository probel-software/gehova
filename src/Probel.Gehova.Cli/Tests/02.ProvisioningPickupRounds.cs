using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;

namespace Probel.Gehova.Cli.Tests
{
    public sealed class ProvisioningPickupRounds : ITestCase
    {
        #region Fields

        private readonly IProvisioningService _service;

        #endregion Fields

        #region Constructors

        public ProvisioningPickupRounds()
        {
            var dbl = new MyDocumentLocator();
            _service = new ProvisioningService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 2;
        public string Title => "Pickup Rounds";

        #endregion Properties

        #region Methods

        private void DisplayCategories()
        {
            var rounds = _service.GetPickupRounds();

            foreach (var r in rounds)
            {
                Output.WriteLine($"[{r.Id,-2}] - {r.Name,-10}");
            }
        }

        public void Execute()
        {
            Output.WriteTitle("Get pickup rounds");
            DisplayCategories();

            var pickup = new GroupDisplayModel() { Name = Guid.NewGuid().ToString() };
            Output.WriteTitle("Add new  pickup round");
            _service.CreatePickupRound(pickup);
            DisplayCategories();

            Output.WriteTitle("Update  pickup round name");
            pickup.Name += "zzzz";
            _service.UpdateTeam(pickup);
            DisplayCategories();

            Output.WriteTitle("Delete  pickup rounds");
            _service.Remove(pickup);
            DisplayCategories();
        }

        #endregion Methods
    }
}