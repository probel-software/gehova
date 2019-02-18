using Probel.Gehova.Business.I18n;
using System;

namespace Probel.Gehova.Business.Mappers
{
    public static class StringExtension
    {
        #region Methods

        public static string AsDay(this long src)
        {
            switch (src)
            {
                case 1: return Strings.Monday;
                case 2: return Strings.Tuesday;
                case 3: return Strings.Wednesday;
                case 4: return Strings.Thursday;
                case 5: return Strings.Friday;
                default: throw new NotSupportedException($"The value '{src}' cannot be translated into a day of week name.");
            }
        }

        #endregion Methods
    }
}
