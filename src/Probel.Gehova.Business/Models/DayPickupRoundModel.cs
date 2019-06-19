using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Business.Models
{
    public class DayPickupRoundModel
    {
        #region Constructors

        public DayPickupRoundModel()
        {
            PickupRounds = new List<GroupModel>();
        }

        #endregion Constructors

        #region Properties

        public long DayId { get; internal set; }
        public string DayName { get; set; }
        public IList<GroupModel> PickupRounds { get; set; }

        #endregion Properties
    }

    public static class DayPickupRoundModelExtensions
    {
        public static void Reorder(this DayPickupRoundModel src)
        {
            var orderedP = (from p in src.PickupRounds
                            orderby p.Name
                            select p).ToList();
            src.PickupRounds = orderedP;

            foreach (var pickup in src.PickupRounds)
            {
                var ordered = (from p in pickup.People
                               orderby p.CategoryKey ascending
                                     , p.LastName ascending
                                     , p.FirstName ascending
                               select p).ToList();
                pickup.People = ordered;
            }
        }
        public static void Reorder(this IEnumerable<DayPickupRoundModel> src)
        {
            foreach (var pickup in src) { pickup.Reorder(); }
        }
    }
}
