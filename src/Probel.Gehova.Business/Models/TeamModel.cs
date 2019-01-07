using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class TeamModel : TeamDisplayModel
    {
        #region Constructors

        public TeamModel()
        {
        }

        public TeamModel(TeamDisplayModel src)
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