using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using Probel.Lorm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class VisualisationService : DbAgent, IVisualisationService
    {
        #region Constructors

        public VisualisationService(IDbLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Methods

        private IEnumerable<WeekDay> GetLunchtime(DateTime? date) => InTransaction(c =>
        {
            if (date.HasValue) { SetSelectedWeek(date.Value); }

            var sql = @"
                select day_name   as Day
                     , team       as Team
                     , first_name as FirstName
                     , last_name  as LastName
                from lunchtime_v";
            var result = c.Query<WeekDay>(sql);
            return result;
        });

        private IEnumerable<WeekDay> GetPickupRounds(DateTime? date) => InTransaction(c =>
        {
            if (date.HasValue) { SetSelectedWeek(date.Value); }

            var sql = @"
                select day_name     as Day
                     , team         as Team
                     , pickup_round as PickupRound
                     , first_name   as FirstName
                     , last_name    as LastName
                from pickup_rounds_v";
            var result = c.Query<WeekDay>(sql);
            return result;
        });

        private IEnumerable<WeekDay> GetReceptionEvening(DateTime? date) => InTransaction(c =>
        {
            if (date.HasValue) { SetSelectedWeek(date.Value); }

            var sql = @"
                select day_name   as Day
                     , team       as Team
                     , first_name as FirstName
                     , last_name  as LastName
                from reception_evening_v";
            var result = c.Query<WeekDay>(sql);
            return result;
        });

        private IEnumerable<WeekDay> GetReceptionMorning(DateTime? date) => InTransaction(c =>
        {
            if (date.HasValue) { SetSelectedWeek(date.Value); }

            var sql = @"
                select day_name   as Day
                     , team       as Team
                     , first_name as FirstName
                     , last_name  as LastName
                from reception_morning_v";
            var result = c.Query<WeekDay>(sql);
            return result;
        });

        private DateTime GetSelectedWeekAs(string day)
        {
            using (var c = NewConnection())
            {
                var sql = $@"select {day} from settings_weekday_v";
                var strResult = c.Query<string>(sql)
                                .FirstOrDefault();

                return (DateTime.TryParse(strResult, out var dateResult))
                    ? dateResult
                    : throw new InvalidCastException($"The string '{strResult}' cannot be casted as a '{typeof(DateTime)}'");
            }
        }

        public IEnumerable<WeekDay> GetLunchtime(DateTime date) => GetLunchtime(date);

        public IEnumerable<WeekDay> GetLunchtime() => GetLunchtime(null);

        public IEnumerable<WeekDay> GetPickupRounds() => GetPickupRounds(null);

        public IEnumerable<WeekDay> GetReceptionEvening(DateTime date) => GetReceptionEvening(date);

        public IEnumerable<WeekDay> GetReceptionEvening() => GetReceptionEvening(null);

        public IEnumerable<WeekDay> GetReceptionMorning(DateTime date) => GetReceptionMorning(date);

        public IEnumerable<WeekDay> GetReceptionMorning() => GetReceptionMorning(null);

        public DateTime GetSelectedWeekAsFriday() => GetSelectedWeekAs("friday");

        public DateTime GetSelectedWeekAsMonday() => GetSelectedWeekAs("monday");

        public void SetSelectedWeek(DateTime date)
        {
            var sql = @"
                    update settings
                    set
                        ""value"" = @SetDate
                    where ""key"" = 'week_date'";

            using (var c = NewConnection())
            {
                c.Execute(sql, new { SetDate = date });
            }
        }

        #endregion Methods
    }
}