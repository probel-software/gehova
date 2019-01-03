using Dapper;
using NLog;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using System;
using System.Data;
using System.Data.SQLite;

namespace Probel.Gehova.Business
{
    public abstract class DbAgent
    {
        #region Constructors

        public DbAgent(IDbLocator dbLocator)
        {
            DbLocator = dbLocator;
        }

        #endregion Constructors

        #region Properties

        protected IDbLocator DbLocator { get; private set; }
        protected ILogger Log { get; } = LogManager.GetLogger("Service");

        #endregion Properties

        #region Methods

        private void CreateDatabase()
        {
            var assetManager = new AssetManager(this);
            var scripts = new string[]
            {
                "Probel.Gehova.Business.Assets.database.sql",
                "Probel.Gehova.Business.Assets.default_data.sql",
                "Probel.Gehova.Business.Assets.views_person.sql",
                "Probel.Gehova.Business.Assets.views_absences.sql",
                "Probel.Gehova.Business.Assets.views_absence_on_date.sql",
                "Probel.Gehova.Business.Assets.views_settings.sql"
            };
            using (var c = new SQLiteConnection(GetConnectionString()))
            {
                c.Open();
                using (var t = c.BeginTransaction())
                {
                    foreach (var script in scripts)
                    {
                        var sql = assetManager.GetScript(script);
                        c.Execute(sql);
                    }
                    t.Commit();
                }
            };
        }

        private string GetConnectionString()
        {
            var path = DbLocator.GetDatabasePath();
            var cs = $@"Data Source={path};Version=3;";
            return cs;
        }

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
            var cs = GetConnectionString();
            Log.Trace($"Creating new connection at '{cs}'");

            if (!DbLocator.CheckDbExist())
            {
                Log.Info($"Database does not exists. Creation using this connection string '{cs}'");
                CreateDatabase();
            }

            return new SQLiteConnection(cs);
        }

        #endregion Methods
    }
}