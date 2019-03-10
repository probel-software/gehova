using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PersonModel : PersonDisplayModel
    {
        #region Properties

        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<ReceptionModel> Receptions { get; set; }
        public GroupDisplayModel PickupRound { get; set; }
        public GroupDisplayModel Team { get; set; }

        #endregion Properties
    }
}