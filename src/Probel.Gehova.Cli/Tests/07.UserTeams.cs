using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System.Collections.Generic;

namespace Probel.Gehova.Cli.Tests
{
    public class UserTeams : ITestCase
    {
        #region Fields

        private readonly IUserService _service;

        #endregion Fields

        #region Constructors

        public UserTeams()
        {
            var dbl = new MyDocumentLocator();
            _service = new UserService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 7;
        public string Title => "Teams (UserService)";

        #endregion Properties

        #region Methods

        private void DisplayTeams()
        {
            var teams = _service.GetTeams();
            Output.WriteLine("List teams");
            foreach (var team in teams) { Output.Write(team); }
        }

        public void Execute()
        {
            DisplayTeams();
            Output.WriteTitle("Update team");
            var team1 = _service.GetTeam(1);
            team1.People.Remove(team1.People[0]);
            //team1.People.Remove(team1.People[1]);

            var team2 = _service.GetTeam(2);
            var p = new List<PersonDisplayModel>();
            team2.People.Add(_service.GetPerson(4));
            team2.People.Add(_service.GetPerson(5));

            Output.WriteTitle("Updating team");
            _service.Update(team1);
            _service.Update(team2);

            Output.WriteTitle("Check update");
            DisplayTeams();
        }

        #endregion Methods
    }
}