using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class PersonFullDisplayModel : PersonDisplayModel
    {
        #region Properties

        public IEnumerable<long> CategoryIds { get; set; }
        public IEnumerable<long> ReceptionIds { get; set; }
        public string PickupRound { get; set; }
        public long PickupRoundId { get; set; }
        public string Team { get; set; }
        public long TeamId { get; set; }

        #endregion Properties

        #region Methods

        public override string ToString() => $"{FirstName} {LastName}";

        #endregion Methods
    }
}