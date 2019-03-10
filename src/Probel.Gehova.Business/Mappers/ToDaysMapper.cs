using Probel.Gehova.Business.I18n;
using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.Mappers
{
    internal class ToDaysMapper
    {
        #region Fields

        private List<DayModel> Days;

        public ToDaysMapper()
        {
            ResetDays();
        }

        private void ResetDays()
        {
            if (Days == null) { Days = new List<DayModel>(); }
            else { Days.Clear(); }

            Days.Add(new DayModel { DayName = Strings.Monday, DayId = 1 });
            Days.Add(new DayModel { DayName = Strings.Tuesday, DayId = 2 });
            Days.Add(new DayModel { DayName = Strings.Wednesday, DayId = 3 });
            Days.Add(new DayModel { DayName = Strings.Thursday, DayId = 4 });
            Days.Add(new DayModel { DayName = Strings.Friday, DayId = 5 });
        }
        private IDataReader _reader;

        #endregion Fields

        #region Methods


        private (PersonDisplayModel person, GroupModel team, DayModel day) Get()
        {
            var person = new PersonDisplayModel
            {
                Category = _reader["category"] as string ?? string.Empty,
                CategoryKey = _reader["category_key"] as string ?? string.Empty,
                FirstName = _reader["first_name"] as string ?? string.Empty,
                LastName = _reader["last_name"] as string ?? string.Empty,
                Id = _reader["person_id"] as long? ?? 0
            };
            var team = new GroupModel
            {
                Id = _reader["team_id"] as long? ?? 0,
                Name = _reader["team"] as string ?? string.Empty
            };
            var did = (_reader["day"] as long? ?? 0);
            var day = new DayModel
            {
                DayName = did.AsDay(),
                DayId = did
            };

            return
            (
                person,
                team,
                day
            );
        }

        public IEnumerable<DayModel> Map(IDataReader src)
        {
            _reader = src ?? throw new ArgumentNullException(nameof(src), $"The instance of '{typeof(IDataReader)}' is NULL. Did you forgot to specified a valid DataReader?");            

            while (_reader.Read())
            {
                var (person, team, day) = Get();

                var foundDay = Get(day);
                if (Has(team, foundDay))
                {
                    var foundTeam = Get(team, foundDay);
                    if (!Has(person, foundTeam))
                    {
                        foundTeam.People.Add(person);
                    }
                }
                else
                {
                    team.People.Add(person);
                    foundDay.Teams.Add(team);
                }
            }
            return Days;
        }

        private bool Has(PersonDisplayModel person, GroupModel team)
        {
            var count = (from p in team.People
                         where p.Id == person.Id
                         select p).Count();
            return count > 0;
        }

        private GroupModel Get(GroupModel team, DayModel day)
        {
            var result = (from t in day.Teams
                          where t.Id == team.Id
                          select t).Single();
            return result;
        }

        private bool Has(GroupModel team, DayModel day)
        {
            var count = (from t in day.Teams
                         where t.Id == team.Id
                         select t).Count();
            return count > 0;
        }

        private DayModel Get(DayModel day)
        {
            var result = (from d in Days
                          where d.DayId == day.DayId
                          select d).Single();
            return result;
        }

        #endregion Methods

    }
}