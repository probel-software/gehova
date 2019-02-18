using System.Collections.Generic;
using System.Diagnostics;

namespace Probel.Gehova.Business.Models
{
    [DebuggerDisplay("ReceptionModel: {DayName} [{DayId}]")]
    public class DayModel
    {
        #region Constructors

        public DayModel()
        {
            Teams = new List<GroupModel>();
        }

        #endregion Constructors

        #region Properties

        public long DayId { get; set; }

        public string DayName { get; set; }

        public IList<GroupModel> Teams { get; set; }

        #endregion Properties
    }
}