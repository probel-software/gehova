using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

    public static class DayModelExtension
    {
        public static void Reorder(this DayModel src)
        {
            foreach (var team in src.Teams)
            {
                var ordered = (from p in team.People
                               orderby p.CategoryKey ascending
                                     , p.LastName ascending
                                     , p.FirstName ascending
                               select p).ToList();
                team.People = ordered;
            }
        }

        public static void Reorder(this IEnumerable<DayModel> src)
        {
            foreach (var day in src) { day.Reorder(); }
        }
    }
}
