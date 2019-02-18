using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;

namespace Probel.Gehova.Cli.Tests
{
    public sealed class ProvisioningTeams : ITestCase
    {
        #region Fields

        private readonly IProvisioningService _service;

        #endregion Fields

        #region Constructors

        public ProvisioningTeams()
        {
            var dbl = new MyDocumentLocator();
            _service = new ProvisioningService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 3;
        public string Title => "Teams (ProvisioningService)";

        #endregion Properties

        #region Methods

        private void DisplayTeams()
        {
            var teams = _service.GetTeams();

            foreach (var team in teams)
            {
                Output.WriteLine($"[{team.Id,-2}] - {team.Name}");
            }
        }

        public void Execute()
        {
            Output.WriteTitle("Get teams");
            DisplayTeams();

            var team = new GroupDisplayModel() { Name = Guid.NewGuid().ToString() };
            Output.WriteTitle("Add new team");
            _service.CreatePickupRound(team);
            DisplayTeams();

            Output.WriteTitle("Update team name");
            team.Name += "zzzz";
            _service.UpdateTeam(team);
            DisplayTeams();

            Output.WriteTitle("Delete team");
            _service.Remove(team);
            DisplayTeams();
        }

        #endregion Methods
    }
}