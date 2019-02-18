using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.Mappers
{
    internal class ToReceptionGroupMapper
    {
        #region Fields

        private readonly List<ReceptionGroupModel> ReceptionGroups = new List<ReceptionGroupModel>();
        private IDataReader _reader;

        #endregion Fields

        #region Methods

        private ReceptionModel Get(long id, ReceptionGroupModel frg)
        {
            var r = (from x in frg.Receptions
                     where x.Id == id
                     select x).Single();
            return r;
        }

        private ReceptionGroupModel Get(ReceptionGroupModel rg)
        {
            var res = (from r in ReceptionGroups
                       where r.Id == rg.Id
                       select r).Single();
            return res;
        }

        private DayModel Get(long dayId, ReceptionModel foundReception)
        {
            var r = (from x in foundReception.Days
                     where x.DayId == dayId
                     select x).Single();
            return r;
        }

        private (PersonDisplayModel person, GroupModel team, DayModel day, ReceptionModel reception, ReceptionGroupModel receptionGroup) Get()
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
            var reception = new ReceptionModel
            {
                Id = _reader["reception_id"] as long? ?? 0,
                ReceptionName = _reader["reception"] as string ?? string.Empty,
            };
            var receptionGroup = new ReceptionGroupModel
            {
                ReceptionGroupName = _reader["reception_group"] as string ?? string.Empty,
                Id = _reader["reception_group_id"] as long? ?? 0,
            };

            return
            (
                person,
                team,
                day,
                reception,
                receptionGroup
            );
        }

        private GroupModel Get(long id, DayModel foundDay)
        {
            var res = (from d in foundDay.Teams
                       where d.Id == id
                       select d).Single();
            return res;
        }

        private bool Has(DayModel day, IList<DayModel> days)
        {
            var r = (from d in days
                     where d.DayId == day.DayId
                     select d);
            return r.Count() > 0;
        }

        private bool Has(long id, ReceptionGroupModel frg)
        {
            var r = (from x in frg.Receptions
                     where x.Id == id
                     select x).Count() > 0;
            return r;
        }

        private bool Has(ReceptionGroupModel rg)
        {
            var res = (from r in ReceptionGroups
                       where r.Id == rg.Id
                       select r).Count() > 0;
            return res;
        }

        private bool Has(long id, IList<GroupModel> teams)
        {
            var r = (from t in teams
                     where t.Id == id
                     select t).Count() > 0;
            return r;
        }

        private bool Has(long id, IList<PersonDisplayModel> people)
        {
            var res = (from p in people
                       where p.Id == id
                       select p).Count() > 0;
            return res;
        }

        private void InsertInDay(PersonDisplayModel person, GroupModel team, DayModel day, ReceptionModel foundReception)
        {
            if (Has(day, foundReception.Days))
            {
                var foundDay = Get(day.DayId, foundReception);
                if (Has(team.Id, foundDay.Teams))
                {
                    var foundTeam = Get(team.Id, foundDay);
                    if (!Has(person.Id, foundTeam.People))
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
            else { throw new NotSupportedException($"All the days should be integrated into a reception. Code review is needed!"); }
        }

        public IEnumerable<ReceptionGroupModel> Map(IDataReader src)
        {
            _reader = src ?? throw new ArgumentNullException(nameof(src), $"The instance of '{typeof(IDataReader)}' is NULL. Did you forgot to specified a valid DataReader?");
            ReceptionGroups.Clear();

            while (_reader.Read())
            {
                var (person, team, day, reception, receptionGroup) = Get();

                if (Has(receptionGroup))
                {
                    var foundGroup = Get(receptionGroup);
                    if (Has(reception.Id, foundGroup))
                    {
                        var foundReception = Get(reception.Id, foundGroup);
                        InsertInDay(person, team, day, foundReception);
                    }
                    else
                    {
                        team.People.Add(person);
                        day.Teams.Add(team);
                        InsertInDay(person, team, day, reception);
                        foundGroup.Receptions.Add(reception);
                    }
                }
                else
                {
                    team.People.Add(person);
                    day.Teams.Add(team);
                    receptionGroup.Receptions.Add(reception);
                    ReceptionGroups.Add(receptionGroup);
                }
            }
            return ReceptionGroups;
        }

        #endregion Methods
    }
}