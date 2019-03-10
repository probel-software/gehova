using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IProvisioningService
    {
        #region Methods

        void Create(AbsenceDisplayModel absence);

        void Create(CategoryModel category);

        void Create(PersonModel person);

        void Create(PersonFullDisplayModel personFullDisplayModel);

        void CreatePickupRound(GroupDisplayModel pickup);

        void CreateTeam(GroupDisplayModel team);

        IEnumerable<AbsenceDisplayModel> GetAbsencesOf(PersonFullDisplayModel person);

        IEnumerable<CategoryModel> GetCategories();

        CategoryModel GetCategory(long id);

        IEnumerable<PersonFullDisplayModel> GetPeople();

        PersonDisplayModel GetPerson(long id);

        GroupDisplayModel GetPickupRound(long id);

        IEnumerable<GroupDisplayModel> GetPickupRounds();

        IEnumerable<ReceptionModel> GetReceptions();

        IEnumerable<long> GetReceptionsOf(long id);

        GroupDisplayModel GetTeam(long id);

        IEnumerable<GroupDisplayModel> GetTeams();

        void Remove(AbsenceDisplayModel absence);

        void Remove(GroupDisplayModel team);

        void Remove(CategoryModel category);

        void Remove(PersonDisplayModel person);

        void RemovePickup(GroupDisplayModel pickup);

        void Update(CategoryModel category);

        void Update(PersonFullDisplayModel person);

        void UpdatePickup(GroupDisplayModel pickup);

        void UpdatePickupRound(GroupModel pickup);

        void UpdateTeam(GroupDisplayModel team);

        void UpdateTeam(GroupModel team);

        #endregion Methods
    }
}