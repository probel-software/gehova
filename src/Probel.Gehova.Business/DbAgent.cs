using Dapper;
using NLog;
using System;
using System.Data;
using System.Data.SQLite;

namespace Probel.Gehova.Business
{
    public abstract class DbAgent
    {
        #region Properties

        protected ILogger Log { get; } = LogManager.GetLogger("Service");

        #endregion Properties

        #region Methods

        protected static long GetLastId(IDbConnection c)
        {
            var sql = "select last_insert_rowid()";
            var lastId = c.QuerySingle<long>(sql);
            return lastId;
        }

        protected void InTransation(Action<IDbConnection> action)
        {
            using (var c = NewConnection())
            {
                c.Open();
                Log.Trace("Start transaction");
                using (var t = c.BeginTransaction())
                {
                    action(c);
                    Log.Trace("Commiting transaction...");
                    t.Commit();
                }
            }
        }

        protected IDbConnection NewConnection()
        {
            var cs = @"Data Source=D:\Binaries\geho_db.db;Version=3;";
            Log.Trace($"Creating new connection at '{cs}'");
            return new SQLiteConnection(cs);
        }

        #endregion Methods
    }
}