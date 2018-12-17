namespace Probel.Gehova.Business.ServiceActions
{
    internal interface IServiceAction<TContext>
    {
        #region Methods

        void Execute();

        IServiceAction<TContext> WithContext(TContext context);

        #endregion Methods
    }
}