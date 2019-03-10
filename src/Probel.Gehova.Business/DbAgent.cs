using Microsoft.Data.Sqlite;
using NLog;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Lorm;
using System;
using System.Data;

namespace Probel.Gehova.Business
{
    public abstract class DbAgent
    {
        #region Fields

        private bool _hasTable = false;

        #endregion Fields

        #region Constructors

        static DbAgent()
        {
            var c = new LormConfigurator();
            c.AddMapper<WeekDay>(e => e.AsWeekday());
            c.AddMapper<AbsenceDisplayModel>(e => e.AsAbsenceDisplayModel());
            c.AddMapper<CategoryModel>(e => e.AsCategoryModel());
            c.AddMapper<PersonDisplayModel>(e => e.AsPersonDisplayModel());
            c.AddMapper<PersonFullDisplayModel>(e => e.AsPersonFullDisplayModel());
            c.AddMapper<SettingModel>(e => e.AsSettingModel());
            c.AddMapper<ReceptionModel>(e => e.AsReceptionModel());
            c.AddMapper<string>(e => e.AsString());
            c.AddMapper<RawPresenceWeekModel>(e => e.AsPresenceWeekRaw());
        }

        public DbAgent(IFileLocator dbLocator)
        {
            DbLocator = dbLocator;
        }

        #endregion Constructors

        #region Properties

        protected IFileLocator DbLocator { get; private set; }
        protected ILogger Log { get; } = LogManager.GetLogger("Service");

        #endregion Properties

        #region Methods
        //TODO: this code should be moved into a database manager
        private void CreateDatabase()
        {
            var assetManager = new AssetManager(this);
            var scripts = new string[]
            {
                "Probel.Gehova.Business.Assets.database.sql",
                "Probel.Gehova.Business.Assets.default_data.sql",
                "Probel.Gehova.Business.Assets.views_person.sql",
                "Probel.Gehova.Business.Assets.views_presences.sql",
                "Probel.Gehova.Business.Assets.views_settings.sql"
            };
            using (var c = new SqliteConnection(GetConnectionString()))
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
            var cs = $@"Filename={path};";
            return cs;
        }

        private bool HasTable()
        {
            if (_hasTable == true) { return true; }
            else
            {
                using (var c = new SqliteConnection(GetConnectionString()))
                {
                    var sql = @"
                        select count(name) as tablecount
                        from sqlite_master
                        where type='table';";
                    var result = c.Scalar<long>(sql);
                    _hasTable = result > 0;
                    return _hasTable;
                }
            }
        }

        protected static long GetLastId(IDbConnection c)
        {
            var sql = "select last_insert_rowid()";

            using (var cmd = c.CreateCommand(sql))
            {
                var lastId = cmd.ExecuteScalar();
                return (long)lastId;
            }
        }

        protected IDbCommand GetCommand(string sql, IDbConnection connection)
        {
            if (connection is SqliteConnection c)
            {
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                return c.CreateCommand(sql);
            }
            else { throw new NotSupportedException($"Connection of type '{connection.GetType()}' is not supported."); }
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

            if (!DbLocator.CheckDbExist() || !HasTable())
            {
                Log.Info($"Database does not exists. Creation using this connection string '{cs}'");
                CreateDatabase();
            }

            return new SqliteConnection(cs);
        }

        #endregion Methods
    }
}