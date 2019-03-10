using System.IO;

namespace Probel.Gehova.Business.Db
{
    public abstract class BaseFileLocator : IFileLocator
    {
        #region Fields

        private static bool _isChecked = false;
        protected const string RELATIVE_PATH = @"TeamPlanner\database.db";

        #endregion Fields

        #region Methods

        private void CreateDirectory()
        {
            var dir = Path.GetDirectoryName(BuildDatabasePath());
            if (Directory.Exists(dir) == false) { Directory.CreateDirectory(dir); }
        }

        protected abstract string BuildDatabasePath();

        public bool CheckDbExist() => File.Exists(BuildDatabasePath());

        public string GetDatabasePath()
        {
            if (!_isChecked)
            {
                CreateDirectory();
                _isChecked = true;
            }
            return BuildDatabasePath();
        }


        #endregion Methods
    }
}