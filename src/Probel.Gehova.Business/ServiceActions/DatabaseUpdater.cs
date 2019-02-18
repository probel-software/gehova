using Microsoft.Data.Sqlite;
using Probel.Gehova.Business.Helpers;
using Probel.Lorm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Business.ServiceActions
{
    public class DatabaseUpdater
    {
        #region Fields

        private readonly AssetManager AssetManager;
        private readonly List<string> UpdateScripts;
        private readonly int SLENGTH = "Probel.Gehova.Business.Assets.Scripts.".Length + 2;

        #endregion Fields

        #region Constructors

        public DatabaseUpdater(List<string> scripts, AssetManager assetManager)
        {
            UpdateScripts = scripts;
            AssetManager = assetManager;
        }

        #endregion Constructors

        #region Methods

        private IEnumerable<string> GetVersionsToApply(Version version)
        {
            var scripts = (from v in UpdateScripts
                           where new Version(v.Substring(SLENGTH, v.Length - SLENGTH).Replace(".sql", "")) > version
                           select v).ToList();
            return scripts;
        }

        public IEnumerable<string> GetSqlScripts(Version version)
        {
            foreach (var script in GetVersionsToApply(version))
            {
                yield return AssetManager.GetScript(script);
            }
        }

        public void Update(SqliteConnection c, Version from)
        {
            var versionToApply = GetSqlScripts(from);

            foreach (var script in versionToApply)
            {
                var sql = AssetManager.GetScript(script);
                c.Execute(sql);
            }
        }

        #endregion Methods
    }
}