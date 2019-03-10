using Probel.Gehova.Business.Db;
using System.IO;
using Windows.Storage;

namespace Probel.Gehova.Views.Helpers
{
    public class UwpDbLocator : BaseFileLocator
    {
        #region Fields

        private string _databasePath = null;

        #endregion Fields

        #region Methods

        protected override string BuildDatabasePath()
        {
            if (string.IsNullOrEmpty(_databasePath))
            {
                var result = ApplicationData.Current.LocalFolder;
                _databasePath = Path.Combine(result.Path, RELATIVE_PATH);
            }
            return _databasePath;
        }

        #endregion Methods
    }
}