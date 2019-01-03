using System;
using System.IO;

namespace Probel.Gehova.Business.Db
{
    public class MyDocumentLocator : BaseDbLocator
    {
        #region Fields

        private string _databasePath = null;

        #endregion Fields

        #region Methods

        protected override string BuildDatabasePath()
        {
            if (string.IsNullOrEmpty(_databasePath))
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _databasePath = Path.Combine(path, RELATIVE_PATH);
            }
            return _databasePath;
        }

        #endregion Methods
    }
}