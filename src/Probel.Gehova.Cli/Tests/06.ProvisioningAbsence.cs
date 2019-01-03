using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;
using System;

namespace Probel.Gehova.Cli.Tests
{
    public class ProvisioningAbsence : ITestCase
    {
        #region Fields

        private readonly IUserService _service;

        #endregion Fields

        #region Constructors

        public ProvisioningAbsence()
        {
            var dbl = new MyDocumentLocator();
            _service = new UserService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 6;
        public string Title => "Absences";

        #endregion Properties

        #region Methods

        private void DisplayAbsences()
        {
            var absences = _service.GetAbsences();
            Output.WriteLine("List absences");
            foreach (var absence in absences)
            {
                Output.Write(absence);
            }
        }

        public void Execute()
        {
            var person = _service.GetPerson(1);
            var dt = DateTime.Now;

            Output.Write(person);
            DisplayAbsences();

            var absence = new AbsenceModel
            {
                Person = person,
                From = dt.AddDays(120),
                To = dt.AddDays(126)
            };

            Output.WriteTitle("Create absence");
            _service.Create(absence);
            DisplayAbsences();

            Output.WriteTitle("Update absence");
            absence.From = DateTime.Today;
            absence.To = DateTime.Today.AddDays(1);
            _service.Update(absence);
            DisplayAbsences();

            Output.WriteTitle("Delete absence");
            _service.Remove(absence);
            DisplayAbsences();
        }

        #endregion Methods
    }
}