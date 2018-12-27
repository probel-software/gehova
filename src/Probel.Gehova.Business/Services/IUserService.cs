using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.Business.Services
{
    public interface IUserService
    {
        #region Methods

        void Create(AbsenceModel absence);

        IEnumerable<AbsenceDisplayModel> GetAbsences();

        PersonDisplayModel GetPerson(long id);

        PickupRoundModel GetPickupRound(long id);

        IEnumerable<PickupRoundModel> GetPickupRounds();

        TeamModel GetTeam(long teamId);

        IEnumerable<TeamModel> GetTeams();

        void Remove(AbsenceModel absence);

        void Update(TeamModel team);

        void Update(PickupRoundModel round);

        void Update(AbsenceModel absence);

        #endregion Methods
    }
}