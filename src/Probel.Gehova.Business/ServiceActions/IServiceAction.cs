namespace Probel.Gehova.Business.ServiceActions
{
    public interface IServiceAction<TContext>
    {
        #region Methods

        IServiceAction<TContext> Execute();

        object Result { get; }

        IServiceAction<TContext> WithContext(TContext context);

        #endregion Methods
    }
}