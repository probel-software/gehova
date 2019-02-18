using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IProvisioningService
    {
        #region Methods

        void Create(AbsenceDisplayModel absence);

        void CreatePickupRound(GroupDisplayModel team);

        void Create(CategoryModel category);

        void CreatePickup(GroupDisplayModel pickup);

        void Create(PersonModel person);

        void Create(PersonFullDisplayModel personFullDisplayModel);

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

        void RemovePickup(GroupDisplayModel pickup);

        void Remove(PersonDisplayModel person);

        void UpdateTeam(GroupDisplayModel team);

        void Update(CategoryModel category);

        void UpdatePickupRound(GroupModel pickup);

        void UpdatePickup(GroupDisplayModel pickup);

        void Update(PersonFullDisplayModel person);

        void UpdateTeam(GroupModel team);

        #endregion Methods
    }
}