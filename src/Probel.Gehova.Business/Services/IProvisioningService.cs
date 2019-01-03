using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IProvisioningService
    {
        #region Methods

        void Create(TeamDisplayModel team);

        void Create(CategoryModel category);

        void Create(PickupRoundDisplayModel pickup);

        void Create(PersonModel person);

        void Create(PersonFullDisplayModel personFullDisplayModel);

        IEnumerable<CategoryModel> GetCategories();

        CategoryModel GetCategory(long id);

        IEnumerable<PersonFullDisplayModel> GetPeople();

        PersonDisplayModel GetPerson(long id);

        PickupRoundDisplayModel GetPickupRound(long id);

        IEnumerable<PickupRoundDisplayModel> GetPickupRounds();

        TeamDisplayModel GetTeam(long id);

        IEnumerable<TeamDisplayModel> GetTeams();

        void Remove(TeamDisplayModel team);

        void Remove(CategoryModel category);

        void Remove(PickupRoundDisplayModel pickup);

        void Remove(PersonModel person);

        void Update(PersonModel person);

        void Update(TeamDisplayModel team);

        void Update(CategoryModel category);

        void Update(PickupRoundDisplayModel pickup);

        void Update(PersonFullDisplayModel person);

        #endregion Methods
    }
}