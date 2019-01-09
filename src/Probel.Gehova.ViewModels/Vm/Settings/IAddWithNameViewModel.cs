using System.Windows.Input;

namespace Probel.Gehova.ViewModels.Vm.Settings
{
    public interface IAddWithNameViewModel
    {
        #region Properties

        ICommand AddCommand { get; }
        bool IsAbleToAdd { get; set; }
        string Name { get; set; }
        string PrimaryButtonText { get; }
        string SecondaryButtonText { get; }
        string Title { get; }

        #endregion Properties
    }
}