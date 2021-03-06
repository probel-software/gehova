﻿using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Lorm;
using System.Linq;

namespace Probel.Gehova.Business.ServiceActions
{
    public class GetPerson : DbAgent, IServiceAction<PersonDisplayModel>
    {
        #region Fields

        private long _id;

        #endregion Fields

        #region Constructors

        public GetPerson(IFileLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Properties

        public object Result
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public IServiceAction<PersonDisplayModel> Execute()
        {
            using (var c = NewConnection())
            {
                var sql = @"
                    select person_id    as Id
                         , first_name   as FirstName
                         , last_name    as LastName
                         , category     as Category
                         , category_key as CategoryKey
                    from everyone_v
                    where id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", _id);
                    var result = cmd.ExecuteReader()
                                    .AsPersonDisplayModel()
                                    .FirstOrDefault();
                    Result = result;
                }
            }
            return this;
        }

        public IServiceAction<PersonDisplayModel> WithContext(PersonDisplayModel context)
        {
            _id = context.Id;
            return this;
        }

        #endregion Methods
    }
}