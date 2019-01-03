using Dapper;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
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

        public IEnumerable<WeekDay> GetLunchtime(DateTime date) => GetLunchtime(date);

        public IEnumerable<WeekDay> GetLunchtime() => GetLunchtime(null);

        public IEnumerable<WeekDay> GetReceptionEvening(DateTime date) => GetReceptionEvening(date);

        public IEnumerable<WeekDay> GetReceptionEvening() => GetReceptionEvening(null);

        public IEnumerable<WeekDay> GetReceptionMorning(DateTime date) => GetReceptionMorning(date);

        public IEnumerable<WeekDay> GetReceptionMorning() => GetReceptionMorning(null);

        public DateTime GetSelectedWeek()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select ""key""   as ""Key""
                         , ""value"" as ""Value""
                    from settings
                    where ""key"" = 'week_date'";

                var strResult = (from e in c.Query<SettingModel>(sql)
                                 select e.Value).FirstOrDefault();
                Log.Trace($"Retrieving 'week_date' from settings. ({strResult})");

                return (DateTime.TryParse(strResult, out var dateResult))
                    ? dateResult
                    : throw new InvalidCastException($"The string '{strResult}' cannot be casted as a '{typeof(DateTime)}'");
            }
        }

        public void SetSelectedWeek(DateTime date)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update settings
                    set
                        ""value"" = @SetDate
                    where ""key"" = 'week_date'";
                c.Execute(sql, new { SetDate = date });
            }
        }

        #endregion Methods
    }
}