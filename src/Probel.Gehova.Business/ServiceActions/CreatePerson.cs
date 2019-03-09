using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using Probel.Lorm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.ServiceActions
{
    internal class CreatePerson : DbAgent, IServiceAction<PersonModel>
    {
        #region Constructors

        public CreatePerson(IFileLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Properties

        private PersonModel Context { get; set; }

        public object Result { get; private set; }

        #endregion Properties

        #region Methods

        private IEnumerable<CategoryModel> GetCategories(IDbConnection c)
        {
            var list = GetCategoriesOfPerson(c);
            if (list == null) { list = new List<CategoryModel>(); }

            Log.Trace(list?.Dump());
            return list;
        }

        private IEnumerable<CategoryModel> GetCategoriesOfPerson(IDbConnection c)
        {
            var sql = @"
                select id      as Id
                     , ""key"" as Key
                     , display as Display
                 from category
                 where id  in @ids";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameters("ids", Context.Categories.Select(i => i.Id));
                var result = cmd.ExecuteReader().AsCategoryModel();
                Log.Trace(result?.Dump());
                return result;
            }
        }

        private GroupDisplayModel GetPickupRound(IDbConnection c)
        {
            if (Context?.PickupRound?.Id == null) { return null; }
            else
            {
                var sql = @"
                select id   as id
                     , name as name
                from pickup_round
                where id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", Context.PickupRound.Id);
                    var result = cmd.ExecuteReader()
                                    .AsPickupRoundDisplayModel()
                                    .FirstOrDefault();
                    Log.Trace(result?.Dump());
                    return result;
                }
            }
        }

        private IEnumerable<ReceptionModel> GetReceptionOfPerson(IDbConnection c)
        {
            var sql = @"
                select r.id   as Id
                     , r.name as Name
                from reception r
                where id in @ids";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameters("ids", Context.Receptions.Select(e => e.Id));
                var result = cmd.ExecuteReader().AsReceptionModel();
                Log.Trace(result?.Dump());
                return result;
            }
        }

        private IEnumerable<ReceptionModel> GetReceptions(IDbConnection c)
        {
            var list = GetReceptionOfPerson(c);
            if (list == null) { list = new List<ReceptionModel>(); }

            Log.Trace(list?.Dump());
            return list;
        }

        private GroupDisplayModel GetTeam(IDbConnection c)
        {
            if (Context?.Team?.Id == null) { return null; }
            else
            {
                var sql = @"
                select id   as id
                     , name as name
                from team
                where id = @id";

                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", Context.Team.Id);
                    var result = cmd.ExecuteReader()
                                    .AsTeamDisplayModel()
                                    .FirstOrDefault();
                    Log.Trace(result?.Dump());
                    return result;
                }
            }
        }

        private void InsertCategories(IDbConnection c, IEnumerable<long> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                Log.Trace($"Inserting cagegory '{categoryId}' to person '{Context.Id}'");
                var sql = @"insert into person_category(person_id, category_id) values(@person_id, @category_id)";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("person_id", Context.Id);
                    cmd.AddParameter("category_id", categoryId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertPerson(IDbConnection c, long pickupRoundId, long teamId)
        {
            var pri = pickupRoundId == 0 ? DBNull.Value : (object)pickupRoundId;
            var tid = (teamId == 0) ? DBNull.Value : (object)teamId;

            var sql = @"
                insert into person (
                    first_name,
                    last_name,
                    pickup_round_id,
                    team_id
                ) values (
                    @first_name,
                    @last_name,
                    @pickup_round_id,
                    @team_id
                )";
            using (var cmd = GetCommand(sql, c))
            {
                cmd.AddParameter("first_name", Context.FirstName);
                cmd.AddParameter("last_name", Context.LastName);
                cmd.AddParameter("pickup_round_id", pri, DbType.Int64);
                cmd.AddParameter("team_id", tid, DbType.Int64);
                cmd.ExecuteNonQuery();
            }

            Context.Id = GetLastId(c);
        }

        private void InsertReceptions(IDbConnection c, IEnumerable<long> receptionIds)
        {
            foreach (var receptionId in receptionIds)
            {
                Log.Trace($"Inserting reception '{receptionId}' to person '{Context.Id}'");
                var sql = @"insert into reception_person(person_id, reception_id) values(@person_id, @reception_id)";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("person_id", Context.Id);
                    cmd.AddParameter("reception_id", receptionId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IServiceAction<PersonModel> Execute()
        {
            if (Context == null) { throw new NotSupportedException($"The context is not configured. Please set the property Context."); }
            else
            {
                InTransaction(c =>
                {
                    var categories = GetCategories(c);
                    var receptions = GetReceptions(c);
                    var pickupRound = GetPickupRound(c);
                    var team = GetTeam(c);

                    InsertPerson(c, pickupRound?.Id ?? 0, team?.Id ?? 0);
                    if (categories != null && categories.Count() > 0)
                    {
                        InsertCategories(c, categories.Select(cat => cat.Id));
                    }
                    if (receptions != null && receptions.Count() > 0)
                    {
                        InsertReceptions(c, receptions.Select(rec => rec.Id));
                    }
                    else
                    {
                        var t = string.Empty;
                        categories.ToList().ForEach(i => t += i + ", ");
                        if (t.Length > 2) { t.Remove(t.Length - 2, 2); }
                        Log.Warn($"Categories '{t}' do not exist in the database.");
                    }
                });
            }
            return this;
        }

        public IServiceAction<PersonModel> WithContext(PersonModel context)
        {
            Context = context;
            return this;
        }

        #endregion Methods
    }
}