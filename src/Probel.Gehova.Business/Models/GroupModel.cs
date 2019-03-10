using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class GroupModel : GroupDisplayModel
    {
        #region Constructors

        public GroupModel()
        {
            People = new List<PersonDisplayModel>();
        }

        public GroupModel(GroupDisplayModel src) : this()
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
