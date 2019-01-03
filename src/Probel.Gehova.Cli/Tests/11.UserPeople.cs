using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Services;
using Probel.Gehova.Business.ServicesImpl;
using Probel.Gehova.Cli.Helpers;

namespace Probel.Gehova.Cli.Tests
{
    public class UserPeople : ITestCase
    {
        #region Fields

        private readonly IUserService _service;

        #endregion Fields

        #region Constructors

        public UserPeople()
        {
            var dbl = new MyDocumentLocator();
            _service = new UserService(dbl);
        }

        #endregion Constructors

        #region Properties

        public int Order => 11;
        public string Title => "List all people";

        #endregion Properties

        #region Methods

        public void Execute()
        {
            var people = _service.GetPeople();
            Output.WriteTitle("People encoded in the database:");
            foreach (var person in people)
            {
                Output.Write(person);
            }
        }

        #endregion Methods
    }
}