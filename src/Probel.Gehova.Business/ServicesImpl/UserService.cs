using Dapper;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class UserService : DbAgent, IUserService
    {
        #region Methods

        public void Create(AbsenceModel absence) => InTransaction(c =>
        {
            if (absence.Person == null)
            {
                throw new NotSupportedException($"Impossible to insert absence without person in it.");
            }
            else if (absence.From > absence.To)
            {
                throw new NotSupportedException("The start date of after the end date of the absence.");
            }
            else
            {
                var sql = @"
                    insert into absence (
                        date_start,
                        date_end,
                        person_id
                    ) values (
                        @date_start,
                        @date_end,
                        @person_id
                    )";
                c.Execute(sql, new
                {
                    date_start = absence.From,
                    date_end = absence.To,
                    person_id = absence.Person.Id
                });
                absence.Id = GetLastId(c);
            }
        });

        public IEnumerable<AbsenceDisplayModel> GetAbsences()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select a.id         as Id
                         , p.Id         as PersonId
                         , p.first_name as FirstName
                         , p.last_name  as LastName
                         , a.date_start as ""From""
                         , a.date_end as ""To""
                    from absence a
                    inner
                    join person p on a.person_id = p.id";
                var absences = c.Query<AbsenceDisplayModel>(sql);
                return absences;
            }
        }

        public PersonDisplayModel GetPerson(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id         as id
                         , first_name as FirstName
                         , last_name  as LastName
                    from person
                    where id = @id";
                var person = c.Query<PersonDisplayModel>(sql, new { id });
                return person.FirstOrDefault();
            }
        }

        public PickupRoundModel GetPickupRound(long id)
        {
            var result = (from p in GetPickupRounds()
                          where p.Id == id
                          select p).FirstOrDefault();
            return result;
        }

        public IEnumerable<PickupRoundModel> GetPickupRounds()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select r.id         as Id
                         , r.name      	as Name
                         , p.id         as PersonId
                         , p.first_name as FirstName
                         , p.last_name  as LastName
				         , (
				         	select group_concat(c.""display"", ', ')
				         	from person_category pc
				         	inner join category c on pc.category_id = c.id
				         	where pc.person_id = p.id
				         ) as Category
				         , (
				         	select group_concat(c.""key"", ', ')
				         	from person_category pc
				         	inner join category c on pc.category_id = c.id
				         	where pc.person_id = p.id
				         ) as CategoryKey
                    from pickup_round r
                    left join person p on p.pickup_round_id = r.id";

                var lookup = new Dictionary<long, PickupRoundModel>();
                var result = c.Query<PickupRoundModel, dynamic, PickupRoundModel>(sql, (round, person) =>
                {
                    if (!lookup.TryGetValue(round.Id, out var currentRound))
                    {
                        currentRound = round;
                        currentRound.People = new List<PersonDisplayModel>();
                        lookup.Add(currentRound.Id, currentRound);
                    }
                    if (person != null)
                    {
                        currentRound.People.Add(new PersonDisplayModel
                        {
                            Id = person.PersonId,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            Category = person.Category,
                            CategoryKey = person.CategoryKey
                        });
                    }
                    return currentRound;
                }, splitOn: "Id, PersonId")
                .Distinct()
                .ToList();
                return result;
            }
        }

        public TeamModel GetTeam(long teamId)
        {
            var team = (from t in GetTeams()
                        where t.Id == teamId
                        select t).FirstOrDefault();
            return team;
        }

        public IEnumerable<TeamModel> GetTeams()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select t.id         as Id
                         , t.name      	as Name
                         , p.id         as PersonId
                         , p.first_name as FirstName
                         , p.last_name  as LastName
				         , (
				         	select group_concat(c.""display"", ', ')
				         	from person_category pc
				         	inner join category c on pc.category_id = c.id
				         	where pc.person_id = p.id
				         ) as Category
				         , (
				         	select group_concat(c.""key"", ', ')
				         	from person_category pc
				         	inner join category c on pc.category_id = c.id
				         	where pc.person_id = p.id
				         ) as CategoryKey
                    from team t
                    left join person p on p.team_id = t.id";

                var lookup = new Dictionary<long, TeamModel>();
                var result = c.Query<TeamModel, dynamic, TeamModel>(sql, (team, person) =>
                {
                    if (!lookup.TryGetValue(team.Id, out var currentTeam))
                    {
                        currentTeam = team;
                        currentTeam.People = new List<PersonDisplayModel>();
                        lookup.Add(currentTeam.Id, currentTeam);
                    }
                    if (person != null)
                    {
                        currentTeam.People.Add(new PersonDisplayModel
                        {
                            Id = person.PersonId,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            Category = person.Category,
                            CategoryKey = person.CategoryKey
                        });
                    }
                    return currentTeam;
                }, splitOn: "Id, PersonId")
                .Distinct()
                .ToList();
                return result;
            }
        }

        public void Remove(AbsenceModel absence)
        {
            using (var c = NewConnection())
            {
                Log.Debug($"Removing absence '{absence.Id}'");
                var sql = @"delete from absence where id = @id";
                c.Execute(sql, new { absence.Id });
            }
        }

        public void Update(PickupRoundModel round) => InTransaction(c =>
        {
            var sql = @"
                update person
                set
                    pickup_round_id = null
                where pickup_round_id = @RoundId";
            c.Execute(sql, new { RoundId = round.Id });

            sql = @"
                update person 
                set
                    pickup_round_id = @RoundId
                where id in @Ids";
            c.Execute(sql, new { Ids = round.People.Select(p => p.Id), RoundId = round.Id });
        });

        public void Update(TeamModel team) => InTransaction(c =>
        {
            var sql = @"
                update person
                set
	                team_id = null
                where team_id = @TeamId";
            c.Execute(sql, new { TeamId = team.Id });

            sql = @"
                update person
                set
	                team_id = @TeamId
                where id in @Ids";
            c.Execute(sql, new { Ids = team.People.Select(p => p.Id), TeamId = team.Id });
        });

        public void Update(AbsenceModel absence)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update absence
                    set
                        date_start = @date_start,
                        date_end   = @date_end
                    where id = @id";
                c.Execute(sql, new
                {
                    date_start = absence.From,
                    date_end = absence.To,
                    id = absence.Id
                });
            }
        }

        #endregion Methods
    }
}