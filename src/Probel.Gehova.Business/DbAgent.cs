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

        protected TResult InTransaction<TResult>(Func<IDbConnection, TResult> func)
        {
            using (var c = NewConnection())
            {
                c.Open();
                Log.Trace("Start transaction");
                using (var t = c.BeginTransaction())
                {
                    var result = func(c);
                    Log.Trace("Commiting transaction...");
                    t.Commit();
                    return result;
                }
            }
        }

        protected void InTransaction(Action<IDbConnection> action) => InTransaction(c =>
        {
            action(c);
            return 0;
        });

        protected IDbConnection NewConnection()
        {
            var cs = @"Data Source=D:\Binaries\geho_db.db;Version=3;";
            Log.Trace($"Creating new connection at '{cs}'");
            return new SQLiteConnection(cs);
        }

        #endregion Methods
    }
}