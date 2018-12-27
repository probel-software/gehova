namespace Probel.Gehova.Business.Models
{
    public class PersonDisplayModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public string CategoryKey { get; internal set; }
    }
}
