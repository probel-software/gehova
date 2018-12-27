using System.Collections.Generic;

namespace Probel.Gehova.Business.Models
{
    public class TeamModel : TeamDisplayModel
    {
        public IList<PersonDisplayModel> People { get; set; }
    }
}
