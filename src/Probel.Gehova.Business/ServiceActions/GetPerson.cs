using Dapper;
using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using System.Linq;

namespace Probel.Gehova.Business.ServiceActions
{
    public class GetPerson : DbAgent, IServiceAction<PersonDisplayModel>
    {
        #region Fields

        private long _id;

        #endregion Fields

        #region Constructors

        public GetPerson(IDbLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Methods

        public object Execute()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select id           as id
                         , first_name   as FirstName
                         , last_name    as LastName
                         , category     as Category
                         , category_key as CategoryKey
                    from everyone_v
                    where id = @id";
                var person = c.Query<PersonDisplayModel>(sql, new { id = _id });
                return person.FirstOrDefault();
            }
        }

        public IServiceAction<PersonDisplayModel> WithContext(PersonDisplayModel context)
        {
            _id = context.Id;
            return this;
        }

        #endregion Methods
    }
}