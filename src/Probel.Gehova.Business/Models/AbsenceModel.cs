using System;

namespace Probel.Gehova.Business.Models
{
    public class AbsenceModel : BaseModel
    {
        #region Properties

        public DateTime From { get; set; }
        public PersonDisplayModel Person { get; set; }
        public DateTime To { get; set; }

        #endregion Properties
    }
}