using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System.Collections.Generic;

namespace Probel.Gehova.Cli.Tests
{
    public class UserPickupRounds : ITestCase
    {
        #region Fields

        private readonly IUserService _service = new UserService();

        #endregion Fields

        #region Properties

        public int Order => 8;
        public string Title => "Pickup Rounds";

        #endregion Properties

        #region Methods

        private void DisplayPickupRounds()
        {
            var rounds = _service.GetPickupRounds();
            Output.WriteLine("List pickup rounds");
            foreach (var round in rounds) { Output.Write(round); }
        }

        public void Execute()
        {
            DisplayPickupRounds();
            Output.WriteTitle("Update team");
            var round1 = _service.GetPickupRound(1);
            round1.People.Remove(round1.People[0]);
            round1.People.Remove(round1.People[1]);

            var round2 = _service.GetPickupRound(2);
            var p = new List<PersonDisplayModel>();
            round2.People.Add(_service.GetPerson(4));
            round2.People.Add(_service.GetPerson(5));

            Output.WriteTitle("Updating team");
            _service.Update(round1);
            _service.Update(round2);

            Output.WriteTitle("Check update");
            DisplayPickupRounds();
        }

        #endregion Methods
    }
}