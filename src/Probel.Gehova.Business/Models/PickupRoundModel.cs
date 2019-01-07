using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PickupRoundModel : PickupRoundDisplayModel
    {
        #region Constructors

        public PickupRoundModel()
        {
        }

        public PickupRoundModel(PickupRoundDisplayModel src)
        {
            Name = src.Name;
            Id = src.Id;
        }

        #endregion Constructors

        #region Properties

        public IList<PersonDisplayModel> People { get; set; }

        #endregion Properties
    }
}