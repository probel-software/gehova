using Probel.Gehova.ViewModels.Vm.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.ViewModels.GuiUtils
{
    public static class ReceptionModelsExtensions
    {
        #region Methods

        public static IEnumerable<ReceptionViewModel> CheckUserReception(this IEnumerable<ReceptionViewModel> models, IEnumerable<long> ids)
        {
            foreach (var model in models)
            {
                model.IsSelected = (ids.Contains(model.Id));
            }
            return models;
        }

        #endregion Methods
    }
}