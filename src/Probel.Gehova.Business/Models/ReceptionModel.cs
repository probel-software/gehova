using Probel.Gehova.Business.I18n;
using System.Collections.Generic;
using System.Diagnostics;

namespace Probel.Gehova.Business.Models
{
    [DebuggerDisplay("ReceptionModel: {ReceptionName}")]
    public class ReceptionModel : BaseModel
    {
        #region Constructors

        public ReceptionModel()
        {
            Days = new List<DayModel>
            {
                new DayModel{ DayName = Strings.Monday, DayId = 1},
                new DayModel{ DayName = Strings.Tuesday, DayId = 2},
                new DayModel{ DayName = Strings.Wednesday, DayId = 3},
                new DayModel{ DayName = Strings.Thursday, DayId = 4},
                new DayModel{ DayName = Strings.Friday, DayId = 5},
            };

        }

        #endregion Constructors

        #region Properties

        public IList<DayModel> Days { get; set; }
        public string ReceptionName { get; set; }

        #endregion Properties
    }
}