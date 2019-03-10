using Probel.Gehova.Business.Models;
using System.Collections.Generic;
using System.Data;

namespace Probel.Gehova.Business.Mappers
{
    public static class Map
    {
        public static IEnumerable<ReceptionGroupModel> ToReceptionGroupModel(this IDataReader src) => new ToReceptionGroupMapper().Map(src);
        public static IEnumerable<DayPickupRoundModel> ToDayPickupRounds(this IDataReader src) => new ToPickupRoundMapper().Map(src);
        public static IEnumerable<DayModel> ToDayModel(this IDataReader src) => new ToDaysMapper().Map(src);
    }
}
