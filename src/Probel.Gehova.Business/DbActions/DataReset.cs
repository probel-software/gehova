using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.ServicesImpl;

namespace Probel.Gehova.Business.Helpers
{
    public class DataReset : IDataReset
    {
        #region Fields

        private readonly IDbLocator _locator;

        #endregion Fields

        #region Constructors

        public DataReset(IDbLocator locator)
        {
            _locator = locator;
        }

        #endregion Constructors

        #region Methods

        public void Execute()
        {
            var rn = "Probel.Gehova.Business.Assets.test_data.sql";
            var sql = new AssetManager(this).GetScript(rn);

            var service = new DbAdminService(_locator);
            service.ExecuteScript(sql);
        }

        #endregion Methods
    }
}