using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Gehova.Business.ServiceActions;
using Probel.Gehova.Business.Services;
using Probel.Lorm;
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

        public void CreateTeam(GroupDisplayModel team) => InTransaction(c =>
        {
            var sql = @"insert into team (name) values (@Name);";
            using (var cmd = c.CreateCommand(sql))
            {
                cmd.AddParameter("Name", team.Name);
                cmd.ExecuteNonQuery();
            }
            team.Id = GetLastId(c);
        });

        public void Create(CategoryModel category) => InTransaction(c =>
        {
            var sql = @"insert into category (display, key) values (@Display, @Key);";
            using (var cmd = c.CreateCommand(sql))
            {
                cmd.AddParameter("Display", category.Display);
                cmd.AddParameter("Key", category.Key);
                cmd.ExecuteNonQuery();
            }
            category.Id = GetLastId(c);
        });

        public void CreatePickupRound(GroupDisplayModel pickup) => InTransaction(c =>
        {
            var sql = @"insert into pickup_round (name) values (@Name);";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("Name", pickup.Name);
                cmd.ExecuteNonQuery();
            }

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
            if (person?.Receptions == null || person?.Receptions?.Count() == 0)
            {
                Log.Debug("No reception for the person.");
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
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("from", absence.From.ToSQLiteDateString());
                    cmd.AddParameter("to", absence.To.ToSQLiteDateString());
                    cmd.AddParameter("person_id", absence.PersonId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<AbsenceDisplayModel> GetAbsencesOf(PersonFullDisplayModel person)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id         as Id
                         , date_start as ""From""
                         , date_end   as ""To""
                         , person_id  as PersonId
                    from absence
                    where person_id = @pid
                    order by date_start desc";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("pid", person.Id);
                    var result = cmd.ExecuteReader()
                                    .AsAbsenceDisplayModel();
                    return result;
                }
            }
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id       as Id
                         , key      as Key
                         , display  as Display
                    from category";
                using (var cmd = GetCommand(sql, c))
                {
                    var result = cmd.ExecuteReader()
                                   .AsCategoryModel();
                    return result;
                }
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

                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", id);
                    var result = cmd.ExecuteReader()
                                    .AsCategoryModel()
                                    .FirstOrDefault();
                    return result;
                }
            }
        }

        public IEnumerable<PersonFullDisplayModel> GetPeople()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select person_id                   as Id
                         , first_name           as FirstName
                         , last_name            as LastName
                         , category             as Category
                         , category_key         as CategoryKey
                         , team                 as Team
                         , team_id              as TeamId
                         , pickup_round_id      as PickupRoundId
                    from everyone_v";
                using (var cmd = GetCommand(sql, c))
                {
                    var result = cmd.ExecuteReader()
                                    .AsPersonFullDisplayModel();
                    return result;
                }
            }
        }

        public PersonDisplayModel GetPerson(long id)
        {
            var r = new GetPerson(DbLocator)
                        .WithContext(new PersonDisplayModel { Id = id })
                        .Execute()
                        .Result;
            return (PersonDisplayModel)r;
        }

        public GroupDisplayModel GetPickupRound(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as Id
                         , name as Name
                    from pickup_round
                   where id = @id";
                var result = c.Query<GroupDisplayModel>(sql, new { id })
                              .FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<GroupDisplayModel> GetPickupRounds()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as Id
                         , name as Name
                    from pickup_round
                    order by name";

                using (var cmd = GetCommand(sql, c))
                {
                    var result = cmd.ExecuteReader().AsPickupRoundDisplayModel();
                    return result;
                }
            }
        }

        public GroupDisplayModel GetTeam(long id)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as Id
                         , name as Name
                    from team
                   where id = @id";

                var r = c.Query<GroupDisplayModel>(sql, new { id })
                         .FirstOrDefault();
                return r;
            }
        }

        public IEnumerable<GroupDisplayModel> GetTeams()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id    as Id
                         , name  as Name
                    from team
                    order by name";
                using (var cmd = GetCommand(sql, c))
                {
                    var result = cmd.ExecuteReader().AsTeamDisplayModel();
                    return result;
                }
            }
        }

        public void Remove(GroupDisplayModel team) => InTransaction(c =>
        {
            var sql = @"
                update person
                set
                    team_id = null
                where team_id = @tid";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("tid", team.Id);
                cmd.ExecuteNonQuery();
            }

            sql = @"
                    delete from team
                    where id = @id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("id", team.Id);
                cmd.ExecuteNonQuery();
            }
        });

        public void Remove(CategoryModel category)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from category
                    where id = @Id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("Id", category.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemovePickup(GroupDisplayModel pickup) => InTransaction(c =>
        {
            var sql = @"
                update person
                set
                    pickup_round_id = null
                where pickup_round_id = @prid";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("prid", pickup.Id);
                cmd.ExecuteNonQuery();
            }

            sql = @"
               delete from pickup_round
                where id = @id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("id", pickup.Id);
                cmd.ExecuteNonQuery();
            }
        });

        public void Remove(PersonDisplayModel person) => InTransaction(c =>
        {
            var sql = @"
                    delete from person_category
                    where person_id = @Id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("Id", person.Id);
                cmd.ExecuteNonQuery();
            }

            sql = @"
                delete from person
                where id = @id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("id", person.Id);
                cmd.ExecuteNonQuery();
            }
        });

        public void Remove(AbsenceDisplayModel absence)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    delete from absence
                    where id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", absence.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTeam(GroupDisplayModel team)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update team
                       set name = @Name
                     where id = @Id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("Id", team.Id);
                    cmd.AddParameter("Name", team.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTeam(GroupModel team) => InTransaction(c =>
        {
            var sql = @"
                    update team
                       set name = @Name
                     where id = @Id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("Name", team.Id);
                cmd.AddParameter("Id", team.Name);
                cmd.ExecuteNonQuery();
            }

            sql = @"
            update person
            set
                team_id = null
            where team_id = @tid";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("tid", team.Id);
                cmd.ExecuteNonQuery();
            }

            sql = @"
            update person
            set
                team_id = @team_id
            where id in @person_ids";

            using (var cmd = GetCommand(sql, c))
            {
                var prid = team.People.Select(e => e.Id).ToList();
                cmd.AddParameter("team_id", team.Id);
                cmd.AddParameters("person_ids", prid);
                cmd.ExecuteNonQuery();
            }
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

                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("Id", category.Id);
                    cmd.AddParameter("Key", category.Display);
                    cmd.AddParameter("Display", category.Key);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePickupRound(GroupModel pickupRound) => InTransaction(c =>
        {
            var sql = @"
                    update pickup_round
                       set name = @Name
                     where id = @Id";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("Id", pickupRound.Id);
                cmd.AddParameter("Name", pickupRound.Name);
                cmd.ExecuteNonQuery();
            }
            sql = @"
            update person
            set
                pickup_round_id = null
            where pickup_round_id = @pid";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("pid", pickupRound.Id);
                cmd.ExecuteNonQuery();
            }

            sql = @"
            update person
            set
                pickup_round_id = @pickup_id
            where id in @person_ids";
            var prid = pickupRound.People.Select(e => e.Id).ToList();
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("pickup_id", pickupRound.Id);
                cmd.AddParameters("person_ids", prid);
                cmd.ExecuteNonQuery();
            }
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
                    pickup_round_id = @pickup_round_id,
                    team_id = @team_id
                where id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("first_name", person.FirstName);
                    cmd.AddParameter("last_name", person.LastName);
                    cmd.AddParameter("pickup_round_id", person.PickupRound.Id);
                    cmd.AddParameter("team_id", person.Team.Id);
                    cmd.AddParameter("id", person.Id);
                    cmd.ExecuteNonQuery();
                }

                sql = @"delete from person_category where person_id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", person.Id);
                    cmd.ExecuteNonQuery();
                }

                foreach (var category in person.Categories)
                {
                    using (var cmd = GetCommand(sql, c))
                    {
                        sql = @"
                        insert into person_category (
                            person_id,
                            category_id
                        ) values (
                            @person_id,
                            @category_id
                        )";
                        cmd.AddParameter("person_id", person.Id);
                        cmd.AddParameter("category_id", category.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        });

        public void Update(PersonFullDisplayModel person)
        {
            new UpdatePerson(DbLocator)
                .WithContext(person)
                .Execute();
        }

        public void UpdatePickup(GroupDisplayModel pickup)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    update pickup_round
                       set name = @Name
                     where id = @Id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("Id", pickup.Id);
                    cmd.AddParameter("Name", pickup.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<ReceptionModel> GetReceptions()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id   as Id
                         , name as Name
                    from reception";
                var result = c.Query<ReceptionModel>(sql);
                return result;
            }
        }

        public IEnumerable<long> GetReceptionsOf(long personId)
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select r.id   as Id
                    from reception r
                    inner join reception_person rp on rp.reception_id = r.id
                    where rp.person_id = @personId";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("personId", personId);
                    var result = cmd.ExecuteReader().AsLong();
                    return result;
                }
            }
        }

        #endregion Methods
    }
}