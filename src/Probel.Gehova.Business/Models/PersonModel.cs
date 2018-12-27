using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PersonModel : PersonDisplayModel
    {
        #region Properties

        public IEnumerable<CategoryModel> Categories { get; set; }
        public bool IsLunchTime { get; set; }
        public bool IsReceptionEvening { get; set; }
        public bool IsReceptionMorning { get; set; }
        public PickupRoundDisplayModel PickupRound { get; set; }
        public TeamDisplayModel Team { get; set; }

        #endregion Properties
    }
}