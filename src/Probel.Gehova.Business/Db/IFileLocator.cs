namespace Probel.Gehova.Business.Db
{
    public interface IFileLocator
    {
        #region Methods

        bool CheckDbExist();

        string GetDatabasePath();
        #endregion Methods
    }
}