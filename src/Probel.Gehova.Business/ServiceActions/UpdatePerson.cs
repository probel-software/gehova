using Probel.Gehova.Business.Db;
using Probel.Gehova.Business.Models;
using Probel.Lorm;

namespace Probel.Gehova.Business.ServiceActions
{
    public class UpdatePerson : DbAgent, IServiceAction<PersonFullDisplayModel>
    {
        #region Fields

        private PersonFullDisplayModel person;

        #endregion Fields

        #region Constructors

        public UpdatePerson(IDbLocator dbLocator) : base(dbLocator)
        {
        }

        #endregion Constructors

        #region Properties

        public object Result { get; private set; }

        #endregion Properties

        #region Methods

        public IServiceAction<PersonFullDisplayModel> Execute()
        {
            InTransaction(c =>
            {
                var sql = @"
                update person
                set
                    first_name = @first_name,
                    last_name  = @last_name
                where id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("first_name", person.FirstName);
                    cmd.AddParameter("last_name", person.LastName);
                    cmd.AddParameter("id", person.Id);
                    cmd.ExecuteNonQuery();
                }

                sql = @"delete from person_category where person_id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", person.Id);
                    cmd.ExecuteNonQuery();
                }

                sql = @"insert into person_category(person_id, category_id) values (@person_id, @category_id)";
                foreach (var categoryId in person.CategoryIds)
                {
                    using (var cmd = GetCommand(sql, c))
                    {
                        cmd.AddParameter("person_id", person.Id);
                        cmd.AddParameter("category_id", categoryId);
                        cmd.ExecuteNonQuery();
                    }
                }

                sql = @"delete from reception_person where person_id = @id";
                using (var cmd = GetCommand(sql, c))
                {
                    cmd.AddParameter("id", person.Id);
                    cmd.ExecuteNonQuery();
                }
                sql = @"insert into reception_person(person_id, reception_id) values (@person_id, @reception_id)";
                foreach (var receptionId in person.ReceptionIds)
                {
                    using (var cmd = GetCommand(sql, c))
                    {
                        cmd.AddParameter("person_id", person.Id);
                        cmd.AddParameter("reception_id", receptionId);
                        cmd.ExecuteNonQuery();
                    }
                }
            });
            return this;
        }

        public IServiceAction<PersonFullDisplayModel> WithContext(PersonFullDisplayModel context)
        {
            person = context;
            return this;
        }

        #endregion Methods
    }
}