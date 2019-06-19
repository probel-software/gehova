using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;

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

    internal static class ReceptionGroupModelExtensions
    {
        public static void Reorder(this ReceptionGroupModel src)
        {
            foreach (var reception in src.Receptions)
            {
                foreach (var day in reception.Days)
                {
                    foreach (var team in day.Teams)
                    {
                        var ordered = (from p in team.People
                                       orderby p.CategoryKey ascending
                                             , p.LastName ascending
                                             , p.FirstName ascending
                                       select p).ToList();
                        team.People = ordered;
                    }

                }
            }
        }

        public static void Reorder(this IEnumerable<ReceptionGroupModel> src)
        {
            foreach (var reception in src) { reception.Reorder(); }
        }
    }
}
