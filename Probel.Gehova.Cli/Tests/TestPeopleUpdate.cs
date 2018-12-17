using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;
using System.Collections.Generic;

namespace Probel.Gehova.Cli.Tests
{
    public class TestPeopleUpdate : ITestCase
    {
        #region Fields

        private readonly IProvisioningService _service = new ProvisioningService();

        #endregion Fields

        #region Properties

        private string RandomText => Guid.NewGuid().ToString();
        public int Order => 5;
        public string Title => "People Update";

        #endregion Properties

        #region Methods

        private void DisplayPeople() => Output.Write(_service.GetPeople());

        public void Execute()
        {
            var team = _service.GetTeam(1);
            var category1 = _service.GetCategory(1);
            var category2 = _service.GetCategory(2);
            var pickupRound = _service.GetPickupRound(1);

            var person = new PersonModel
            {
                FirstName = RandomText,
                LastName = RandomText,
                IsLunchTime = false,
                IsReceptionEvening = false,
                IsReceptionMorning = false,
                PickupRound = pickupRound,
                Categories = new List<PersonCategoryModel> { category1 },
                Team = team
            };
            _service.Create(person);
            DisplayPeople();

            Output.WriteTitle("Get data");
            var team2 = _service.GetTeam(2);
            Output.Write(team);

            Output.WriteTitle("Update people");
            person.FirstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            person.LastName = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";
            person.IsLunchTime = true;
            person.IsReceptionEvening = true;
            person.IsReceptionMorning = true;
            person.Team = team2;
            person.Categories = new List<PersonCategoryModel> { category2 };

            _service.Update(person);
            DisplayPeople();

            Output.WriteTitle("Remove people");
            _service.Remove(person);
            DisplayPeople();
        }

        #endregion Methods
    }
}