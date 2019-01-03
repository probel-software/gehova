using Dapper;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Services;
using System.IO;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class DbAdminService : DbAgent, IDbAdminService
    {
        #region Constructors

        public DbAdminService(IDbLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Methods

        public void ExecuteScript(string sql) => InTransaction(c =>
        {
            if (string.IsNullOrWhiteSpace(sql)) { throw new FileNotFoundException($"The SQL script is empty."); }
            else
            {
                c.Execute(sql);
            }
        });

        #endregion Methods
    }
}