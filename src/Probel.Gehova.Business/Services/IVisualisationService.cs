using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IVisualisationService
    {
        #region Methods

        IEnumerable<WeekDay> GetLunchtime(DateTime date);

        IEnumerable<WeekDay> GetLunchtime();

        IEnumerable<WeekDay> GetPickupRounds();

        IEnumerable<WeekDay> GetReceptionEvening(DateTime date);

        IEnumerable<WeekDay> GetReceptionEvening();

        IEnumerable<WeekDay> GetReceptionMorning(DateTime date);

        IEnumerable<WeekDay> GetReceptionMorning();

        DateTime GetSelectedWeekAsMonday();
        DateTime GetSelectedWeekAsFriday();

        void SetSelectedWeek(DateTime date);

        #endregion Methods
    }
}