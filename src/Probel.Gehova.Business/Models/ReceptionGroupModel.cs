using System.Collections.Generic;
using System.Diagnostics;

namespace Probel.Gehova.Business.Models
{
    [DebuggerDisplay("ReceptionGroupModel: {ReceptionGroupName} [{Id}]")]
    public class ReceptionGroupModel
    {
        #region Constructors

        public ReceptionGroupModel()
        {
            Receptions = new List<ReceptionModel>();
        }

        #endregion Constructors

        #region Properties

        public long Id { get; internal set; }
        public string ReceptionGroupName { get; set; }
        public IList<ReceptionModel> Receptions { get; set; }

        #endregion Properties
    }
}