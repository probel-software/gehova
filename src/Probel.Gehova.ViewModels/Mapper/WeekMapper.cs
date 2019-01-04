using Probel.Gehova.Business.Models;
using Probel.Gehova.ViewModels.Visualisation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Probel.Gehova.ViewModels.Mapper
{
    public class WeekMapper
    {
        #region Fields
        private readonly string[] DayNames = { "lundi", "mardi", "mercredi", "jeudi", "vendredi" };
        private readonly IEnumerable<WeekDay> _days;

        #endregion Fields

        #region Constructors

        public WeekMapper(IEnumerable<WeekDay> days)
        {
            _days = days;
        }

        #endregion Constructors

        #region Properties

        public WeekViewModel Result
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        private DayViewModel Get(string day)
        {
            var dayResult = new DayViewModel() { DayName = day };

            var all = (from d in _days
                       where d.Day == day
                       select d);

            var teamNames = all.Select(e => e.Team).Distinct();
            var teamsResult = new List<TeamViewModel>();
            foreach (var teamName in teamNames)
            {
                teamsResult.Add(new TeamViewModel()
                {
                    TeamName = teamName,
                    People = new ObservableCollection<PersonViewModel>(GetPeople(all, teamName))
                });
            }
            dayResult.Teams = new ObservableCollection<TeamViewModel>(teamsResult);
            return dayResult;
        }

        private IEnumerable<PersonViewModel> GetPeople(IEnumerable<WeekDay> all, string team)
        {
            var peopleInTeam = (from p in all
                                where p.Team == team
                                select p);

            var people = new List<PersonViewModel>();
            foreach (var p in peopleInTeam)
            {
                people.Add(new PersonViewModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName
                });
            }
            return people;
        }

        public WeekMapper Get()
        {
            if (Result == null)
            {
                var days = new List<DayViewModel>();
                foreach (var day in DayNames)
                {
                    days.Add(Get(day));
                }
                Result = new WeekViewModel { Days = new ObservableCollection<DayViewModel>(days) };
            }
            return this;
        }

        #endregion Methods
    }
}