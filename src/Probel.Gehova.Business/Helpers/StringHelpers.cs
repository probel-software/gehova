using System;

namespace Probel.Gehova.Business.Helpers
{
    public static class StringHelpers
    {
        public static Version AsVersion(this string str)
        {
            if (Version.TryParse(str, out var result))
            {
                return result;
            }
            else { throw new InvalidCastException($"Cannot cast '{str}' into a '{typeof(Version)}'"); }
        }
    }
}
