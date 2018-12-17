using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PersonModel : BaseModel
    {
        #region Properties

        public IEnumerable<PersonCategoryModel> Categories { get; set; }
        public string FirstName { get; set; }
        public bool IsLunchTime { get; set; }
        public bool IsReceptionEvening { get; set; }
        public bool IsReceptionMorning { get; set; }
        public string LastName { get; set; }
        public PickupRoundModel PickupRound { get; set; }
        public TeamModel Team { get; set; }

        #endregion Properties
    }
}