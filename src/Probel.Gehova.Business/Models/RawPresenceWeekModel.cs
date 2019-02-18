namespace Probel.Gehova.Business.Models
{
    internal class RawPresenceWeekModel
    {
        #region Properties

        public string Categories { get; set; }
        public string CategoryKeys { get; set; }
        public long Day { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonId { get; set; }
        public string PickupRound { get; set; }
        public long PickupRoundId { get; set; }
        public long ReceptionGroupId { get; set; }
        public long ReceptionId { get; set; }
        public string Team { get; set; }
        public long TeamId { get; set; }

        #endregion Properties
    }
}