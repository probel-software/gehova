using Probel.Gehova.ViewModels.Settings;

namespace Probel.Gehova.Views.Infrastructure
{
    public interface IViewModelFactory
    {
        SettingsHomeViewModel SettingsHomeViewModel { get; }
    }
}