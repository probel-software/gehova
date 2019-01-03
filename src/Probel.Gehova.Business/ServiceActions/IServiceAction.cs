namespace Probel.Gehova.Business.ServiceActions
{
    public interface IServiceAction<TContext>
    {
        #region Methods

        object Execute();

        IServiceAction<TContext> WithContext(TContext context);

        #endregion Methods
    }
}