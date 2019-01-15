using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Probel.Lorm
{
    public class LormConfigurator
    {
        #region Fields

        private static readonly Dictionary<Type, Func<IDataReader, IEnumerable<object>>> _mappers = new Dictionary<Type, Func<IDataReader, IEnumerable<object>>>();

        #endregion Fields

        #region Methods

        public Func<IDataReader, IEnumerable<object>> Get<TType>()
        {
            var mapper = (from m in _mappers
                          where m.Key == typeof(TType)
                          select m.Value).Single();
            return mapper;
        }

        public void AddMapper<TType>(Func<IDataReader, IEnumerable<object>> mapper)
        {
            _mappers.Add(typeof(TType), mapper);
        }

        #endregion Methods
    }
}