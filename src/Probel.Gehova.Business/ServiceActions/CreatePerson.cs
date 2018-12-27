using Dapper;
using Probel.Gehova.Business.Helpers;
using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.ServiceActions
{
    internal class CreatePerson : DbAgent, IServiceAction<PersonModel>
    {
        #region Properties

        private PersonModel Context { get; set; }

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
                select id
                     , ""key""
                     , display
                 from category 
                 where id  in @ids";
            var result = c.Query<CategoryModel>(sql, new { ids = Context.Categories.Select(i => i.Id) });

            Log.Trace(result?.Dump());
            return result;
        }

        private PickupRoundDisplayModel GetPickupRound(IDbConnection c)
        {
            if (Context?.PickupRound?.Id == null) { return null; }
            else
            {
                var sql = @"
                select id   as id
                     , name as name
                from pickup_round
                where id = @id";
                var result = c.Query<PickupRoundDisplayModel>(sql, new { id = Context.PickupRound.Id })
                              .FirstOrDefault();
                Log.Trace(result?.Dump());
                return result;
            }
        }

        private TeamDisplayModel GetTeam(IDbConnection c)
        {
            if (Context?.Team?.Id == null) { return null; }
            else
            {
                var sql = @"
                select id   as id
                     , name as name
                from team
                where id = @id";
                var result = c.Query<TeamDisplayModel>(sql, new { id = Context.Team.Id })
                              .FirstOrDefault();
                Log.Trace(result?.Dump());
                return result;
            }
        }

        public void Execute()
        {
            if (Context == null) { throw new NotSupportedException($"The context is not configured. Please set the property Context."); }
            else
            {
                InTransaction(c =>
                {
                    var categories = GetCategories(c);
                    var pickupRound = GetPickupRound(c);
                    var team = GetTeam(c);

                    InsertPerson(c, pickupRound.Id, team.Id);
                    if (categories != null && categories.Count() > 0)
                    {
                        InsertCategories(c, categories.Select(cat => cat.Id));
                    }
                    else
                    {
                        var t = string.Empty;
                        categories.ToList().ForEach(i => t += i + ", ");
                        t.Remove(t.Length - 2, 2);
                        Log.Warn($"Categories '{t}' do not exist in the database.");
                    }
                });
            }
        }

        private void InsertCategories(IDbConnection c, IEnumerable<long> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                Log.Trace($"Inserting cagegory '{categoryId}' to person '{Context.Id}'");
                var sql = @"insert into person_category(person_id, category_id) values(@person_id, @category_id)";
                c.Execute(sql, new
                {
                    person_id = Context.Id,
                    category_id = categoryId
                });
            }
        }

        private void InsertPerson(IDbConnection c, long pickupRoundId, long teamId)
        {
            var sql = @"
                insert into person (
                    first_name, 
                    last_name, 
                    is_lunchtime, 
                    is_reception_morning, 
                    is_reception_evening, 
                    pickup_round_id, 
                    team_id
                ) values (
                    @first_name, 
                    @last_name, 
                    @is_lunchtime, 
                    @is_reception_morning, 
                    @is_reception_evening, 
                    @pickup_round_id, 
                    @team_id
                )";
            c.Execute(sql, new
            {
                first_name = Context.FirstName,
                last_name = Context.LastName,
                is_lunchtime = Context.IsLunchTime,
                is_reception_morning = Context.IsReceptionMorning,
                is_reception_evening = Context.IsReceptionEvening,
                pickup_round_id = pickupRoundId,
                team_id = teamId
            });

            Context.Id = GetLastId(c);            
        }

        public IServiceAction<PersonModel> WithContext(PersonModel context)
        {
            Context = context;
            return this;
        }

        #endregion Methods
    }
}