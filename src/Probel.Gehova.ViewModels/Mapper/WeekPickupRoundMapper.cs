﻿using Probel.Gehova.Business.Models;
using Probel.Gehova.ViewModels.Vm.Visualisation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Probel.Gehova.ViewModels.Mapper
{
    public class WeekPickupRoundMapper
    {
        #region Fields
        private readonly string[] DayNames = { "lundi", "mardi", "mercredi", "jeudi", "vendredi" };
        private readonly IEnumerable<WeekDay> _days;

        #endregion Fields

        #region Constructors

        public WeekPickupRoundMapper(IEnumerable<WeekDay> days)
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

            var names = all.Select(e => e.PickupRound).Distinct();
            var pRoundResult = new List<PeopleBagViewModel>();
            foreach (var name in names)
            {
                pRoundResult.Add(new PeopleBagViewModel()
                {
                    Name = name,
                    People = new ObservableCollection<PersonViewModel>(GetPeople(all, name))
                });
            }
            dayResult.PeopleBags = new ObservableCollection<PeopleBagViewModel>(pRoundResult);
            return dayResult;
        }

        private IEnumerable<PersonViewModel> GetPeople(IEnumerable<WeekDay> all, string pickupRound)
        {
            var peopleInBag = (from p in all
                                where p.PickupRound == pickupRound
                                select p);

            var people = new List<PersonViewModel>();
            foreach (var p in peopleInBag)
            {
                people.Add(new PersonViewModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName
                });
            }
            return people;
        }

        public WeekPickupRoundMapper Get()
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
