using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Mappers;
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

        public VisualisationService(IFileLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Methods

        private IEnumerable<WeekDay> GetGroups(DateTime? date) => InTransaction(c =>
        {
            if (date.HasValue) { SetSelectedWeek(date.Value); }

            var sql = @"
                select day_name     as Day
                     , team         as Team
                     , pickup_round as PickupRound
                     , first_name   as FirstName
                     , last_name    as LastName
                     , category_key as Categories
                from groups_v";
            var result = c.Query<WeekDay>(sql);
            return result;
        });

        private IEnumerable<WeekDay> GetLunchtime(DateTime? date) => InTransaction(c =>
                {
                    if (date.HasValue) { SetSelectedWeek(date.Value); }

                    var sql = @"
                select day_name   as Day
                     , team       as Team
                     , first_name as FirstName
                     , last_name  as LastName
                     , category_key as Categories
                from lunchtime_v";
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

        public IEnumerable<ReceptionGroupModel> GetReceptionGroups()
        {
            var sql = @"
                select *
                from presence_week_v
                where team_id is not null
                order by rg_order, r_order, category_key";
            using (var c = NewConnection())
            using (var cmd = GetCommand(sql, c))
            {
                var r = cmd.ExecuteReader();
                var result = Map.ToReceptionGroupModel(r);
                return result;
            }
        }

        public IEnumerable<DayPickupRoundModel> GetPickupRounds()
        {
            var sql = @"
                select *
                from presence_week_v
                where pickup_round_id is not null
                order by rg_order, r_order, category_key";
            using (var c = NewConnection())
            using (var cmd = GetCommand(sql, c))
            {
                var r = cmd.ExecuteReader();
                var result = Map.ToDayPickupRounds(r);
                return result;
            }
        }

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

        public IEnumerable<DayModel> GetAllTeams()
        {
            var sql = @"
                select *
                from presence_week_v
                where team_id is not null
                order by rg_order, r_order";
            using (var c = NewConnection())
            using (var cmd = GetCommand(sql, c))
            {
                var r = cmd.ExecuteReader();
                var result = Map.ToDayModel(r);
                return result;
            }
        }

        #endregion Methods
    }
}