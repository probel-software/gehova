using Probel.Gehova.Business.I18n;
using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Gehova.Business.Mappers
{
    internal class ToPickupRoundMapper
    {
        #region Fields

        private IDataReader _reader;
        private List<DayPickupRoundModel> PickupRounds;

        #endregion Fields

        #region Constructors

        public ToPickupRoundMapper()
        {
            ResetPickupRounds();
        }

        #endregion Constructors

        #region Methods

        private (DayPickupRoundModel day, GroupModel pickupRound, PersonDisplayModel person) Get()
        {
            var did = _reader["day"] as long? ?? 0;
            var day = new DayPickupRoundModel
            {
                DayId = did,
                DayName = did.AsDay(),
            };
            var person = new PersonDisplayModel
            {
                Category = _reader["category"] as string ?? string.Empty,
                CategoryKey = _reader["category_key"] as string ?? string.Empty,
                FirstName = _reader["first_name"] as string ?? string.Empty,
                LastName = _reader["last_name"] as string ?? string.Empty,
                Id = _reader["person_id"] as long? ?? 0
            };
            var pickupRound = new GroupModel
            {
                Id = _reader["pickup_round_id"] as long? ?? 0,
                Name = _reader["pickup_round"] as string ?? string.Empty
            };
            return
            (
                day,
                pickupRound,
                person
            );
        }

        private DayPickupRoundModel Get(DayPickupRoundModel day)
        {
            var found = (from d in PickupRounds
                         where d.DayId == day.DayId
                         select d).Single();
            return found;
        }

        private GroupModel Get(GroupModel pickup, DayPickupRoundModel day)
        {
            var result = (from p in day.PickupRounds
                          where p.Id == pickup.Id
                          select p).Single();
            return result;
        }

        private bool Has(PersonDisplayModel person, GroupModel pickup)
        {
            var count = (from p in pickup.People
                         where p.Id == person.Id
                         select p).Count();
            return count > 0;
        }

        private bool Has(GroupModel pickup, DayPickupRoundModel day)
        {
            var count = (from p in day.PickupRounds
                         where p.Id == pickup.Id
                         select p).Count();
            return count > 0;
        }

        private void ResetPickupRounds()
        {
            if (PickupRounds == null) { PickupRounds = new List<DayPickupRoundModel>(); }
            else { PickupRounds.Clear(); }

            PickupRounds.Add(new DayPickupRoundModel { DayName = Strings.Monday, DayId = 1 });
            PickupRounds.Add(new DayPickupRoundModel { DayName = Strings.Tuesday, DayId = 2 });
            PickupRounds.Add(new DayPickupRoundModel { DayName = Strings.Wednesday, DayId = 3 });
            PickupRounds.Add(new DayPickupRoundModel { DayName = Strings.Thursday, DayId = 4 });
            PickupRounds.Add(new DayPickupRoundModel { DayName = Strings.Friday, DayId = 5 });
        }

        public IEnumerable<DayPickupRoundModel> Map(IDataReader src)
        {
            _reader = src ?? throw new ArgumentNullException(nameof(src), $"The instance of '{typeof(IDataReader)}' is NULL. Did you forgot to specified a valid DataReader?");
            ResetPickupRounds();

            while (_reader.Read())
            {
                var (day, pickup, person) = Get();

                var foundDay = Get(day);
                if (Has(pickup, foundDay))
                {
                    var foundPickup = Get(pickup, foundDay);
                    if (!Has(person, foundPickup))
                    {
                        foundPickup.People.Add(person);
                    }
                }
                else
                {
                    pickup.People.Add(person);
                    foundDay.PickupRounds.Add(pickup);
                }
            }
            return PickupRounds;
        }

        #endregion Methods
    }
}