using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class DayPickupRoundModel
    {
        #region Constructors

        public DayPickupRoundModel()
        {
            PickupRounds = new List<GroupModel>();
        }

        #endregion Constructors

        #region Properties

        public long DayId { get; internal set; }
        public string DayName { get; set; }
        public IList<GroupModel> PickupRounds { get; set; }

        #endregion Properties
    }
}