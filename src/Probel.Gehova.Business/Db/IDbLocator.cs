namespace Probel.Gehova.Business.Db
{
    public interface IDbLocator
    {
        #region Methods

        bool CheckDbExist();

        string GetDatabasePath();

        #endregion Methods
    }
}