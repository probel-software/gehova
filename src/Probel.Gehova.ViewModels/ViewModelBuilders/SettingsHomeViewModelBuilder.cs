using Probel.Gehova.ViewModels.Vm.Settings;
using System.Collections.ObjectModel;

namespace Probel.Gehova.ViewModels.ViewModelBuilders
{
    public static class SettingsHomeViewModelBuilder
    {
        #region Methods

        public static AddPersonViewModel HandDown(this SettingsHomeViewModel src, AddPersonViewModel vm)
        {
            vm.Categories = new ObservableCollection<CategoryViewModel>(src.Categories.ToViewModel());
            vm.Receptions = new ObservableCollection<ReceptionViewModel>(src.Receptions.ToViewModel());
            return vm;
        }

        #endregion Methods
    }
}