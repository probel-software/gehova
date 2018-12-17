using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IProvisioningService
    {
        #region Methods

        void Create(TeamModel team);

        void Create(PersonCategoryModel category);

        void Create(PickupRoundModel pickup);

        void Create(PersonModel person);

        IEnumerable<PersonCategoryModel> GetCategories();

        PersonCategoryModel GetCategory(long id);

        IEnumerable<PersonDisplayModel> GetPeople();

        PickupRoundModel GetPickupRound(long id);

        IEnumerable<PickupRoundModel> GetPickupRounds();

        TeamModel GetTeam(long id);

        IEnumerable<TeamModel> GetTeams();

        void Remove(TeamModel team);

        void Remove(PersonCategoryModel category);

        void Remove(PickupRoundModel pickup);

        void Remove(PersonModel person);

        void Update(PersonModel person);

        void Update(TeamModel team);

        void Update(PersonCategoryModel category);

        void Update(PickupRoundModel pickup);

        #endregion Methods
    }
}