namespace Probel.Gehova.Business.Models
{
    public class PersonFullDisplayModel : PersonDisplayModel
    {
        #region Properties

        public string Category { get; set; }
        public bool IsLunchTime { get; set; }
        public bool IsReceptionEvening { get; set; }
        public bool IsReceptionMorning { get; set; }
        public string Team { get; set; }

        #endregion Properties
    }
}