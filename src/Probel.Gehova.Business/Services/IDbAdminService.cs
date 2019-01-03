namespace Probel.Gehova.Business.Services
{
    public interface IDbAdminService
    {
        #region Methods

        void ExecuteScript(string sql);

        #endregion Methods
    }
}