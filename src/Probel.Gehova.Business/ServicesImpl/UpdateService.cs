using Microsoft.Data.Sqlite;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Lorm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class UpdateService : DbAgent, IUpdateService
    {
        #region Fields

        private readonly List<string> Scripts;

        #endregion Fields

        #region Constructors

        public UpdateService(IDbLocator dbLocator) : base(dbLocator)
        {
            Scripts = new List<string>
            {
                "Probel.Gehova.Business.Assets.views_person.sql",
                "Probel.Gehova.Business.Assets.views_absences.sql",
                "Probel.Gehova.Business.Assets.views_absence_on_date.sql",
                "Probel.Gehova.Business.Assets.views_settings.sql"
            };
        }

        #endregion Constructors

        #region Methods

        private string GetConnectionString()
        {
            var path = DbLocator.GetDatabasePath();
            var cs = $@"Filename={path};";
            return cs;
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

        public bool CheckVersion(Version v)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id    as Id
                         , key   as Key
                         , value as Value
                    from settings where key = 'db_version'";

                var result = c.Query<SettingModel>(sql).FirstOrDefault();

                if (result != null)
                {
                    var version = new Version(v.Major, v.Minor, v.Build);
                    if (Version.TryParse(result.Value, out var cu))
                    {
                        var r = (version == new Version(cu.Major, cu.Minor, cu.Build));
                        return r;
                    }
                    else { return false; }
                }
                else
                {
                    var ver = new Version(1, 0, 0);
                    sql = $@"insert into settings (key, value) values ('db_version', '{ver.ToString(3)}')";
                    c.Execute(sql);
                    return false;
                }
            }
        }

        public void UpdateToVersion(Version version)
        {
            var assetManager = new AssetManager(this);
            using (var c = new SqliteConnection(GetConnectionString()))
            {
                c.Open();
                using (var t = c.BeginTransaction())
                {
                    foreach (var script in Scripts)
                    {
                        var sql = assetManager.GetScript(script);
                        c.Execute(sql);
                    }
                    SetVersion(c, version);
                    t.Commit();
                }
            };
        }

        #endregion Methods
    }
}