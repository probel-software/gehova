using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;
using System.Collections.Generic;

namespace Probel.Gehova.Cli.Tests
{
    public sealed class ProvisioningPeopleCreate : ITestCase
    {
        #region Fields

        private readonly IProvisioningService _service;

        #endregion Fields

        #region Constructors

        public ProvisioningPeopleCreate()
        {
            var dbl = new MyDocumentLocator();
            _service = new ProvisioningService(dbl);
        }

        #endregion Constructors

        #region Properties

        private string RandomText => Guid.NewGuid().ToString();
        public int Order => 4;
        public string Title => "People Create";

        #endregion Properties

        #region Methods

        private void DisplayPeople() => Output.Write(_service.GetPeople());

        public void Execute()
        {
            Output.WriteTitle("Get people");
            DisplayPeople();

            Output.WriteTitle("Get data");
            var team = _service.GetTeam(1);
            Output.Write(team);

            var category1 = _service.GetCategory(1);
            Output.Write(category1);

            var category2 = _service.GetCategory(2);
            Output.Write(category2);

            var pickupRound = _service.GetPickupRound(1);
            Output.Write(pickupRound);

            Output.WriteTitle("Create new person");
            var person = new PersonModel
            {
                FirstName = RandomText,
                LastName = RandomText,
                PickupRound = pickupRound,
                Categories = new List<CategoryModel> { category1, category2 },
                Team = team
            };
            _service.Create(person);
            DisplayPeople();

            Output.WriteTitle("Remove people");
            _service.Remove(person);
            DisplayPeople();
        }

        #endregion Methods
    }
}