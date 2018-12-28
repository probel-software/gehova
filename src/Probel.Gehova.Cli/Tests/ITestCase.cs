namespace Probel.Gehova.Cli.Tests
{
    public interface ITestCase
    {
        #region Properties

        string Title { get; }

        int Order { get; }

        #endregion Properties

        #region Methods

        void Execute();

        #endregion Methods
    }
}