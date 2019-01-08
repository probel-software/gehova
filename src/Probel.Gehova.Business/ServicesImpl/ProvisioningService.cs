using Dapper;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.ServiceActions;
using Probel.Gehova.Business.Services;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Business.ServicesImpl
{
    public class ProvisioningService : DbAgent, IProvisioningService
    {
        #region Constructors

        public ProvisioningService(IDbLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Methods

        public void Create(TeamDisplayModel team) => InTransaction(c =>
        {
            var sql = @"insert into team (name) values (@Name);";
            c.Execute(sql, new { team.Name });
            team.Id = GetLastId(c);
        });

        public void Create(CategoryModel category) => InTransaction(c =>
        {
            var sql = @"insert into category (display, key) values (@Display, @Key);";
            c.Execute(sql, new { category.Display, category.Key });
            category.Id = GetLastId(c);
        });

        public void Create(PickupRoundDisplayModel pickup) => InTransaction(c =>
        {
            var sql = @"insert into pickup_round (name) values (@Name);";
            c.Execute(sql, new { pickup.Name });

            pickup.Id = GetLastId(c);
        });

        public void Create(PersonModel person)
        {
            if (person == null)
            {
                Log.Info("No person to add. Specified person id NULL");
                return;
            }
            if (person?.Categories == null || person?.Categories?.Count() == 0)
            {
                Log.Debug("No category for the person.");
                return;
            }
            new CreatePerson(DbLocator)
                .WithContext(person)
                .Execute();
        }

        public void Create(PersonFullDisplayModel person)
        {
            var categories = new List<CategoryModel>();
            foreach (var catId in person.CategoryIds) { categories.Add(new CategoryModel { Id = catId }); }
            var p = new PersonModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsLunchTime = person.IsLunchTime,
                IsReceptionEvening = person.IsReceptionEvening,
                IsReceptionMorning = person.IsReceptionMorning,
                Categories = categories,
            };
            Create(p);
        }

        public void Create(AbsenceDisplayModel absence)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    insert into absence (
                        date_start,
                        date_end,
                        person_id)
                    values (
                        @from,
                        @to,
                        @person_id
                    )";
                c.Execute(sql, new { from = absence.From, to = absence.To, person_id = absence.PersonId });
            }
        }

        public IEnumerable<AbsenceDisplayModel> GetAbsencesOf(PersonFullDisplayModel person)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id         as Id
                         , date_end   as ""From""
                         , date_start as ""To""
                         , person_id  as PersonId
                    from absence
                    where person_id = @pid
                    order by date_start desc";
                var result = c.Query<AbsenceDisplayModel>(sql, new { pid = person.Id });

                return result;
            }
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id       as ID
                         , key      as Key
                         , display  as Display
                    from category";

                var result = c.Query<CategoryModel>(sql);
                return result;
            }
        }

        public CategoryModel GetCategory(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id      as id
                         , key     as Key
                         , display as Display
                    from category
                   where id = @id";

                var result = c.Query<CategoryModel>(sql, new { id });
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<PersonFullDisplayModel> GetPeople()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id                   as Id
                         , first_name           as FirstName
                         , last_name            as LastName
                         , category             as Category
                         , category_key         as CategoryKey
                         , team                 as Team
                         , team_id              as TeamId
                         , pickup_round_id      as PickupRoundId
                         , is_reception_morning as IsReceptionMorning
                         , is_reception_evening as IsReceptionEvening
                         , is_lunchtime         as IsLunchTime
                    from everyone_v";

                var result = c.Query<PersonFullDisplayModel>(sql);
                return result;
            }
        }

        public PersonDisplayModel GetPerson(long id)
        {
            var r = new GetPerson(DbLocator)
                        .WithContext(new PersonDisplayModel { Id = id })
                        .Execute();
            return (PersonDisplayModel)r;
        }

        public PickupRoundDisplayModel GetPickupRound(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id      as id
                         , name    as Name
                    from pickup_round
                   where id = @id";

                var result = c.Query<PickupRoundDisplayModel>(sql, new { id });
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<PickupRoundDisplayModel> GetPickupRounds()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as ID
                         , name as Name
                    from pickup_round";

                var result = c.Query<PickupRoundDisplayModel>(sql);
                return result;
            }
        }

        public TeamDisplayModel GetTeam(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as id
                         , name as Name
                    from team
                   where id = @id";

                var result = c.Query<TeamDisplayModel>(sql, new { id });
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<TeamDisplayModel> GetTeams()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id    as ID
                         , name  as Name
                    from team";

                var result = c.Query<TeamDisplayModel>(sql);
                return result;
            }
        }

        public void Remove(TeamDisplayModel team)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from team
                    where id = @Id";
                c.Execute(sql, new { team.Id });
            }
        }

        public void Remove(CategoryModel category)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from category
                    where id = @Id";
                c.Execute(sql, new { category.Id });
            }
        }

        public void Remove(PickupRoundDisplayModel pickup)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from pickup_round
                    where id = @Id";
                c.Execute(sql, new { pickup.Id });
            }
        }

        public void Remove(PersonDisplayModel person) => InTransaction(c =>
        {
            var sql = @"
                    delete from person_category
                    where person_id = @Id";
            c.Execute(sql, new { person.Id });

            sql = @"
                delete from person
                where id = @id";
            c.Execute(sql, new { person.Id });
        });

        public void Remove(AbsenceDisplayModel absence)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from absence
                    where id = @id";
                c.Execute(sql, new { id = absence.Id });
            }
        }

        public void Update(TeamDisplayModel team)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update team
                       set name = @Name
                     where id = @Id";
                c.Execute(sql, new { team.Id, team.Name });
            }
        }

        public void Update(TeamModel team) => InTransaction(c =>
        {
            var sql = @"
                    update team
                       set name = @Name
                     where id = @Id";
            c.Execute(sql, new { team.Id, team.Name });

            sql = @"
            update person
            set
                team_id = null
            where team_id = @tid";

            c.Execute(sql, new { tid = team.Id });

            sql = @"
            update person
            set
                team_id = @team_id
            where id in @person_ids";
            var prid = team.People.Select(e => e.Id).ToList();
            c.Execute(sql, new { team_id = team.Id, person_ids = prid });
        });

        public void Update(CategoryModel category)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update category
                    set
                        display = @Display,
                        key     = @Key
                     where id = @Id";
                c.Execute(sql, new { category.Id, category.Display, category.Key });
            }
        }

        public void Update(PickupRoundModel pickupRound) => InTransaction(c =>
        {
            var sql = @"
                    update pickup_round
                       set name = @Name
                     where id = @Id";
            c.Execute(sql, new { pickupRound.Id, pickupRound.Name });

            sql = @"
            update person
            set
                pickup_round_id = null
            where pickup_round_id = @pid";

            c.Execute(sql, new { pid = pickupRound.Id });

            sql = @"
            update person
            set
                pickup_round_id = @pickup_id
            where id in @person_ids";
            var prid = pickupRound.People.Select(e => e.Id).ToList();
            c.Execute(sql, new { pickup_id = pickupRound.Id, person_ids = prid });
        });

        public void Update(PersonModel person) => InTransaction(c =>
        {
            if (person?.Team?.Id == null) { throw new KeyNotFoundException($"The person with id '{person.Id}' cannot be updated as (s)he has not Team."); }
            else if (person?.PickupRound?.Id == null) { throw new KeyNotFoundException($"The person with id '{person.Id}' cannot be updated as (s)he has not pickup round."); }
            else
            {
                var sql = @"
                update person
                set
                    first_name = @first_name,
                    last_name = @last_name,
                    is_reception_morning = @is_reception_morning,
                    is_reception_evening = @is_reception_evening,
                    is_lunchtime = @is_lunchtime,
                    pickup_round_id = @pickup_round_id,
                    team_id = @team_id
                where id = @id";
                c.Execute(sql, new
                {
                    first_name = person.FirstName,
                    last_name = person.LastName,
                    is_reception_morning = person.IsReceptionMorning,
                    is_reception_evening = person.IsReceptionEvening,
                    is_lunchtime = person.IsLunchTime,
                    pickup_round_id = person.PickupRound.Id,
                    team_id = person.Team.Id,
                    id = person.Id
                });

                sql = @"delete from person_category where person_id = @id";
                c.Execute(sql, new { id = person.Id });

                foreach (var category in person.Categories)
                {
                    sql = @"
                        insert into person_category (
                            person_id,
                            category_id
                        ) values (
                            @person_id,
                            @category_id
                        )";
                    c.Execute(sql, new { person_id = person.Id, category_id = category.Id });
                }
            }
        });

        public void Update(PersonFullDisplayModel person) => InTransaction(c =>
        {
            var sql = @"
                update person
                set
                    first_name = @first_name,
                    last_name  = @last_name,
                    is_reception_morning = @is_reception_morning,
                    is_lunchtime = @is_lunchtime,
                    is_reception_evening = @is_reception_evening
                where id = @id";
            c.Execute(sql, new
            {
                first_name = person.FirstName,
                last_name = person.LastName,
                id = person.Id,
                is_reception_morning = person.IsReceptionMorning,
                is_lunchtime = person.IsLunchTime,
                is_reception_evening = person.IsReceptionEvening
            });

            sql = @"delete from person_category where person_id = @id";
            c.Execute(sql, new { id = person.Id });

            sql = @"insert into person_category(person_id, category_id) values (@person_id, @category_id)";
            foreach (var categoryId in person.CategoryIds)
            {
                c.Execute(sql, new { person_id = person.Id, category_id = categoryId });
            }
        });

        public void Update(PickupRoundDisplayModel pickup)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update pickup_round
                       set name = @Name
                     where id = @Id";
                c.Execute(sql, new { pickup.Id, pickup.Name });
            }
        }

        #endregion Methods
    }
}