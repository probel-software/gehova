using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IVisualisationService
    {
        #region Methods

        IEnumerable<DayPickupRoundModel> GetPickupRounds();

        IEnumerable<ReceptionGroupModel> GetReceptionGroups();

        IEnumerable<DayModel> GetAllTeams();

        DateTime GetSelectedWeekAsFriday();

        DateTime GetSelectedWeekAsMonday();

        void SetSelectedWeek(DateTime date);

        #endregion Methods
    }
}