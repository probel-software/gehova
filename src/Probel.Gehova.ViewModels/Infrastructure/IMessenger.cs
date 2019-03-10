namespace Probel.Gehova.ViewModels.Infrastructure
{
    public interface IUserMessenger
    {
        void Say(string message);
        void Warn(string message);
    }
}
