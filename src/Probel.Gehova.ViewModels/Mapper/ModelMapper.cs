using Probel.Gehova.Business.Models;
using System.Collections.Generic;

namespace Probel.Gehova.ViewModels.Mapper
{
    public static class ModelMapper
    {
        #region Methods

        public static IList<PersonDisplayModel> ToPersonDisplayModel(this IEnumerable<PersonFullDisplayModel> src)
        {
            var result = new List<PersonDisplayModel>();

            foreach (var item in src) { result.Add(item); }

            return result;
        }

        public static IList<PersonDisplayModel> ToPersonModel(this IEnumerable<long> ids)
        {
            var result = new List<PersonDisplayModel>();
            foreach (var id in ids)
            {
                result.Add(new PersonDisplayModel() { Id = id });
            }
            return result;
        }

        #endregion Methods
    }
}