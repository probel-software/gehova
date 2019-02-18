using Microsoft.Data.Sqlite;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.ServiceActions;
using Probel.Gehova.Business.Services;
using Probel.Lorm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class UpdateService : DbAgent, IUpdateService
    {
        #region Fields

        private readonly AssetManager AssetManager;
        private readonly List<string> Updates;
        private readonly List<string> Views;

        #endregion Fields

        #region Constructors

        public UpdateService(IDbLocator dbLocator) : base(dbLocator)
        {
            var asm = Assembly.GetExecutingAssembly().GetManifestResourceNames().ToList();
            Views = (from asset in asm
                     where asset.StartsWith("Probel.Gehova.Business.Assets.views_")
                     select asset).ToList();

            Updates = (from asset in asm
                       where asset.StartsWith("Probel.Gehova.Business.Assets.Scripts")
                       orderby asset
                       select asset).ToList();
            AssetManager = new AssetManager(this);
        }

        #endregion Constructors

        #region Methods

        private string GetConnectionString()
        {
            var path = DbLocator.GetDatabasePath();
            var cs = $@"Filename={path};";
            return cs;
        }

        private SettingModel GetVersion(IDbConnection c)
        {
            var sql = @"
                    select id    as Id
                         , key   as Key
                         , value as Value
                    from settings where key = 'db_version'";
            return c.Query<SettingModel>(sql).FirstOrDefault();
        }

        private IEnumerable<string> GetViews(IDbConnection c)
        {
            var sql = @"
                select name
                from sqlite_master
                where type = 'view'";
            return c.Query<string>(sql).ToList();
        }

        private void SetVersion(IDbConnection c, Version v)
        {
            var sql = $@"
                update settings
                set
                    value = '{v.ToString(3)}'
                where key = 'db_version'";
            c.Execute(sql);
        }

        private void UpdateDbStructure(SqliteConnection c, Version from)
        {

            var updater = new DatabaseUpdater(Updates, AssetManager);
            var scripts = updater.GetSqlScripts(from);
            foreach (var script in scripts)
            {
                c.Execute(script);
            }
        }

        private void UpdateViews(SqliteConnection c)
        {
            foreach (var script in Views)
            {
                var sql = AssetManager.GetScript(script);
                c.Execute(sql);
            }
        }

        public bool CheckVersion(Version v)
        {
            using (var c = NewConnection())
            {
                string sql;
                var dbVersion = GetVersion(c);

                if (dbVersion != null)
                {
                    var version = new Version(v.Major, v.Minor, v.Build);
                    if (Version.TryParse(dbVersion.Value, out var cu))
                    {
                        var r = (version == new Version(cu.Major, cu.Minor, cu.Build));
                        return r;
                    }
                    else { return false; }
                }
                else
                {
                    sql = $@"insert into settings (key, value) values ('db_version', '{v.ToString(3)}')";
                    c.Execute(sql);
                    return false;
                }
            }
        }

        public void UpdateToVersion(Version version)
        {
            using (var c = new SqliteConnection(GetConnectionString()))
            {
                c.Open();
                using (var t = c.BeginTransaction())
                {
                    UpdateDbStructure(c, GetVersion(c).Value.AsVersion());
                    UpdateViews(c);
                    SetVersion(c, version);
                    t.Commit();
                }
            }
        }

        #endregion Methods
    }
}