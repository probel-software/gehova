using System;

namespace Probel.Gehova.Business.Models
{
    public class AbsenceDisplayModel : BaseModel
    {
        #region Properties

        public string FirstName { get; set; }
        public DateTime From { get; set; }
        public string LastName { get; set; }
        public long PersonId { get; set; }
        public DateTime To { get; set; }

        #endregion Properties
    }
}