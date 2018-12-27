using Dapper;
using Probel.Gehova.Business.Services;
using System.IO;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class DbAdminService : DbAgent, IDbAdminService
    {
        #region Methods

        public void ExecuteScript(string path) => InTransaction(c =>
        {
            if (File.Exists(path))
            {
                var sql = File.ReadAllText(path);
                c.Execute(sql);
            }
            else { throw new FileNotFoundException($"The SQL script file not found. Specified path '{path}'."); }
        });

        #endregion Methods
    }
}