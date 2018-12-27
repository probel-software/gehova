using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PickupRoundModel : PickupRoundDisplayModel
    {
        public IList<PersonDisplayModel> People { get; set; }
    }
}
